﻿using System;
using Aurora4xAutomation.Command;
using Aurora4xAutomation.IO;

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

            new OpenCommands(UIMap).SelectColony(Parameters[0]);

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
