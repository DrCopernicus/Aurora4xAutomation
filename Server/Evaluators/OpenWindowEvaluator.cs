﻿using Server.Common.Exceptions;
using Server.Evaluators.Helpers;
using Server.IO;

namespace Server.Evaluators
{
    public class OpenWindowEvaluator : UIEvaluator
    {
        public OpenWindowEvaluator(string text, IUIMap uiMap)
            : base(text, uiMap)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new CommandExecutionException(1, Parameters.Count, Text);

            switch (Parameters[0])
            {
                case "r":
                    UIMap.PopulationAndProduction.MakeActive();
                    UIMap.PopulationAndProduction.SelectResearchTab();
                    break;
                case "ship":
                    UIMap.PopulationAndProduction.MakeActive();
                    UIMap.PopulationAndProduction.SelectManageShipyards();
                    break;
                case "tg":
                    UIMap.TaskGroups.MakeActive();
                    break;
            }
        }

        public override HelpText Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
