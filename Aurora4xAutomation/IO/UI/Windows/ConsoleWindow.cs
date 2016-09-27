using System;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class ConsoleWindow : Window
    {
        public ConsoleWindow(IScreen screen, IWindowFinder windowFinder, IInputDevice inputDevice, ISettingsStore settings) :
            base(@"file:///", screen, windowFinder, inputDevice, settings)
        {
            
        }

        protected override void OpenIfNotFound()
        {
            throw new Exception("Console window not found!");
        }
    }
}
