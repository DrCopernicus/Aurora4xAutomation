using System;
using Server.IO.UI.Display;
using Server.Settings;

namespace Server.IO.UI.Windows
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
