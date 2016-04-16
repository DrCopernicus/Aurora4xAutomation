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
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectResearchTab();
        }

        public void OpenShipyard(object sender, EventArgs e)
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectManageShipyards();
        }

        public void OpenTaskGroup(object sender, EventArgs e)
        {
            _taskGroups.MakeActive();
        }

        public void OpenResearchCategory(object sender, EventArgs e)
        {
            var output = "";

            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectResearchTab();
            output += "Available Labs: " + UIMap.PopulationAndProductionWindow.AvailableLabs.Text + "\n\n";
            if (((MessageEventArgs) e).Message == "all")
            {
                UIMap.PopulationAndProductionWindow.SelectBiology();
                output += ReadResearchTables();
                UIMap.PopulationAndProductionWindow.SelectConstruction();
                output += ReadResearchTables();
                UIMap.PopulationAndProductionWindow.SelectDefensive();
                output += ReadResearchTables();
                UIMap.PopulationAndProductionWindow.SelectEnergy();
                output += ReadResearchTables();
                UIMap.PopulationAndProductionWindow.SelectLogistics();
                output += ReadResearchTables();
                UIMap.PopulationAndProductionWindow.SelectMissiles();
                output += ReadResearchTables();
                UIMap.PopulationAndProductionWindow.SelectPower();
                output += ReadResearchTables();
                UIMap.PopulationAndProductionWindow.SelectSensors();
                output += ReadResearchTables();
            }
            else
            {
                UIMap.PopulationAndProductionWindow.SelectResearchByCategory(((MessageEventArgs)e).Message);
                output += ReadResearchTables();
            }

            Timeline.AddEvent(MessageCommands.PrintFeedback, output);
        }

        public void SelectColony(string colony)
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.Populations.Select(colony);
        }

        public void SelectColony(object sender, EventArgs e)
        {
            SelectColony(((MessageEventArgs) e).Message);
        }

        public void ReadPopulations(object sender, EventArgs e)
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            Timeline.AddEvent(MessageCommands.PrintFeedback, UIMap.PopulationAndProductionWindow.Populations.Text);
        }

        private string ReadResearchTables()
        {
            var output = "";
            UIMap.PopulationAndProductionWindow.SetShowMatchingScientistsOnly(true);
            Thread.Sleep(250);
            output += UIMap.PopulationAndProductionWindow.ResearchTable.GetText();
            output += UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetText();
            return output;
        }

        private readonly TaskGroupsWindow _taskGroups = new TaskGroupsWindow();
    }
}