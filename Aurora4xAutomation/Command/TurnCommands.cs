using System;
using Aurora4xAutomation.Settings;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command
{
    public static class TurnCommands
    {
        public static void AdvanceTurn(object sender, EventArgs e)
        {
            UIMap.PopulationAndProductionWindow.Dirty();

            UIMap.SystemMap.MakeActive();
            switch (SettingsStore.Increment)
            {
                case SettingsStore.IncrementLength.FiveSecond:
                    UIMap.SystemMap.ClickIncrement5SecondsButton();
                    break;
                case SettingsStore.IncrementLength.ThirtySecond:
                    UIMap.SystemMap.ClickIncrement30SecondsButton();
                    break;
                case SettingsStore.IncrementLength.TwoMinute:
                    UIMap.SystemMap.ClickIncrement2MinutesButton();
                    break;
                case SettingsStore.IncrementLength.FiveMinute:
                    UIMap.SystemMap.ClickIncrement5MinutesButton();
                    break;
                case SettingsStore.IncrementLength.TwentyMinute:
                    UIMap.SystemMap.ClickIncrement20MinutesButton();
                    break;
                case SettingsStore.IncrementLength.OneHour:
                    UIMap.SystemMap.ClickIncrement1HoursButton();
                    break;
                case SettingsStore.IncrementLength.ThreeHour:
                    UIMap.SystemMap.ClickIncrement3HoursButton();
                    break;
                case SettingsStore.IncrementLength.EightHour:
                    UIMap.SystemMap.ClickIncrement8HoursButton();
                    break;
                case SettingsStore.IncrementLength.OneDay:
                    UIMap.SystemMap.ClickIncrement1DayButton();
                    break;
                case SettingsStore.IncrementLength.FiveDay:
                    UIMap.SystemMap.ClickIncrement5DaysButton();
                    break;
                case SettingsStore.IncrementLength.ThirtyDay:
                    UIMap.SystemMap.ClickIncrement30DaysButton();
                    break;
            }
        }
    }
}