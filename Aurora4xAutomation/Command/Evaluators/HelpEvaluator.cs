using System;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class HelpEvaluator : Evaluator
    {
        public HelpEvaluator(string text)
            : base(text)
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

            MessageCommands.PrintInterrupt(Body.Help);
        }

        public override string Help
        {
            get { return "help <command>: Returns the help message for <command>."; }
        }
    }
}
