using System.Windows.Forms;
using Server.Common;
using Server.IO.UI.Display;
using Server.Settings;

namespace Server.IO.UI.Windows
{
    public class SystemMapWindow : Window
    {
        public SystemMapWindow(IScreen screen, IWindowFinder windowFinder, IInputDevice inputDevice, ISettingsStore settings) :
            base("System Map", screen, windowFinder, inputDevice, settings)
        {
            
        }

        public void ClickIncrement5SecondsButton()
        {
            Click(329, 110);
        }

        public void ClickIncrement30SecondsButton()
        {
            Click(409, 110);
        }

        public void ClickIncrement2MinutesButton()
        {
            Click(488, 110);
        }

        public void ClickIncrement5MinutesButton()
        {
            Click(568, 110);
        }

        public void ClickIncrement20MinutesButton()
        {
            Click(647, 110);
        }

        public void ClickIncrement1HoursButton()
        {
            Click(730, 110);
        }

        public void ClickIncrement3HoursButton()
        {
            Click(808, 110);
        }

        public void ClickIncrement8HoursButton()
        {
            Click(889, 110);
        }

        public void ClickIncrement1DayButton()
        {
            Click(969, 110);
        }

        public void ClickIncrement5DaysButton()
        {
            Click(1052, 110);
        }

        public void ClickIncrement30DaysButton()
        {
            Click(1130, 110);
        }

        public string GetTime()
        {
            return GetWindowText().Substring(10).Trim();
        }

        protected override void OpenIfNotFound()
        {
            new BaseAuroraWindow(Screen, WindowFinder, InputDevice, Settings).MakeActive();
            StaticSleeper.Sleep(1000);
            SendKeys.SendWait("{F3}");
        }
    }
}
