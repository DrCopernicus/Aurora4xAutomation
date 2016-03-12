using System.Threading;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Command
{
    public class ResearchCommands
    {
        public void ResearchTechCommand(string category, int researchNum, int scientistNum, string labsNum)
        {
            _production.MakeActive();
            _production.SelectResearchTab();
            _production.SelectResearchByCategory(category);

            _production.SetShowMatchingScientistsOnly(true);
            Thread.Sleep(500);
            _production.SelectNthResearch(researchNum);
            _production.SelectNthScientist(scientistNum);
            _production.SetAllocatedLabs(labsNum);
            _production.CreateResearch();
            _console.MakeActive();
        }

        private readonly ConsoleWindow _console = new ConsoleWindow();
        private readonly PopulationAndProductionWindow _production = new PopulationAndProductionWindow();
    }
}