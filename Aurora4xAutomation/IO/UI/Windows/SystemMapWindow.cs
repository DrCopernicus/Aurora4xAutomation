using Aurora4xAutomation.Common;
using Aurora4xAutomation.Settings;
using System.Windows.Forms;

namespace Aurora4xAutomation.IO.UI.Windows
{
    public class SystemMapWindow : Window
    {
        public SystemMapWindow(IScreen screen, IWindowFinder windowFinder, ISettingsStore settings) :
            base("System Map", screen, windowFinder, settings)
        {
            
        }

        public void ClickIncrement5SecondsButton()
        {
            this.Click(329, 110);
        }

        public void ClickIncrement30SecondsButton()
        {
            this.Click(409, 110);
        }

        public void ClickIncrement2MinutesButton()
        {
            this.Click(488, 110);
        }

        public void ClickIncrement5MinutesButton()
        {
            this.Click(568, 110);
        }

        public void ClickIncrement20MinutesButton()
        {
            this.Click(647, 110);
        }

        public void ClickIncrement1HoursButton()
        {
            this.Click(730, 110);
        }

        public void ClickIncrement3HoursButton()
        {
            this.Click(808, 110);
        }

        public void ClickIncrement8HoursButton()
        {
            this.Click(889, 110);
        }

        public void ClickIncrement1DayButton()
        {
            this.Click(969, 110);
        }

        public void ClickIncrement5DaysButton()
        {
            this.Click(1052, 110);
        }

        public void ClickIncrement30DaysButton()
        {
            this.Click(1130, 110);
        }

        public string GetTime()
        {
            return GetWindowText().Substring(10).Trim();
        }

        protected override void OpenIfNotFound()
        {
            new BaseAuroraWindow(Screen, WindowFinder, Settings).MakeActive();
            Sleeper.Sleep(1000);
            SendKeys.SendWait("{F3}");
        }
    }
}
