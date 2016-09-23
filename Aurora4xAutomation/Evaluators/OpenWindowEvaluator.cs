using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.Evaluators
{
    public class OpenWindowEvaluator : UIEvaluator
    {
        public OpenWindowEvaluator(string text, IUIMap uiMap)
            : base(text, uiMap)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new CommandExecutionException(1, Parameters.Count, Text);

            switch (Parameters[0])
            {
                case "r":
                    UIMap.PopulationAndProduction.MakeActive();
                    UIMap.PopulationAndProduction.SelectResearchTab();
                    break;
                case "ship":
                    UIMap.PopulationAndProduction.MakeActive();
                    UIMap.PopulationAndProduction.SelectManageShipyards();
                    break;
                case "tg":
                    UIMap.TaskGroups.MakeActive();
                    break;
            }
        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
