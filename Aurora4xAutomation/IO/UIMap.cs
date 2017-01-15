using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO.UI;
using Aurora4xAutomation.IO.UI.Display;
using Aurora4xAutomation.IO.UI.Windows;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.IO
{
    public class UIMap : IUIMap
    {
        public UIMap(ISettingsStore settings, IWindowFinder windowFinder, IOCRReader ocr, IInputDevice input, IScreen screen)
        {
            Settings = settings;
            WindowFinder = windowFinder;
            OCR = ocr;
            InputDevice = input;
            Screen = screen;
        }

        public ISettingsStore Settings { get; set; }
        public IWindowFinder WindowFinder { get; set; }
        public IOCRReader OCR { get; set; }
        public IInputDevice InputDevice { get; set; }
        public IScreen Screen { get; set; }

        public BaseAuroraWindow BaseAuroraWindow
        {
            get { return _baseAuroraWindow ?? (_baseAuroraWindow = new BaseAuroraWindow(Screen, WindowFinder, InputDevice, Settings)); }
        }

        public EventWindow Events
        {
            get { return _eventWindow ?? (_eventWindow = new EventWindow(Screen, WindowFinder, InputDevice, Settings)); }
        }

        public CommandersWindow Leaders
        {
            get { return _commandersWindow ?? (_commandersWindow = new CommandersWindow(Screen, WindowFinder, InputDevice, OCR, Settings)); }
        }

        public SystemMapWindow SystemMap
        {
            get { return _systemMapWindow ?? (_systemMapWindow = new SystemMapWindow(Screen, WindowFinder, InputDevice, Settings)); }
        }

        public PopulationAndProductionWindow PopulationAndProduction
        {
            get { return _populationAndProductionWindow ?? (_populationAndProductionWindow = new PopulationAndProductionWindow(Screen, WindowFinder, InputDevice, OCR, Settings)); }
        }

        public TaskGroupsWindow TaskGroups
        {
            get { return _taskGroupsWindow ?? (_taskGroupsWindow = new TaskGroupsWindow(Screen, WindowFinder, InputDevice, Settings)); }
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
