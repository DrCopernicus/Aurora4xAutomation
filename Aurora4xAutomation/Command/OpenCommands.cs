using System;
using System.Threading;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.Command
{
    [Obsolete("Commands and related classes should be discontinued in favor of Evaluators, and in the case of duplicated functionality using compound evaluators.")]
    public class OpenCommands
    {
        public OpenCommands(IUIMap uiMap)
        {
            UIMap = uiMap;
        }

        public void OpenResearchCategory(string category)
        {
            var output = "";

            UIMap.PopulationAndProduction.MakeActive();
            UIMap.PopulationAndProduction.SelectResearchTab();
            output += "Available Labs: " + UIMap.PopulationAndProduction.AvailableLabs.Text + "\n\n";
            if (category == "all")
            {
                UIMap.PopulationAndProduction.SelectBiology();
                output += ReadResearchTables();
                UIMap.PopulationAndProduction.SelectConstruction();
                output += ReadResearchTables();
                UIMap.PopulationAndProduction.SelectDefensive();
                output += ReadResearchTables();
                UIMap.PopulationAndProduction.SelectEnergy();
                output += ReadResearchTables();
                UIMap.PopulationAndProduction.SelectLogistics();
                output += ReadResearchTables();
                UIMap.PopulationAndProduction.SelectMissiles();
                output += ReadResearchTables();
                UIMap.PopulationAndProduction.SelectPower();
                output += ReadResearchTables();
                UIMap.PopulationAndProduction.SelectSensors();
                output += ReadResearchTables();
            }
            else
            {
                UIMap.PopulationAndProduction.SelectResearchByCategory(category);
                output += ReadResearchTables();
            }

            //new MessageCommands(Settings).PrintFeedback(output);
            throw new NotImplementedException();
        }

        public void SelectColony(string colony)
        {
            UIMap.PopulationAndProduction.MakeActive();
            UIMap.PopulationAndProduction.Populations.Select(colony);
        }

        public void ReadPopulations()
        {
            UIMap.PopulationAndProduction.MakeActive();
            //new MessageCommands(Settings).PrintFeedback(UIMap.PopulationAndProduction.Populations.Text);
            throw new NotImplementedException();
        }

        private string ReadResearchTables()
        {
            var output = "";
            UIMap.PopulationAndProduction.SetShowMatchingScientistsOnly(true);
            Thread.Sleep(250);
            output += UIMap.PopulationAndProduction.ResearchTable.GetText();
            output += UIMap.PopulationAndProduction.AvailableScientistsTable.GetText();
            return output;
        }

        private IUIMap UIMap { get; set; }
    }
}