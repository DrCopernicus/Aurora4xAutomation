using Server.Messages;

namespace Server.Evaluators.Message
{
    public abstract class MessageEvaluator : Evaluator
    {
        protected MessageEvaluator(string text, IMessageManager messages) : base(text)
        {
            Messages = messages;
        }

        protected IMessageManager Messages;
    }
}
