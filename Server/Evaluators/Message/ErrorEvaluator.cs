using Server.Messages;
using System;
using Server.Evaluators.Helpers;

namespace Server.Evaluators.Message
{
    public class ErrorEvaluator : MessageEvaluator
    {
        public ErrorEvaluator(string text, IMessageManager messages) : base(text, messages)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameter, got {0} in function name {1}.",
                    Parameters.Count, Text));

            Messages.AddMessage(MessageType.Error, Parameters[0]);
        }

        public override string Help
        {
            get { return "error <message>: Adds <message> with the \"error\" tag."; }
        }

        public static ErrorEvaluator Error(string message, string stackTrace, IMessageManager messageManager)
        {
            var evaluator = new ErrorEvaluator("log", messageManager);
            new EvaluatorParameterizer().SetParameters(evaluator, message + "\n" + stackTrace);
            return evaluator;
        }
    }
}