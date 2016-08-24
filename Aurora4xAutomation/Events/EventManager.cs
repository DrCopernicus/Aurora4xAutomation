using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Events
{
    public class EventManager
    {
        public EventManager(IUIMap uiMap, SettingsStore settings)
        {
            UIMap = uiMap;
            Settings = settings;
        }

        public void AddEvent(IEvaluator evaluator, Time time = null)
        {
            _timeline.AddEvent(evaluator);
        }

        public void ActOnActiveTimelineEntries()
        {
            Settings.StatusMessage = "Evaluating events";
            IEvaluator ev;
            while ((ev = _timeline.PopNextActiveEvent(new Time(UIMap.SystemMap.GetTime()))) != null)
                ev.Execute();
        }

        public void ParseEvents()
        {
            Settings.StatusMessage = "Parsing events log";

            if (Settings.DatabasePassword == null)
            {
                new EventParser(UIMap, Settings).ParseUsingEventWindow(UIMap.SystemMap.GetTime());
            }
            else
                new EventParser(UIMap, Settings).ParseUsingDatabase();
        }

        private IUIMap UIMap { get; set; }
        private SettingsStore Settings { get; set; }
        private readonly Timeline _timeline = new Timeline();
    }
}
