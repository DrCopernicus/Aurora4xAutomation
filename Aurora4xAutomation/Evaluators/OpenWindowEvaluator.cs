using Aurora4xAutomation.Command;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.Evaluators
{
    public class OpenWindowEvaluator : Evaluator
    {
        public OpenWindowEvaluator(string text)
            : base(text)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new CommandExecutionException(1, Parameters.Count, Text);

            if (Parameters[0] == "r")
                OpenCommands.OpenResearch();

            else if (Parameters[0] == "ship")
                OpenCommands.OpenShipyard();

            else if (Parameters[0] == "tg")
                OpenCommands.OpenTaskGroup();
        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
