using System;
using Aurora4xAutomation.Evaluators.Factories;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.Evaluators
{
    public class MoveEvaluator : UIEvaluator
    {
        public MoveEvaluator(string text, IUIMap uiMap)
            : base(text, uiMap)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 4)
                throw new Exception(string.Format("Expected 4 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            var supplyContract = new ContractEvaluator("move", UIMap);
            new EvaluatorParameterizer().SetParameters(supplyContract, Parameters[0], Parameters[2], Parameters[3], true);

            var demandContract = new ContractEvaluator("move", UIMap);
            new EvaluatorParameterizer().SetParameters(demandContract, Parameters[1], Parameters[2], Parameters[3], false);

            supplyContract.Execute();
            demandContract.Execute();
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
