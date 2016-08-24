using System;
using System.Threading;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Command
{
    [Obsolete("Commands and related classes should be discontinued in favor of Evaluators, and in the case of duplicated functionality using compound evaluators.")]
    public class OpenCommands
    {
        public OpenCommands(IUIMap uiMap)
        {
            UIMap = uiMap;
        }

        public void OpenResearch()
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectResearchTab();
        }

        public void OpenShipyard()
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectManageShipyards();
        }

        public void OpenTaskGroup()
        {
            UIMap.TaskGroups.MakeActive();
        }

        public void OpenResearchCategory(string category)
        {
            var output = "";

            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectResearchTab();
            output += "Available Labs: " + UIMap.PopulationAndProductionWindow.AvailableLabs.Text + "\n\n";
            if (category == "all")
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
                UIMap.PopulationAndProductionWindow.SelectResearchByCategory(category);
                output += ReadResearchTables();
            }

            //new MessageCommands(Settings).PrintFeedback(output);
            throw new NotImplementedException();
        }

        public void SelectColony(string colony)
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.Populations.Select(colony);
        }

        public void ReadPopulations()
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            //new MessageCommands(Settings).PrintFeedback(UIMap.PopulationAndProductionWindow.Populations.Text);
            throw new NotImplementedException();
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

        private IUIMap UIMap { get; set; }
    }
}