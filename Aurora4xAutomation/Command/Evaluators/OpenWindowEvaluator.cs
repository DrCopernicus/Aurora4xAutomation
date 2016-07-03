using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command.Evaluators
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
