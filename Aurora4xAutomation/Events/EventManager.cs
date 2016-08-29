using Aurora4xAutomation.Command;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Evaluators.Factories;
using Aurora4xAutomation.Evaluators.Message;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.Settings;
using System.ComponentModel;
using System.Linq;

namespace Aurora4xAutomation.Events
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
            _timeline.AddEvent(evaluator);
        }

        public void Begin(ILogger handler)
        {
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
                        e.Cancel = true;
                        break;
                    }
                    CompleteOneRound();
                    Sleeper.Sleep(1000);
                }
            };

            _worker.RunWorkerCompleted += (sender, e) =>
            {
                handler.Write("Stopping event manager.");

                if (e.Error != null)
                    handler.Error(e.Error);
            };

            _worker.RunWorkerAsync();
        }

        private void CompleteOneRound()
        {
            ActOnActiveTimelineEntries();

            if (!Settings.Stopped)
            {
                ParseEvents();

                if (!Settings.Stopped)
                    new TurnCommands(UIMap, Settings).AdvanceTurn();

                if (!Settings.AutoTurnsOn)
                    new StopEvaluator("stop", Settings).Execute();
            }
            else
            {
                var log = new LogEvaluator("log", Messages);
                new EvaluatorParameterizer().SetParameters(log, MessageType.Debug, "Waiting for user input.");
                log.Execute();
            }
        }

        public void Stop()
        {
            if (_worker == null)
                return;

            _worker.CancelAsync();
        }

        public void ActOnActiveTimelineEntries()
        {
            var log = new LogEvaluator("log", Messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Debug, "Waiting for user input.");
            log.Execute();

            IEvaluator ev;
            while ((ev = _timeline.PopNextActiveEvent(UIMap.GetTime())) != null)
                ev.Execute();
        }

        public void ParseEvents()
        {
            var log = new LogEvaluator("log", Messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Debug, "Parsing event log.");
            log.Execute();

            if (Settings.DatabasePassword == null)
                new EventParser(UIMap, Settings).ParseUsingEventWindow(UIMap.SystemMap.GetTime());
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
