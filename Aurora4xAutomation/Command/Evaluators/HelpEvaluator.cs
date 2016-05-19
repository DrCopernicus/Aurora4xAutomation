﻿using System;
using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class HelpEvaluator : Evaluator
    {
        public HelpEvaluator(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            Timeline.AddEvent(MessageCommands.PrintInterrupt, Body.Help);
        }

        public override string Help
        {
            get { return "help: Takes 1 parameter. Returns the help message for the command in the parameter."; }
        }
    }
}
