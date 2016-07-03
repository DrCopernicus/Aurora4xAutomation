﻿using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class ReadDataEvaluator : Evaluator
    {
        public ReadDataEvaluator(string text)
            : base(text)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 2)
                throw new CommandExecutionException(2, Parameters.Count, Text);

            if (Parameters[0] == "research")
                OpenCommands.OpenResearchCategory(Parameters[1]);
        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
