using System.Threading;
using System.Windows.Forms;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.UI
{
    public class EventWindow : Window
    {
        public EventWindow() : 
            base("Event Updates")
        {
            
        }

        public void ClickTextFileButton()
        {
            this.Click(152, 880);
        }

        protected override void OpenIfNotFound()
        {
            new AuroraWrapperWindow().OpenBase();
            Thread.Sleep(1000);
            SendKeys.SendWait("^{F3}");
        }
    }
}
