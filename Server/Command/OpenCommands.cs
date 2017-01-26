using Server.IO;
using System;

namespace Server.Command
{
    [Obsolete("Commands and related classes should be discontinued in favor of Evaluators, and in the case of duplicated functionality using compound evaluators.")]
    public class OpenCommands
    {
        public OpenCommands(IUIMap uiMap)
        {
            UIMap = uiMap;
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

        

        private IUIMap UIMap { get; set; }
    }
}