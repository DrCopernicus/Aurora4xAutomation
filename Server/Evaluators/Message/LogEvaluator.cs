using System;
using Server.Common.Converters;
using Server.Messages;

namespace Server.Evaluators.Message
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
            get { return "log <type> <message>: Adds <message> with the tag <type>."; }
        }
    }
}
