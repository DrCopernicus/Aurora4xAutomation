using System;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command.Parser
{
    public class SetPopulationCommand : CommandEvaluator
    {
        public SetPopulationCommand(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        public override void Evaluate()
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
