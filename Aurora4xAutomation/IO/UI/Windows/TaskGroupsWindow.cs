using System.Windows.Forms;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class TaskGroupsWindow : Window
    {
        public TaskGroupsWindow(SettingsStore settings)
            : base("Task Groups", settings)
        {
            
        }

        protected override void OpenIfNotFound()
        {
            new BaseAuroraWindow(Settings).MakeActive();
            Sleeper.Sleep(1000);
            SendKeys.SendWait("{F12}");
        }
    }
}
