using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Events;
using Aurora4xAutomation.UI;
using MoreLinq;

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
            Sleeper.Sleep(500);

            if (int.Parse(UIMap.PopulationAndProductionWindow.AvailableLabs.Text) != 0)
            {
                var scientists = new List<string[]>();
                var research = new List<string[]>();

                UIMap.PopulationAndProductionWindow.SelectBiology();
                Sleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "bg", ""+index }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "bg" }));
                UIMap.PopulationAndProductionWindow.SelectConstruction();
                Sleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "cp", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "cp" }));
                UIMap.PopulationAndProductionWindow.SelectDefensive();
                Sleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "ds", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "ds" }));
                UIMap.PopulationAndProductionWindow.SelectEnergy();
                Sleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "ew", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "ew" }));
                UIMap.PopulationAndProductionWindow.SelectLogistics();
                Sleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "lg", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "lg" }));
                UIMap.PopulationAndProductionWindow.SelectMissiles();
                Sleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "mk", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "mk" }));
                UIMap.PopulationAndProductionWindow.SelectPower();
                Sleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "pp", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "pp" }));
                UIMap.PopulationAndProductionWindow.SelectSensors();
                Sleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProductionWindow.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "sf", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProductionWindow.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "sf" }));

                if (SelectScience(research, scientists))
                    return;

                Timeline.AddEvent(SettingsCommands.Stop);
                Timeline.AddEvent(MessageCommands.PrintError, "[AutoResearch] Failed to assign a new research project.");
            }
        }

        private bool SelectScience(List<string[]> research, List<string[]> scientists)
        {
            var totalScientists = scientists.Count(x => x[0] != "");
            foreach (var searchFor in Settings.Research)
            {
                var sci = scientists.Where(x => x[0] != "" && x[1] == searchFor.Value).ToList();
                if (!sci.Any())
                    continue;

                foreach (var searchAgainst in research)
                {
                    if (searchFor.Key.SimilarTo(searchAgainst[0]) && searchFor.Value == searchAgainst[2])
                    {
                        UIMap.PopulationAndProductionWindow.SelectResearchByCategory(searchFor.Value);
                        Sleeper.Sleep(500);
                        var res = UIMap.PopulationAndProductionWindow.ResearchTable.GetTable();

                        for (int i = 0; i < res.Count; i++)
                        {
                            if (res[i][0] == searchAgainst[0])
                            {
                                ResearchTechCommand(searchFor.Value, i, 0, -1);
                                Timeline.AddEvent(MessageCommands.PrintFeedback, string.Format("[AutoResearch] Successfully selected research {0}.", searchFor.Key));
                                if (totalScientists > 1)
                                    Timeline.AddEvent(BalanceResearch);
                                return true;
                            }
                        }

                        break;
                    }
                }
            }

            return SelectCheapestScience(research, scientists);
        }

        private bool SelectCheapestScience(List<string[]> research, List<string[]> scientists)
        {
            var firstCategoryWithScientist = scientists.FirstOrDefault(x => x[0] != "");
            if (firstCategoryWithScientist == null)
                return false;

            var researchInCategory = research.Where(x => x[0] != "" && x[2] == firstCategoryWithScientist[1]).ToList();
            if (!researchInCategory.Any())
                return false;

            var cheapestResearch = researchInCategory.MinBy(x => int.Parse(x[1]));

            ResearchTechCommand(cheapestResearch[2], int.Parse(cheapestResearch[3]), 0, -1);

            return true;
        }

        private void BalanceResearch(object sender, EventArgs e)
        {
            Sleeper.Sleep(500);
            var labs = UIMap.PopulationAndProductionWindow.CurrentResearchProject.GetTable()[0][2];
            var occupied = int.Parse(labs.Split('/')[0]);
            if (occupied >= 2)
            {
                UIMap.PopulationAndProductionWindow.CurrentResearchProject.ClickRow(0);
                for (int i = 0; i < occupied/2; i++)
                {
                    UIMap.PopulationAndProductionWindow.RemoveRL.Click();
                }
                Timeline.AddEvent(AutoResearch);
            }
        }
    }
}