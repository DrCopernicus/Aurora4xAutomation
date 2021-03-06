﻿using Server.Evaluators.Helpers;
using Server.Messages;
using System;

namespace Server.Evaluators.Message
{
    public class PrintEvaluator : MessageEvaluator
    {
        public PrintEvaluator(string text, IMessageManager messages)
            : base(text, messages)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameter, got {0} in function name {1}.",
                    Parameters.Count, Text));

            Messages.AddMessage(MessageType.Information, Parameters[0]);
        }

        public override HelpText Help
        {
            get { return new HelpText("print", "")
                .AddRow("<message>", "Adds {0} with the \"info\" tag."); }
        }
    }
}
