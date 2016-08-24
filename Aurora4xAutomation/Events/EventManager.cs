using Aurora4xAutomation.Evaluators;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Events
{
    public class EventManager
    {
        public EventManager(IUIMap uiMap)
        {
            UIMap = uiMap;
        }

        public void AddEvent(IEvaluator evaluator, Time time = null)
        {
            _timeline.AddEvent(evaluator);
        }

        public void ActOnActiveTimelineEntries()
        {
            SettingsStore.StatusMessage = "Evaluating events";
            IEvaluator ev;
            while ((ev = _timeline.PopNextActiveEvent(new Time(UIMap.SystemMap.GetTime()))) != null)
                ev.Execute();
        }

        public void ParseEvents()
        {
            SettingsStore.StatusMessage = "Parsing events log";

            if (SettingsStore.DatabasePassword == null)
            {
                new EventParser(UIMap).ParseUsingEventWindow(UIMap.SystemMap.GetTime());
            }
            else
                new EventParser(UIMap).ParseUsingDatabase();
        }

        private IUIMap UIMap { get; set; }
        private readonly Timeline _timeline = new Timeline();
    }
}
