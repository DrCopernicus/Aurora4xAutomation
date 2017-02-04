using Server.Evaluators.Helpers;
using Server.Messages;
using System;

namespace Server.Evaluators.Message
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

            Messages.AddMessage(MessageType.Information, Body.Help.ToFormattedString());
        }

        public override HelpText Help
        {
            get { return new HelpText("help", "")
                .AddRow("<command>", "Returns the help message for {0}."); }
        }
    }
}
