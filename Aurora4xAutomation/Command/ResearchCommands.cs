using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command
{
    public class ResearchCommands
    {
        public void ResearchTechCommand(string category, int researchNum, int scientistNum, int labsNum)
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectResearchTab();
            UIMap.PopulationAndProductionWindow.SelectResearchByCategory(category);

            UIMap.PopulationAndProductionWindow.SetShowMatchingScientistsOnly(true);
            Thread.Sleep(500);
            UIMap.PopulationAndProductionWindow.SelectNthResearch(researchNum);
            UIMap.PopulationAndProductionWindow.SelectNthScientist(scientistNum);
            if (labsNum != -1)
                UIMap.PopulationAndProductionWindow.SetAllocatedLabs(labsNum + "");
            UIMap.PopulationAndProductionWindow.CreateResearch();
        }

        public void FocusResearch(string category)
        {
            Settings.Research.Clear();
            foreach (var ban in Settings.ResearchFocuses[category])
                Settings.Research.Add(ban.Key, ban.Value);
        }

        public void BanResearch(string topic)
        {
            throw new NotImplementedException();
        }

        public void AutoResearch(object sender, EventArgs e)
        {
            UIMap.PopulationAndProductionWindow.MakeActive();
            UIMap.PopulationAndProductionWindow.SelectResearchTab();

            UIMap.PopulationAndProductionWindow.SetShowMatchingScientistsOnly(true);
            Thread.Sleep(500);

            while (int.Parse(UIMap.PopulationAndProductionWindow.AvailableLabs.Text) > 0)
            {
                var scientists = new List<string[]>();
                var research = new List<string[]>();

                UIMap.PopulationAndProductionWindow.SelectBiology();
                Thread.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "bg" }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "bg" }));
                UIMap.PopulationAndProductionWindow.SelectConstruction();
                Thread.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "cp" }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "cp" }));
                UIMap.PopulationAndProductionWindow.SelectDefensive();
                Thread.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "ds" }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "ds" }));
                UIMap.PopulationAndProductionWindow.SelectEnergy();
                Thread.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "ew" }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "ew" }));
                UIMap.PopulationAndProductionWindow.SelectLogistics();
                Thread.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "lg" }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "lg" }));
                UIMap.PopulationAndProductionWindow.SelectMissiles();
                Thread.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "mk" }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "mk" }));
                UIMap.PopulationAndProductionWindow.SelectPower();
                Thread.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "pp" }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "pp" }));
                UIMap.PopulationAndProductionWindow.SelectSensors();
                Thread.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "sf" }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "sf" }));

                if (!SelectScience(research, scientists))
                {
                    Timeline.AddEvent(SettingsCommands.Stop);
                    Timeline.AddEvent(MessageCommands.PrintError, "[AutoResearch] Failed to assign a new research project.");
                    break;
                }

                Thread.Sleep(500);
            }
        }

        private bool SelectScience(List<string[]> research, List<string[]> scientists)
        {
            foreach (var searchFor in Settings.Research)
            {
                if (scientists.FirstOrDefault(x => x[0] != "" && x[1] == searchFor.Value) == null)
                    continue;

                foreach (var searchAgainst in research)
                {
                    if (searchFor.Key.SimilarTo(searchAgainst[0]) && searchFor.Value == searchAgainst[2])
                    {
                        UIMap.PopulationAndProductionWindow.SelectResearchByCategory(searchFor.Value);
                        Thread.Sleep(500);
                        var res = UIMap.PopulationAndProductionWindow.ResearchTable.GetTable();

                        for (int i = 0; i < res.Count; i++)
                        {
                            if (res[i][0] == searchAgainst[0])
                            {
                                ResearchTechCommand(searchFor.Value, i, 0, -1);
                                Timeline.AddEvent(MessageCommands.PrintFeedback, string.Format("[AutoResearch] Successfully selected research {0}.", searchFor.Key));
                                return true;
                            }
                        }

                        break;
                    }
                }
            }

            return false;
        }
    }
}