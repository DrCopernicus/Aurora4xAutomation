using System.Windows.Forms;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO.UI.Display;
using Aurora4xAutomation.Settings;
using Button = Aurora4xAutomation.IO.UI.Controls.Button;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class EventWindow : Window
    {
        public EventWindow(IScreen screen, IWindowFinder windowFinder, IInputDevice inputDevice, ISettingsStore settings) :
            base("Event Updates", screen, windowFinder, inputDevice, settings)
        {
            TextFileButton = new Button(this, inputDevice, left: 107, right: 187, top: 866, bottom: 890);
        }

        public Button TextFileButton { get; set; }

        protected override void OpenIfNotFound()
        {
            new BaseAuroraWindow(Screen, WindowFinder, InputDevice, Settings).MakeActive();
            StaticSleeper.Sleep(1000);
            SendKeys.SendWait("^{F3}");
        }
    }
}
