using System;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class BaseAuroraWindow : Window
    {
        public BaseAuroraWindow() : 
            base(SettingsStore.GameName)
        {
            
        }

        protected override void OpenIfNotFound()
        {
            throw new Exception("Base Aurora Window not found!");
        }
    }
}
