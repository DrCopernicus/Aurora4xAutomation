using MoreLinq;
using Server.Common;
using Server.IO;
using Server.IO.UI.Display;
using Server.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.Command
{
    [Obsolete("Commands and related classes should be discontinued in favor of Evaluators, and in the case of duplicated functionality using compound evaluators.")]
    public class ResearchCommands
    {
        public ResearchCommands(IUIMap uiMap, SettingsStore settings)
        {
            UIMap = uiMap;
            Settings = settings;
        }

        public void FocusResearch(string category)
        {
            Settings.Research.Clear();
            foreach (var ban in Settings.ResearchFocuses[category])
                Settings.Research.Add(ban.Key, ban.Value);
        }

        public void AutoResearch(object sender, EventArgs e)
        {
            UIMap.PopulationAndProduction.MakeActive();
            UIMap.PopulationAndProduction.SelectResearchTab();

            UIMap.PopulationAndProduction.MatchingScientistsOnly.Select();
            StaticSleeper.Sleep(500);

            if (int.Parse(UIMap.PopulationAndProduction.AvailableLabs.Text) != 0)
            {
                var scientists = new List<string[]>();
                var research = new List<string[]>();

                UIMap.PopulationAndProduction.SelectBiology();
                StaticSleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProduction.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "bg", ""+index }));
                scientists.AddRange(UIMap.PopulationAndProduction.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "bg" }));
                UIMap.PopulationAndProduction.SelectConstruction();
                StaticSleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProduction.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "cp", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProduction.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "cp" }));
                UIMap.PopulationAndProduction.SelectDefensive();
                StaticSleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProduction.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "ds", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProduction.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "ds" }));
                UIMap.PopulationAndProduction.SelectEnergy();
                StaticSleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProduction.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "ew", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProduction.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "ew" }));
                UIMap.PopulationAndProduction.SelectLogistics();
                StaticSleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProduction.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "lg", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProduction.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "lg" }));
                UIMap.PopulationAndProduction.SelectMissiles();
                StaticSleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProduction.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "mk", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProduction.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "mk" }));
                UIMap.PopulationAndProduction.SelectPower();
                StaticSleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProduction.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "pp", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProduction.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "pp" }));
                UIMap.PopulationAndProduction.SelectSensors();
                StaticSleeper.Sleep(500);
                research.AddRange(UIMap.PopulationAndProduction.ResearchTable.GetTable().Select((x, index) => new[] { x[0], x[1], "sf", "" + index }));
                scientists.AddRange(UIMap.PopulationAndProduction.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "sf" }));

                if (SelectScience(research, scientists))
                    return;

                //new MessageCommands(Settings).PrintError("[AutoResearch] Failed to assign a new research project.");
            }
        }

        private bool SelectScience(List<string[]> research, List<string[]> scientists)
        {
            return SelectCheapScience(research, scientists, 2000)
                || SelectTargetedScience(research, scientists)
                || SelectCheapestScience(research, scientists)
                || AssignLabsToTop();
        }

        private bool SelectCheapScience(List<string[]> research, List<string[]> scientists, int maxCost)
        {
            foreach (var res in research.Where(x => x[0] != "" && int.Parse(x[1].Split('/')[0]) <= maxCost).OrderBy(x => int.Parse(x[1])))
            {
                var firstScientist = scientists.FirstOrDefault(x => x[0] != "" && x[1] == res[2]);
                if (firstScientist == null)
                    continue;

                //ResearchTechCommand(res[2], int.Parse(res[3]), 0, -1);
                return true;
            }

            return false;
        }

        private bool SelectTargetedScience(List<string[]> research, List<string[]> scientists)
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
                        UIMap.PopulationAndProduction.SelectResearchByCategory(searchFor.Value);
                        StaticSleeper.Sleep(500);
                        var res = UIMap.PopulationAndProduction.ResearchTable.GetTable();

                        for (int i = 0; i < res.Count; i++)
                        {
                            if (res[i][0] == searchAgainst[0])
                            {
                                //ResearchTechCommand(searchFor.Value, i, 0, -1);
                                //new MessageCommands(Settings).PrintFeedback(string.Format("[AutoResearch] Successfully selected research {0}.", searchFor.Key));
                                if (totalScientists > 1)
                                    throw new NotImplementedException();
                                    //Timeline.AddEvent(BalanceResearch);
                                return true;
                            }
                        }

                        break;
                    }
                }
            }
            return false;
        }

        private bool SelectCheapestScience(List<string[]> research, List<string[]> scientists)
        {
            var firstCategoryWithScientist = scientists.FirstOrDefault(x => x[0] != "");
            if (firstCategoryWithScientist == null)
                return false;

            var researchInCategory = research.Where(x => x[0] != "" && x[2] == firstCategoryWithScientist[1]).ToList();
            if (!researchInCategory.Any())
                return false;

            var cheapestResearch = researchInCategory.MinBy(x => int.Parse(x[1].Split('/')[0]));

            //ResearchTechCommand(cheapestResearch[2], int.Parse(cheapestResearch[3]), 0, -1);

            return true;
        }

        private bool AssignLabsToTop()
        {
            while (int.Parse(UIMap.PopulationAndProduction.AvailableLabs.Text) >= 0)
            {
                UIMap.PopulationAndProduction.CurrentResearchProject.ClickRow(0);
                UIMap.PopulationAndProduction.AddRL.Click();
                Screen.Dirty();
            }
            return true;
        }

        private void BalanceResearch(object sender, EventArgs e)
        {
            StaticSleeper.Sleep(500);
            var labs = UIMap.PopulationAndProduction.CurrentResearchProject.GetTable()[0][2];
            var occupied = int.Parse(labs.Split('/')[0]);
            if (occupied >= 2)
            {
                UIMap.PopulationAndProduction.CurrentResearchProject.ClickRow(0);
                for (int i = 0; i < occupied/2; i++)
                {
                    UIMap.PopulationAndProduction.RemoveRL.Click();
                }
                //Timeline.AddEvent(AutoResearch);
                throw new NotImplementedException();
            }
        }

        public void CheckNumberOfLabs(object sender, EventArgs e)
        {
            UIMap.Leaders.MakeActive();
            UIMap.Leaders.LeaderType.Text = "l";
            if (!UIMap.Leaders.Officiers.Children[0].Text.Contains("Scientist"))
            {
                //new MessageCommands(Settings).PrintError("[CheckNumberOfLabs] Could not find the Scientist row!");
            }
            else
            {
                var numScientists = int.Parse(UIMap.Leaders.Officiers.Children[0].Text.Split('(')[1].Split(')')[0]);
                UIMap.PopulationAndProduction.MakeActive();
                UIMap.PopulationAndProduction.Populations.Select("Earth");
                UIMap.PopulationAndProduction.SelectResearchTab();
                var numLabs = int.Parse(UIMap.PopulationAndProduction.NumberOfLabs.Text);
                if (numScientists*Settings.MinLabsPerScientist >= numLabs)
                    new InfrastructureCommands(UIMap).BuildInstallation("Earth", "lab", "1");
            }
            //Timeline.AddEvent(CheckNumberOfLabs, "", new Time(UIMap.SystemMap.GetTime()) + new Time(0, 0, SettingsStore.DaysPerLabsCheck, 0, 0, 0));
            //TODO
        }

        private IScreen Screen { get; set; }
        private IUIMap UIMap { get; set; }
        private SettingsStore Settings { get; set; }
    }
}