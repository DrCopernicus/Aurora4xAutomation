using System;
using Aurora4xAutomation.Command;

namespace Aurora4xAutomation.Evaluators
{
    public class OpenPopulationEvaluator : Evaluator
    {
        public OpenPopulationEvaluator(string text)
            : base(text)
        {
        }

        public OpenPopulationEvaluator(params string[] parameters)
            : base("open-pop", parameters)
        {
            
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameter, got {0} in function name {1}.",
                    Parameters.Count, Text));

            new OpenCommands().SelectColony(Parameters[0]);
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
