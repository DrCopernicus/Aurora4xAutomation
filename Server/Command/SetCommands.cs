using System;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command
{
    public static class InfrastructureCommands
    {
        public static void MakeCivilianContract(string population, string installation, int amount, bool supply)
        {
            new OpenCommands().SelectColony(population);
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

        public static void PurchaseMineralOutput(object sender, EventArgs e)
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectCivilianTab();
            UIMap.PopulationAndProductionWindow.PurchaseMineralOutput.Selected = true;
            UIMap.PopulationAndProductionWindow.SelectMiningTab();
            UIMap.PopulationAndProductionWindow.MassDriverDestination.Text = ((MessageEventArgs)e).Message;
        }

        public static void BuildInstallation(string population, string installationName, string installationNumber)
        {
            new OpenCommands().SelectColony(population);
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
                    UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(11);
                    break;
                case "nsc":
                    UIMap.PopulationAndProductionWindow.ConstructionOptions.ClickRow(14);
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