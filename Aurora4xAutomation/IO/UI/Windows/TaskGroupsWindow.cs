using System.Windows.Forms;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class TaskGroupsWindow : Window
    {
        public TaskGroupsWindow()
            : base("Task Groups")
        {
            
        }

        protected override void OpenIfNotFound()
        {
            new BaseAuroraWindow().MakeActive();
            Sleeper.Sleep(1000);
            SendKeys.SendWait("{F12}");
        }
    }
}
