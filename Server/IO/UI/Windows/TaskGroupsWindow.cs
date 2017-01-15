using System.Windows.Forms;
using Server.Common;
using Server.IO.UI.Display;
using Server.Settings;

namespace Server.IO.UI.Windows
{
    public class TaskGroupsWindow : Window
    {
        public TaskGroupsWindow(IScreen screen, IWindowFinder windowFinder, IInputDevice inputDevice, ISettingsStore settings)
            : base("Task Groups", screen, windowFinder, inputDevice, settings)
        {
            
        }

        protected override void OpenIfNotFound()
        {
            new BaseAuroraWindow(Screen, WindowFinder, InputDevice, Settings).MakeActive();
            StaticSleeper.Sleep(1000);
            SendKeys.SendWait("{F12}");
        }
    }
}
