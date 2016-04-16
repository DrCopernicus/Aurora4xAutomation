using System;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command
{
    public static class InfrastructureCommands
    {
        public static void TransferInfrastructure(string installation, int amount, bool supply)
        {
            _production.MakeActive();
            _production.SelectCivilianTab();
            _production.InstallationType.Text = installation;
            _production.ContractAmount.Text = amount + "";
            if (supply)
                _production.CivilianContractSupply.Selected = true;
            else
                _production.CivilianContractDemand.Selected = true;
            _production.AddCivilianContract.Click();
        }

        public static void PurchaseMineralOutput(object sender, EventArgs e)
        {
            _production.MakeActive();
            _production.SelectCivilianTab();
            _production.PurchaseMineralOutput.Selected = true;
            _production.SelectMiningTab();
            _production.MassDriverDestination.Text = ((MessageEventArgs) e).Message;
        }

        private static readonly PopulationAndProductionWindow _production = new PopulationAndProductionWindow();
    }
}