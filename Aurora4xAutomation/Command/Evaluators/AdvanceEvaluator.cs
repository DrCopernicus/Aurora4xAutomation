using System;
using Aurora4xAutomation.Command.Parser;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class AdvanceEvaluator : Evaluator
    {
        public AdvanceEvaluator(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        public override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameter, got {0} in function name {1}.",
                    Parameters.Count, Text));

            switch (Parameters[0])
            {
                case "off":
                    Settings.AutoTurnsOn = false;
                    break;
                case "on":
                    Settings.AutoTurnsOn = true;
                    break;
                case "5s":
                    Settings.Increment = Settings.IncrementLength.FiveSecond;
                    break;
                case "30s":
                    Settings.Increment = Settings.IncrementLength.ThirtySecond;
                    break;
                case "2m":
                    Settings.Increment = Settings.IncrementLength.TwoMinute;
                    break;
                case "5m":
                    Settings.Increment = Settings.IncrementLength.FiveMinute;
                    break;
                case "20m":
                    Settings.Increment = Settings.IncrementLength.TwentyMinute;
                    break;
                case "1h":
                    Settings.Increment = Settings.IncrementLength.OneHour;
                    break;
                case "3h":
                    Settings.Increment = Settings.IncrementLength.ThreeHour;
                    break;
                case "8h":
                    Settings.Increment = Settings.IncrementLength.EightHour;
                    break;
                case "1d":
                    Settings.Increment = Settings.IncrementLength.OneDay;
                    break;
                case "5d":
                    Settings.Increment = Settings.IncrementLength.FiveDay;
                    break;
                case "30d":
                    Settings.Increment = Settings.IncrementLength.ThirtyDay;
                    break;
                default:
                    Settings.Increment = Settings.IncrementLength.FiveDay;
                    break;
            }
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
