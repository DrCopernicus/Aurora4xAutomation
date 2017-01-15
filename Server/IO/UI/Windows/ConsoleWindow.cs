using System;
using Server.IO.UI.Display;
using Server.Settings;

namespace Server.IO.UI.Windows
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
