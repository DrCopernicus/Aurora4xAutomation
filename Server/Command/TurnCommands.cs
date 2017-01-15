using System;
using Server.IO;
using Server.Settings;

namespace Server.Command
{
    [Obsolete("Commands and related classes should be discontinued in favor of Evaluators, and in the case of duplicated functionality using compound evaluators.")]
    public class TurnCommands
    {
        public TurnCommands(IUIMap uiMap, ISettingsStore settings)
        {
            UIMap = uiMap;
            Settings = settings;
        }

        private IUIMap UIMap { get; set; }
        private ISettingsStore Settings { get; set; }

        public void AdvanceTurn()
        {
            UIMap.PopulationAndProduction.Dirty();

            UIMap.SystemMap.MakeActive();
            switch (Settings.Increment)
            {
                case IncrementLength.FiveSecond:
                    UIMap.SystemMap.ClickIncrement5SecondsButton();
                    break;
                case IncrementLength.ThirtySecond:
                    UIMap.SystemMap.ClickIncrement30SecondsButton();
                    break;
                case IncrementLength.TwoMinute:
                    UIMap.SystemMap.ClickIncrement2MinutesButton();
                    break;
                case IncrementLength.FiveMinute:
                    UIMap.SystemMap.ClickIncrement5MinutesButton();
                    break;
                case IncrementLength.TwentyMinute:
                    UIMap.SystemMap.ClickIncrement20MinutesButton();
                    break;
                case IncrementLength.OneHour:
                    UIMap.SystemMap.ClickIncrement1HoursButton();
                    break;
                case IncrementLength.ThreeHour:
                    UIMap.SystemMap.ClickIncrement3HoursButton();
                    break;
                case IncrementLength.EightHour:
                    UIMap.SystemMap.ClickIncrement8HoursButton();
                    break;
                case IncrementLength.OneDay:
                    UIMap.SystemMap.ClickIncrement1DayButton();
                    break;
                case IncrementLength.FiveDay:
                    UIMap.SystemMap.ClickIncrement5DaysButton();
                    break;
                case IncrementLength.ThirtyDay:
                    UIMap.SystemMap.ClickIncrement30DaysButton();
                    break;
            }
        }
    }
}