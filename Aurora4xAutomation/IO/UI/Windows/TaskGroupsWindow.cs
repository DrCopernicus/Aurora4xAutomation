using System.Windows.Forms;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class TaskGroupsWindow : Window
    {
        public TaskGroupsWindow(IScreen screen, IWindowFinder windowFinder, ISettingsStore settings)
            : base("Task Groups", screen, windowFinder, settings)
        {
            
        }

        protected override void OpenIfNotFound()
        {
            new BaseAuroraWindow(Screen, WindowFinder, Settings).MakeActive();
            Sleeper.Sleep(1000);
            SendKeys.SendWait("{F12}");
        }
    }
}
