using System.Windows.Forms;
using Server.Common;
using Server.IO.UI.Display;
using Server.Settings;
using Button = Server.IO.UI.Controls.Button;

namespace Server.IO.UI.Windows
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
