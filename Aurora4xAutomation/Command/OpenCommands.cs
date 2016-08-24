using System;
using System.Threading;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.IO.UI;

namespace Aurora4xAutomation.Command
{
    public class OpenCommands
    {
        public static void OpenResearch()
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectResearchTab();
        }

        public static void OpenShipyard()
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectManageShipyards();
        }

        public static void OpenTaskGroup()
        {
            UIMap.TaskGroups.MakeActive();
        }

        public static void OpenResearchCategory(string category)
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

            MessageCommands.PrintFeedback(output);
        }

        public void SelectColony(string colony)
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.Populations.Select(colony);
        }

        public void ReadPopulations()
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            MessageCommands.PrintFeedback(UIMap.PopulationAndProductionWindow.Populations.Text);
        }

        private static string ReadResearchTables()
        {
            var output = "";
            UIMap.PopulationAndProductionWindow.SetShowMatchingScientistsOnly(true);
            Thread.Sleep(250);
            output += UIMap.PopulationAndProductionWindow.ResearchTable.GetText();
            output += UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetText();
            return output;
        }
    }
}