using System;
using Aurora4xAutomation.Messages;

namespace Aurora4xAutomation.Evaluators.Message
{
    public class HelpEvaluator : MessageEvaluator
    {
        public HelpEvaluator(string text, IMessageManager messages)
            : base(text, messages)
        {
        }

        public override CommandEvaluatorType GetEvaluatorType()
        {
            return CommandEvaluatorType.Help;
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameter, got {0} in function name {1}.",
                    Parameters.Count, Text));

            Messages.AddMessage(MessageType.Information, Body.Help);
        }

        public override string Help
        {
            get { return "help <command>: Returns the help message for <command>."; }
        }
    }
}
