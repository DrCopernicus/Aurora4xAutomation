using Aurora4xAutomation.Common.Converters;
using Aurora4xAutomation.Messages;
using System;

namespace Aurora4xAutomation.Evaluators.Message
{
    public class LogEvaluator : MessageEvaluator
    {
        public LogEvaluator(string text, IMessageManager messages) : base(text, messages)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 2)
                throw new Exception(string.Format("Expected 2 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));
            
            Messages.AddMessage(MessageTypeConverter.ToType(Parameters[0]), Parameters[1]);
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
