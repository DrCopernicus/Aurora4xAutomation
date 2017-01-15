using System.ComponentModel;
using System.Linq;
using Server.Command;
using Server.Common;
using Server.Evaluators;
using Server.Evaluators.Factories;
using Server.Evaluators.Message;
using Server.IO;
using Server.Messages;
using Server.Settings;

namespace Server.Events
{
    public class EventManager : IEventManager
    {
        public EventManager(IUIMap uiMap, ISettingsStore settings, IMessageManager messages)
        {
            UIMap = uiMap;
            Settings = settings;
            Messages = messages;
        }

        public void AddEvent(IEvaluator evaluator, Time time = null)
        {
            _timeline.AddEvent(evaluator, time);
        }

        public void Begin(ILogger handler)
        {
            handler.Write("Beginning event manager.");
            if (Settings.ResearchFocuses != null && Settings.ResearchFocuses.Any())
                Settings.Research = Settings.ResearchFocuses["beamfocus"];

            _worker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };

            _worker.DoWork += (sender, e) =>
            {
                while (true)
                {
                    if (_worker.CancellationPending)
                    {
                        handler.Write("Cancellation pending.");
                        e.Cancel = true;
                        break;
                    }
                    CompleteOneRound(handler);
                    StaticSleeper.Sleep(1000);
                }
            };

            _worker.RunWorkerCompleted += (sender, e) =>
            {
                handler.Write("Stopping event manager.");

                if (e.Error != null)
                    handler.Error(e.Error.Message, e.Error.StackTrace);
            };

            _worker.RunWorkerAsync();
        }

        private void CompleteOneRound(ILogger handler)
        {
            ActOnActiveTimelineEntries();

            if (Settings.Stopped)
                return;

            ParseAuroraEventLog();

            if (!Settings.Stopped)
                new TurnCommands(UIMap, Settings).AdvanceTurn();

            if (!Settings.AutoTurnsOn)
                new StopEvaluator("stop", Settings).Execute();
        }

        public void Stop()
        {
            if (_worker == null)
                return;

            _worker.CancelAsync();
        }

        public void ActOnActiveTimelineEntries()
        {
            IEvaluator ev;
            while ((ev = _timeline.PopNextActiveEvent(UIMap.GetTime())) != null)
                ev.Execute();
        }

        private void ParseAuroraEventLog()
        {
            var log = new LogEvaluator("log", Messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Debug, "Parsing event log.");
            log.Execute();

            if (Settings.DatabasePassword == null)
                new EventParser(UIMap, Settings).ParseUsingEventWindow(UIMap.GetTime());
            else
                new EventParser(UIMap, Settings).ParseUsingDatabase();
        }

        private IUIMap UIMap { get; set; }
        private ISettingsStore Settings { get; set; }
        private IMessageManager Messages { get; set; }
        private readonly Timeline _timeline = new Timeline();
        private BackgroundWorker _worker;
    }
}
