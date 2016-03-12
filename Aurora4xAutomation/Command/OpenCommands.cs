using System.Threading;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command
{
    public class OpenCommands
    {
        public void OpenResearch()
        {
            _production.MakeActive();
            _production.SelectResearchTab();
        }

        public void OpenShipyard()
        {
            _production.MakeActive();
            _production.SelectManageShipyards();
        }

        public void OpenTaskGroup()
        {
            _taskGroups.MakeActive();
        }

        public string OpenResearch(string category)
        {
            var output = "";

            _production.MakeActive();
            _production.SelectResearchTab();
            if (category == "all")
            {
                _production.SelectBiology();
                output += ReadResearchTables();
                _production.SelectConstruction();
                output += ReadResearchTables();
                _production.SelectDefensive();
                output += ReadResearchTables();
                _production.SelectEnergy();
                output += ReadResearchTables();
                _production.SelectLogistics();
                output += ReadResearchTables();
                _production.SelectMissiles();
                output += ReadResearchTables();
                _production.SelectPower();
                output += ReadResearchTables();
                _production.SelectSensors();
                output += ReadResearchTables();
            }
            else
            {
                _production.SelectResearchByCategory(category);
                output += ReadResearchTables();
            }
            _console.MakeActive();
            return output;
        }

        private string ReadResearchTables()
        {
            var output = "";
            _production.SetShowMatchingScientistsOnly(true);
            Thread.Sleep(250);
            output += _production.ResearchTable.GetText();
            output += _production.AvailableScientistsTable.GetText();
            return output;
        }

        private readonly ConsoleWindow _console = new ConsoleWindow();
        private readonly TaskGroupsWindow _taskGroups = new TaskGroupsWindow();
        private readonly PopulationAndProductionWindow _production = new PopulationAndProductionWindow();
    }
}