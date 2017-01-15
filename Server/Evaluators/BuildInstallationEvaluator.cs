using System;
using Server.IO;

namespace Server.Evaluators
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
            UIMap.PopulationAndProduction.SelectIndustry();
            switch (Parameters[1])
            {
                case "automine":
                    UIMap.PopulationAndProduction.ConstructionOptions.ClickRow(0);
                    break;
                case "csc":
                    UIMap.PopulationAndProduction.ConstructionOptions.ClickRow(1);
                    break;
                case "inf":
                case "infra":
                case "infrastructure":
                    UIMap.PopulationAndProduction.ConstructionOptions.ClickRow(10);
                    break;
                case "massdriver":
                    UIMap.PopulationAndProduction.ConstructionOptions.ClickRow(12);
                    break;
                case "nsc":
                    UIMap.PopulationAndProduction.ConstructionOptions.ClickRow(15);
                    break;
                case "lab":
                    UIMap.PopulationAndProduction.ConstructionOptions.ClickRow(17);
                    break;
                case "terra":
                    UIMap.PopulationAndProduction.ConstructionOptions.ClickRow(19);
                    break;
            }
            UIMap.PopulationAndProduction.NumberOfIndustrialProject.Text = Parameters[2];
            UIMap.PopulationAndProduction.CreateIndustrialProject.Click();
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
