using Aurora4xAutomation.IO;
using System;

namespace Aurora4xAutomation.Evaluators
{
    public class SetPopulationEvaluator : UIEvaluator
    {
        public SetPopulationEvaluator(string text, IUIMap uiMap)
            : base(text, uiMap)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 3)
                throw new Exception(string.Format("Expected 3 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            UIMap.PopulationAndProduction.MakeActive();
            UIMap.PopulationAndProduction.Populations.Select(Parameters[0]);

            if (Parameters[1] == "mining-destination")
            {
                UIMap.PopulationAndProduction.MakeActive();
                UIMap.PopulationAndProduction.SelectMiningTab();
                UIMap.PopulationAndProduction.MassDriverDestination.Text = Parameters[2];
            }
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
