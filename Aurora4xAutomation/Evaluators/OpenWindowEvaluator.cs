using Aurora4xAutomation.Command;
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

            if (Parameters[0] == "r")
                new OpenCommands(UIMap).OpenResearch();

            else if (Parameters[0] == "ship")
                new OpenCommands(UIMap).OpenShipyard();

            else if (Parameters[0] == "tg")
                new OpenCommands(UIMap).OpenTaskGroup();
        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
