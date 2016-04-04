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
            _production.MakeActive();
            _production.SelectResearchTab();
            _production.SelectResearchByCategory(category);

            _production.SetShowMatchingScientistsOnly(true);
            Thread.Sleep(500);
            _production.SelectNthResearch(researchNum);
            _production.SelectNthScientist(scientistNum);
            if (labsNum != -1)
                _production.SetAllocatedLabs(labsNum + "");
            _production.CreateResearch();
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
            _production.MakeActive();
            _production.SelectResearchTab();

            _production.SetShowMatchingScientistsOnly(true);
            Thread.Sleep(500);

            while (int.Parse(_production.AvailableLabs.Text) > 0)
            {
                var scientists = new List<string[]>();
                var research = new List<string[]>();

                _production.SelectBiology();
                Thread.Sleep(500);
                research.AddRange(_production.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "bg" }));
                scientists.AddRange(_production.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "bg" }));
                _production.SelectConstruction();
                Thread.Sleep(500);
                research.AddRange(_production.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "cp" }));
                scientists.AddRange(_production.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "cp" }));
                _production.SelectDefensive();
                Thread.Sleep(500);
                research.AddRange(_production.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "ds" }));
                scientists.AddRange(_production.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "ds" }));
                _production.SelectEnergy();
                Thread.Sleep(500);
                research.AddRange(_production.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "ew" }));
                scientists.AddRange(_production.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "ew" }));
                _production.SelectLogistics();
                Thread.Sleep(500);
                research.AddRange(_production.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "lg" }));
                scientists.AddRange(_production.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "lg" }));
                _production.SelectMissiles();
                Thread.Sleep(500);
                research.AddRange(_production.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "mk" }));
                scientists.AddRange(_production.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "mk" }));
                _production.SelectPower();
                Thread.Sleep(500);
                research.AddRange(_production.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "pp" }));
                scientists.AddRange(_production.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "pp" }));
                _production.SelectSensors();
                Thread.Sleep(500);
                research.AddRange(_production.ResearchTable.GetTable().Select(x => new[] { x[0], x[1], "sf" }));
                scientists.AddRange(_production.AvailableScientistsTable.GetTable().Select(x => new[] { x[0], "sf" }));

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
                        _production.SelectResearchByCategory(searchFor.Value);
                        Thread.Sleep(500);
                        var res = _production.ResearchTable.GetTable();

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

        private readonly PopulationAndProductionWindow _production = new PopulationAndProductionWindow();
    }
}