using System;
using System.Collections.Generic;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class AdvanceEvaluator : Evaluator
    {
        public AdvanceEvaluator(string text)
            : base(text)
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
                default:
                    try
                    {
                        SettingsStore.Increment = _incrementLengthStringRepresentations[Parameters[0]];
                    }
                    catch (KeyNotFoundException)
                    {
                        SettingsStore.Increment = SettingsStore.IncrementLength.FiveDay;
                    }
                    break;
            }
        }

        private readonly Dictionary<string, SettingsStore.IncrementLength> _incrementLengthStringRepresentations = new Dictionary
            <string, SettingsStore.IncrementLength>
        {
            {"5s", SettingsStore.IncrementLength.FiveSecond},
            {"30s", SettingsStore.IncrementLength.ThirtySecond},
            {"2m", SettingsStore.IncrementLength.TwoMinute},
            {"5m", SettingsStore.IncrementLength.FiveMinute},
            {"20m", SettingsStore.IncrementLength.TwentyMinute},
            {"1h", SettingsStore.IncrementLength.OneHour},
            {"3h", SettingsStore.IncrementLength.ThreeHour},
            {"8h", SettingsStore.IncrementLength.EightHour},
            {"1d", SettingsStore.IncrementLength.OneDay},
            {"5d", SettingsStore.IncrementLength.FiveDay},
            {"30d", SettingsStore.IncrementLength.ThirtyDay}
        };

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
