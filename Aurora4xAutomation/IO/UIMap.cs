using Aurora4xAutomation.IO.UI.Windows;

namespace Aurora4xAutomation.IO
{
    public class UIMap : IUIMap
    {
        public EventWindow EventWindow
        {
            get { return _eventWindow ?? (_eventWindow = new EventWindow()); }
        }

        public CommandersWindow Leaders
        {
            get { return _commandersWindow ?? (_commandersWindow = new CommandersWindow()); }
        }

        public SystemMapWindow SystemMap
        {
            get { return _systemMapWindow ?? (_systemMapWindow = new SystemMapWindow()); }
        }

        public PopulationAndProductionWindow PopulationAndProductionWindow
        {
            get { return _populationAndProductionWindow ?? (_populationAndProductionWindow = new PopulationAndProductionWindow()); }
        }

        public TaskGroupsWindow TaskGroups
        {
            get { return _taskGroupsWindow ?? (_taskGroupsWindow = new TaskGroupsWindow()); }
        }
        
        private static EventWindow _eventWindow;
        private static CommandersWindow _commandersWindow;
        private static SystemMapWindow _systemMapWindow;
        private static PopulationAndProductionWindow _populationAndProductionWindow;
        private static TaskGroupsWindow _taskGroupsWindow;
    }
}
