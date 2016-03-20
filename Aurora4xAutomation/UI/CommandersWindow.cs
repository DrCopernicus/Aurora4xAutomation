using System.Threading;
using System.Windows.Forms;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.UI
{
    public class CommandersWindow : Window
    {
        public CommandersWindow() 
            : base("Commanders")
        {
            
        }

        public void SetAutomatedAssignments(bool toggle)
        {
            if ((GetPixel(100, 88).EqualsColor(0, 0, 0) && !toggle)
                || GetPixel(100, 88).EqualsColor(255, 255, 255) && toggle)
            {
                Click(100, 88);
            }
        }

        protected override void OpenIfNotFound()
        {
            new AuroraWrapperWindow().OpenBase();
            Thread.Sleep(1000);
            SendKeys.SendWait("{F4}");
        }
    }
}
