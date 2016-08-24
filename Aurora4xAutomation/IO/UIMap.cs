using Aurora4xAutomation.IO.UI.Windows;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.IO
{
    public class UIMap : IUIMap
    {
        public UIMap(SettingsStore settings)
        {
            Settings = settings;
        }

        public SettingsStore Settings { get; set; }

        public BaseAuroraWindow BaseAuroraWindow
        {
            get { return _baseAuroraWindow ?? (_baseAuroraWindow = new BaseAuroraWindow(Settings)); }
        }

        public EventWindow EventWindow
        {
            get { return _eventWindow ?? (_eventWindow = new EventWindow(Settings)); }
        }

        public CommandersWindow Leaders
        {
            get { return _commandersWindow ?? (_commandersWindow = new CommandersWindow(Settings)); }
        }

        public SystemMapWindow SystemMap
        {
            get { return _systemMapWindow ?? (_systemMapWindow = new SystemMapWindow(Settings)); }
        }

        public PopulationAndProductionWindow PopulationAndProductionWindow
        {
            get { return _populationAndProductionWindow ?? (_populationAndProductionWindow = new PopulationAndProductionWindow(Settings)); }
        }

        public TaskGroupsWindow TaskGroups
        {
            get { return _taskGroupsWindow ?? (_taskGroupsWindow = new TaskGroupsWindow(Settings)); }
        }
        
        private EventWindow _eventWindow;
        private CommandersWindow _commandersWindow;
        private SystemMapWindow _systemMapWindow;
        private PopulationAndProductionWindow _populationAndProductionWindow;
        private TaskGroupsWindow _taskGroupsWindow;
        private BaseAuroraWindow _baseAuroraWindow;
    }
}
