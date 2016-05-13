using System;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command.Parser
{
    public class PrintCommand : CommandEvaluator
    {
        public PrintCommand(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        public override void Evaluate()
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
