using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command
{
    public class InfrastructureCommands
    {
        public void TransferInfrastructure(string installation, int amount, bool supply)
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

        private readonly PopulationAndProductionWindow _production = new PopulationAndProductionWindow();
    }
}