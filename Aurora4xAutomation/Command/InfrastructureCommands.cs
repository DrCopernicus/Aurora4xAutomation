using System;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command
{
    public static class InfrastructureCommands
    {
        public static void TransferInfrastructure(string installation, int amount, bool supply)
        {
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
    }
}