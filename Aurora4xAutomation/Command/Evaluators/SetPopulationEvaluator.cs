using System;
using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class SetPopulationEvaluator : Evaluator
    {
        public SetPopulationEvaluator(string text)
            : base(text)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 3)
                throw new Exception(string.Format("Expected 3 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            new OpenCommands().SelectColony(Parameters[0]);

            if (Parameters[1] == "mining-destination")
            {
                UIMap.PopulationAndProductionWindow.MakeActive();
                UIMap.PopulationAndProductionWindow.SelectMiningTab();
                UIMap.PopulationAndProductionWindow.MassDriverDestination.Text = Parameters[2];
            }
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
