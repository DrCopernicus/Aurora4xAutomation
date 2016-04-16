using System;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command
{
    public static class TurnCommands
    {
        public static void AdvanceTurn(object sender, EventArgs e)
        {
            UIMap.PopulationAndProductionWindow.Dirty();

            UIMap.SystemMap.MakeActive();
            switch (Settings.Increment)
            {
                case Settings.IncrementLength.FiveSecond:
                    UIMap.SystemMap.ClickIncrement5SecondsButton();
                    break;
                case Settings.IncrementLength.ThirtySecond:
                    UIMap.SystemMap.ClickIncrement30SecondsButton();
                    break;
                case Settings.IncrementLength.TwoMinute:
                    UIMap.SystemMap.ClickIncrement2MinutesButton();
                    break;
                case Settings.IncrementLength.FiveMinute:
                    UIMap.SystemMap.ClickIncrement5MinutesButton();
                    break;
                case Settings.IncrementLength.TwentyMinute:
                    UIMap.SystemMap.ClickIncrement20MinutesButton();
                    break;
                case Settings.IncrementLength.OneHour:
                    UIMap.SystemMap.ClickIncrement1HoursButton();
                    break;
                case Settings.IncrementLength.ThreeHour:
                    UIMap.SystemMap.ClickIncrement3HoursButton();
                    break;
                case Settings.IncrementLength.EightHour:
                    UIMap.SystemMap.ClickIncrement8HoursButton();
                    break;
                case Settings.IncrementLength.OneDay:
                    UIMap.SystemMap.ClickIncrement1DayButton();
                    break;
                case Settings.IncrementLength.FiveDay:
                    UIMap.SystemMap.ClickIncrement5DaysButton();
                    break;
                case Settings.IncrementLength.ThirtyDay:
                    UIMap.SystemMap.ClickIncrement30DaysButton();
                    break;
            }
        }
    }
}