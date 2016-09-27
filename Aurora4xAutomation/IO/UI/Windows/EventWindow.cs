using System.Windows.Forms;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Settings;
using Button = Aurora4xAutomation.IO.UI.Controls.Button;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class EventWindow : Window
    {
        public EventWindow(IScreen screen, IWindowFinder windowFinder, ISettingsStore settings) : 
            base("Event Updates", screen, windowFinder, settings)
        {
            TextFileButton = new Button(this, left: 107, right: 187, top: 866, bottom: 890);
        }

        public Button TextFileButton { get; set; }

        protected override void OpenIfNotFound()
        {
            new BaseAuroraWindow(Screen, WindowFinder, Settings).MakeActive();
            Sleeper.Sleep(1000);
            SendKeys.SendWait("^{F3}");
        }
    }
}
