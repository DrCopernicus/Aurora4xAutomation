using System;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class ConsoleWindow : Window
    {
        public ConsoleWindow(SettingsStore settings) : 
            base(@"file:///", settings)
        {
            
        }

        protected override void OpenIfNotFound()
        {
            throw new Exception("Console window not found!");
        }
    }
}
