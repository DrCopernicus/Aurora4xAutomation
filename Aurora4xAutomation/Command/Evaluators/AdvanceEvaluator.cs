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
            get { return "adv go: Allows the program to advance turns. If auto-turns are on, will automatically advance turns until blocked." +
                         "Otherwise if auto-turns are off, will only advance once.\n" +
                         "adv stop: Blocks the program from advancing turns until turns are allowed again (for example, by calling \"adv go\").\n" +
                         "adv on: Puts the program into the auto-turn on state, which automatically advances turns by the specified increment length" +
                         "(default five days), and will only be stopped when blocked (for example, by calling \"adv stop\").\n" +
                         "adv off: Puts the program into the auto-turn off state, which will only advance turns when put back into" +
                         "the auto-turn on state (for example, by calling \"adv on\").\n" +
                         "adv (5s|30s|2m|5m|20m|1h|3h|8h|1d|5d|30d): Specifies the turn increment length."; }
        }
    }
}
