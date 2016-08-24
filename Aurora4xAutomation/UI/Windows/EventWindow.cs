using System.Threading;
using System.Windows.Forms;
using Aurora4xAutomation.Common;
using Button = Aurora4xAutomation.UI.Controls.Button;

namespace Aurora4xAutomation.UI.Windows
{
    public class EventWindow : Window
    {
        public EventWindow() : 
            base("Event Updates")
        {
            TextFileButton = new Button(this, left: 107, right: 187, top: 866, bottom: 890);
        }

        public Button TextFileButton { get; set; }

        protected override void OpenIfNotFound()
        {
            new BaseAuroraWindow().MakeActive();
            Sleeper.Sleep(1000);
            SendKeys.SendWait("^{F3}");
        }
    }
}
