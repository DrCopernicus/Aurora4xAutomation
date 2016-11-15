using System;
using Aurora4xAutomation.IO.UI.Display;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class BaseAuroraWindow : Window
    {
        public BaseAuroraWindow(IScreen screen, IWindowFinder windowFinder, IInputDevice inputDevice, ISettingsStore settings) :
            base(settings.GameName, screen, windowFinder, inputDevice, settings)
        {
            
        }

        protected override void OpenIfNotFound()
        {
            throw new Exception("Base Aurora Window not found!");
        }
    }
}
