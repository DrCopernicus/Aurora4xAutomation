using Aurora4xAutomation.IO.UI.Windows;

namespace Aurora4xAutomation.IO.UI
{
    public static class UIMap
    {
        public static EventWindow EventWindow
        {
            get { return _eventWindow ?? (_eventWindow = new EventWindow()); }
        }

        public static CommandersWindow Leaders
        {
            get { return _commandersWindow ?? (_commandersWindow = new CommandersWindow()); }
        }

        public static SystemMapWindow SystemMap
        {
            get { return _systemMapWindow ?? (_systemMapWindow = new SystemMapWindow()); }
        }

        public static PopulationAndProductionWindow PopulationAndProductionWindow
        {
            get { return _populationAndProductionWindow ?? (_populationAndProductionWindow = new PopulationAndProductionWindow()); }
        }

        public static TaskGroupsWindow TaskGroups
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
