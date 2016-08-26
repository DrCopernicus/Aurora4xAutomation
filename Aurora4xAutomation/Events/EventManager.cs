using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.Evaluators.Factories;
using Aurora4xAutomation.Evaluators.Message;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.Messages;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Events
{
    public class EventManager
    {
        public EventManager(IUIMap uiMap, SettingsStore settings, IMessageManager messages)
        {
            UIMap = uiMap;
            Settings = settings;
            Messages = messages;
        }

        public void AddEvent(IEvaluator evaluator, Time time = null)
        {
            _timeline.AddEvent(evaluator);
        }

        public void ActOnActiveTimelineEntries()
        {
            var log = new LogEvaluator("log", Messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Debug, "Waiting for user input.");

            IEvaluator ev;
            while ((ev = _timeline.PopNextActiveEvent(new Time(UIMap.SystemMap.GetTime()))) != null)
                ev.Execute();
        }

        public void ParseEvents()
        {
            var log = new LogEvaluator("log", Messages);
            new EvaluatorParameterizer().SetParameters(log, MessageType.Debug, "Parsing event log.");

            if (Settings.DatabasePassword == null)
                new EventParser(UIMap, Settings).ParseUsingEventWindow(UIMap.SystemMap.GetTime());
            else
                new EventParser(UIMap, Settings).ParseUsingDatabase();
        }

        private IUIMap UIMap { get; set; }
        private SettingsStore Settings { get; set; }
        private IMessageManager Messages { get; set; }
        private readonly Timeline _timeline = new Timeline();
    }
}
