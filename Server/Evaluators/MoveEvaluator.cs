using Server.Evaluators.Helpers;
using Server.IO;
using System;

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

        public override HelpText Help
        {
            get { return new HelpText("move", "")
                .AddRow("<from colony>", "<to colony>", "<amount>", "<installation>", "Creates a supply contract on {0} and a demand contract on {1} for {2} units of {3}."); }
        }
    }
}
