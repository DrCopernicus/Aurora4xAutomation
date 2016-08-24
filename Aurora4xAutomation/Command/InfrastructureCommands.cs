using System;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.Settings;

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
        private SettingsStore Settings { get; set; }

        public void MakeCivilianContract(string population, string installation, int amount, bool supply)
        {
            new OpenCommands(UIMap).SelectColony(population);
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectCivilianTab();
            UIMap.PopulationAndProductionWindow.InstallationType.Text = installation;
            UIMap.PopulationAndProductionWindow.ContractAmount.Text = amount + "";
            if (supply)
                UIMap.PopulationAndProductionWindow.CivilianContractSupply.Selected = true;
            else
                UIMap.PopulationAndProductionWindow.CivilianContractDemand.Selected = true;
            UIMap.PopulationAndProductionWindow.AddCivilianContract.Click();
        }

        public void PurchaseMineralOutput(string massDriverDestination)
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectCivilianTab();
            UIMap.PopulationAndProductionWindow.PurchaseMineralOutput.Selected = true;
            UIMap.PopulationAndProductionWindow.SelectMiningTab();
            UIMap.PopulationAndProductionWindow.MassDriverDestination.Text = massDriverDestination;
        }

        public void BuildInstallation(string population, string installationName, string installationNumber)
        {
            new OpenCommands(UIMap).SelectColony(population);
            UIMap.PopulationAndProductionWindow.SelectIndustry();
            switch (installationName)
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
            UIMap.PopulationAndProductionWindow.NumberOfIndustrialProject.Text = installationNumber;
            UIMap.PopulationAndProductionWindow.CreateIndustrialProject.Click();
        }
    }
}