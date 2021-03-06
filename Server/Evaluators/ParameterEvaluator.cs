﻿using System;
using System.Linq;
using Server.Common;
using Server.Evaluators.Helpers;

namespace Server.Evaluators
{
    public class ParameterEvaluator : Evaluator
    {
        public ParameterEvaluator(string text)
            : base(text)
        {
        }

        public override CommandEvaluatorType GetEvaluatorType()
        {
            return CommandEvaluatorType.Parameter;
        }

        public ParameterEvaluator(string text, params string[] parameters)
            : base(text)
        {
            if (parameters == null || !parameters.Any())
                return;
            
            Body = new ParameterEvaluator(parameters[0], parameters.Subset(1));
        }

        protected override void Evaluate()
        {

        }

        public override HelpText Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
