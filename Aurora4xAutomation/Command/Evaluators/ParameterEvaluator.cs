using System;
using System.Linq;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class ParameterEvaluator : Evaluator
    {
        public ParameterEvaluator(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        public ParameterEvaluator(string text, CommandEvaluatorType type, params string[] parameters)
            : base(text, type)
        {
            if (parameters == null || !parameters.Any())
                return;
            
            Body = new ParameterEvaluator(parameters[0], CommandEvaluatorType.Parameter, parameters.Subset(1));
        }

        protected override void Evaluate()
        {

        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
