using System;
using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class PrintEvaluator : Evaluator
    {
        public PrintEvaluator(string text)
            : base(text)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameter, got {0} in function name {1}.",
                    Parameters.Count, Text));

            Timeline.AddEvent(MessageCommands.PrintFeedback, Parameters[0]);
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
