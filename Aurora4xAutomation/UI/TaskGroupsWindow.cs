using System.Threading;
using System.Windows.Forms;

namespace Aurora4xAutomation.UI
{
    public class TaskGroupsWindow : Window
    {
        public TaskGroupsWindow()
            : base("Task Groups")
        {
            
        }

        protected override void OpenIfNotFound()
        {
            new AuroraWrapperWindow().OpenBase();
            Thread.Sleep(1000);
            SendKeys.SendWait("{F12}");
        }
    }
}
