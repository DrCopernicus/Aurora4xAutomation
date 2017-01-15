using System;
using System.Collections.Generic;
using Server.Settings;

namespace Server.Evaluators
{
    public class AdvanceEvaluator : SettingsEvaluator
    {
        public AdvanceEvaluator(string text, ISettingsStore settings)
            : base(text, settings)
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
                    Settings.Stopped = false;
                    break;
                case "stop":
                    Settings.Stopped = true;
                    break;
                case "off":
                    Settings.AutoTurnsOn = false;
                    break;
                case "on":
                    Settings.AutoTurnsOn = true;
                    break;
                default:
                    try
                    {
                        Settings.Increment = _incrementLengthStringRepresentations[Parameters[0]];
                    }
                    catch (KeyNotFoundException)
                    {
                        Settings.Increment = IncrementLength.FiveDay;
                    }
                    break;
            }
        }

        private readonly Dictionary<string, IncrementLength> _incrementLengthStringRepresentations = new Dictionary
            <string, IncrementLength>
        {
            {"5s", IncrementLength.FiveSecond},
            {"30s", IncrementLength.ThirtySecond},
            {"2m", IncrementLength.TwoMinute},
            {"5m", IncrementLength.FiveMinute},
            {"20m", IncrementLength.TwentyMinute},
            {"1h", IncrementLength.OneHour},
            {"3h", IncrementLength.ThreeHour},
            {"8h", IncrementLength.EightHour},
            {"1d", IncrementLength.OneDay},
            {"5d", IncrementLength.FiveDay},
            {"30d", IncrementLength.ThirtyDay}
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
