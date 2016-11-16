using System;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.Command
{
    [Obsolete("Commands and related classes should be discontinued in favor of Evaluators, and in the case of duplicated functionality using compound evaluators.")]
    public class InfrastructureCommands
    {
        public InfrastructureCommands(IUIMap uiMap)
        {
            UIMap = uiMap;
        }

        private IUIMap UIMap { get; set; }

        public void PurchaseMineralOutput(string massDriverDestination)
        {
            UIMap.PopulationAndProduction.MakeActive();
            UIMap.PopulationAndProduction.SelectCivilianTab();
            UIMap.PopulationAndProduction.PurchaseMineralOutput.Select();
            UIMap.PopulationAndProduction.SelectMiningTab();
            UIMap.PopulationAndProduction.MassDriverDestination.Text = massDriverDestination;
        }

        public void BuildInstallation(string population, string installationName, string installationNumber)
        {
            new OpenCommands(UIMap).SelectColony(population);
            UIMap.PopulationAndProduction.SelectIndustry();
            switch (installationName)
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
            UIMap.PopulationAndProduction.NumberOfIndustrialProject.Text = installationNumber;
            UIMap.PopulationAndProduction.CreateIndustrialProject.Click();
        }
    }
}