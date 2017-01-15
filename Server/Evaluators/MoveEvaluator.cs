using System;
using Server.IO;

namespace Server.Evaluators
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

            var supplyContract = ContractEvaluator.SupplyContract(UIMap, Parameters[0], Parameters[3], Convert.ToInt32(Parameters[2]));
            var demandContract = ContractEvaluator.DemandContract(UIMap, Parameters[1], Parameters[3], Convert.ToInt32(Parameters[2]));

            supplyContract.Execute();
            demandContract.Execute();
        }

        public override string Help
        {
            get { return "move <from colony> <to colony> <amount> <installation>: Creates a supply contract on <from colony> and a demand contract on <to colony> for <amount> units of <installation>."; }
        }
    }
}
