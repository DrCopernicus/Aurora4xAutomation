using System;
using Aurora4xAutomation.Command;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Evaluators
{
    public class HelpEvaluator : SettingsEvaluator
    {
        public HelpEvaluator(string text, SettingsStore settings)
            : base(text, settings)
        {
        }

        public override CommandEvaluatorType GetEvaluatorType()
        {
            return CommandEvaluatorType.Help;
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            new MessageCommands(Settings).PrintInterrupt(Body.Help);
        }

        public override string Help
        {
            get { return "help <command>: Returns the help message for <command>."; }
        }
    }
}
