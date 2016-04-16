using System;
using System.Threading;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command
{
    public class OpenCommands
    {
        public void OpenResearch(object sender, EventArgs e)
        {
            _production.MakeActive();
            _production.SelectResearchTab();
        }

        public void OpenShipyard(object sender, EventArgs e)
        {
            _production.MakeActive();
            _production.SelectManageShipyards();
        }

        public void OpenTaskGroup(object sender, EventArgs e)
        {
            _taskGroups.MakeActive();
        }

        public void OpenResearchCategory(object sender, EventArgs e)
        {
            var output = "";

            _production.MakeActive();
            _production.SelectResearchTab();
            output += "Available Labs: " + _production.AvailableLabs.Text + "\n\n";
            if (((MessageEventArgs) e).Message == "all")
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
                _production.SelectResearchByCategory(((MessageEventArgs)e).Message);
                output += ReadResearchTables();
            }

            Timeline.AddEvent(MessageCommands.PrintFeedback, output);
        }

        public void SelectColony(object sender, EventArgs e)
        {
            _production.MakeActive();
            _production.Populations.Select(((MessageEventArgs)e).Message);
        }

        public void ReadPopulations(object sender, EventArgs e)
        {
            _production.MakeActive();
            Timeline.AddEvent(MessageCommands.PrintFeedback, _production.Populations.Text);
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

        private readonly TaskGroupsWindow _taskGroups = new TaskGroupsWindow();
        private readonly PopulationAndProductionWindow _production = new PopulationAndProductionWindow();
    }
}