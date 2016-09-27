using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Windows;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.IO
{
    public class UIMap : IUIMap
    {
        public UIMap(ISettingsStore settings)
        {
            Settings = settings;
        }

        public ISettingsStore Settings { get; set; }
        public IWindowFinder WindowFinder { get; set; }
        public IScreen Screen { get; set; }

        public BaseAuroraWindow BaseAuroraWindow
        {
            get { return _baseAuroraWindow ?? (_baseAuroraWindow = new BaseAuroraWindow(Screen, WindowFinder, Settings)); }
        }

        public EventWindow Events
        {
            get { return _eventWindow ?? (_eventWindow = new EventWindow(Screen, WindowFinder, Settings)); }
        }

        public CommandersWindow Leaders
        {
            get { return _commandersWindow ?? (_commandersWindow = new CommandersWindow(Screen, WindowFinder, Settings)); }
        }

        public SystemMapWindow SystemMap
        {
            get { return _systemMapWindow ?? (_systemMapWindow = new SystemMapWindow(Screen, WindowFinder, Settings)); }
        }

        public PopulationAndProductionWindow PopulationAndProduction
        {
            get { return _populationAndProductionWindow ?? (_populationAndProductionWindow = new PopulationAndProductionWindow(Screen, WindowFinder, Settings)); }
        }

        public TaskGroupsWindow TaskGroups
        {
            get { return _taskGroupsWindow ?? (_taskGroupsWindow = new TaskGroupsWindow(Screen, WindowFinder, Settings)); }
        }

        public Time GetTime()
        {
            return new Time(SystemMap.GetTime());
        }
        
        private EventWindow _eventWindow;
        private CommandersWindow _commandersWindow;
        private SystemMapWindow _systemMapWindow;
        private PopulationAndProductionWindow _populationAndProductionWindow;
        private TaskGroupsWindow _taskGroupsWindow;
        private BaseAuroraWindow _baseAuroraWindow;
    }
}
