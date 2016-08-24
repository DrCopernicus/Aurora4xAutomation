using System;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.Evaluators
{
    public class BuildInstallationEvaluator : UIEvaluator
    {
        public BuildInstallationEvaluator(string text, IUIMap uiMap)
            : base(text, uiMap)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 3)
                throw new Exception(string.Format("Expected 3 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            new OpenPopulationEvaluator(Parameters[0], UIMap).Execute();
            UIMap.PopulationAndProductionWindow.SelectIndustry();
            switch (Parameters[1])
            {
                case "automine":
                    UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(0);
                    break;
                case "csc":
                    UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(1);
                    break;
                case "inf":
                case "infra":
                case "infrastructure":
                    UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(10);
                    break;
                case "massdriver":
                    UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(12);
                    break;
                case "nsc":
                    UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(15);
                    break;
                case "lab":
                    UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(17);
                    break;
                case "terra":
                    UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(19);
                    break;
            }
            UIMap.PopulationAndProductionWindow.NumberOfIndustrialProject.Text = Parameters[2];
            UIMap.PopulationAndProductionWindow.CreateIndustrialProject.Click();
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
