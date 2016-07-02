using System;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class AdvanceEvaluator : Evaluator
    {
        public AdvanceEvaluator(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameter, got {0} in function name {1}.",
                    Parameters.Count, Text));

            switch (Parameters[0])
            {
                case "go":
                    SettingsStore.Stopped = false;
                    break;
                case "stop":
                    SettingsStore.Stopped = true;
                    break;
                case "off":
                    SettingsStore.AutoTurnsOn = false;
                    break;
                case "on":
                    SettingsStore.AutoTurnsOn = true;
                    break;
                case "5s":
                    SettingsStore.Increment = SettingsStore.IncrementLength.FiveSecond;
                    break;
                case "30s":
                    SettingsStore.Increment = SettingsStore.IncrementLength.ThirtySecond;
                    break;
                case "2m":
                    SettingsStore.Increment = SettingsStore.IncrementLength.TwoMinute;
                    break;
                case "5m":
                    SettingsStore.Increment = SettingsStore.IncrementLength.FiveMinute;
                    break;
                case "20m":
                    SettingsStore.Increment = SettingsStore.IncrementLength.TwentyMinute;
                    break;
                case "1h":
                    SettingsStore.Increment = SettingsStore.IncrementLength.OneHour;
                    break;
                case "3h":
                    SettingsStore.Increment = SettingsStore.IncrementLength.ThreeHour;
                    break;
                case "8h":
                    SettingsStore.Increment = SettingsStore.IncrementLength.EightHour;
                    break;
                case "1d":
                    SettingsStore.Increment = SettingsStore.IncrementLength.OneDay;
                    break;
                case "5d":
                    SettingsStore.Increment = SettingsStore.IncrementLength.FiveDay;
                    break;
                case "30d":
                    SettingsStore.Increment = SettingsStore.IncrementLength.ThirtyDay;
                    break;
                default:
                    SettingsStore.Increment = SettingsStore.IncrementLength.FiveDay;
                    break;
            }
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
