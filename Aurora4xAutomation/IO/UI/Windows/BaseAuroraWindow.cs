using System;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class BaseAuroraWindow : Window
    {
        public BaseAuroraWindow(SettingsStore settings) :
            base(settings.GameName, settings)
        {
            
        }

        protected override void OpenIfNotFound()
        {
            throw new Exception("Base Aurora Window not found!");
        }
    }
}
