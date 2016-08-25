using Aurora4xAutomation.Messages;

namespace Aurora4xAutomation.Evaluators.Message
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
