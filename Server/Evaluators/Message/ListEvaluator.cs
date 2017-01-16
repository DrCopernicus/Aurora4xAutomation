using Server.Command.Parser;
using Server.Messages;
using System;

namespace Server.Evaluators.Message
{
    public class ListEvaluator : MessageEvaluator
    {
        public ListEvaluator(string text, IMessageManager messages, EvaluatorRegistry registry) : base(text, messages)
        {
            Registry = registry;
        }

        private EvaluatorRegistry Registry { get; set; }

        protected override void Evaluate()
        {
            if (Parameters.Count == 0)
                Messages.AddMessage(MessageType.Information, string.Format("All commands: {0}", string.Join(", ", Registry.ListCommands())));
            else if (Parameters.Count == 1)
                Messages.AddMessage(MessageType.Information, string.Format("Commands starting with {0}: {1}", Parameters[0], string.Join(", ", Registry.ListCommands(Parameters[0]))));
            else
                throw new Exception(string.Format("Expected 0 or 1 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));
        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}