using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class OpenWindowEvaluator : Evaluator
    {
        public OpenWindowEvaluator(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new CommandExecutionException(1, Parameters.Count, Text);

            if (Parameters[0] == "r")
                Timeline.AddEvent(OpenCommands.OpenResearch);

            else if (Parameters[0] == "ship")
                Timeline.AddEvent(OpenCommands.OpenShipyard);

            else if (Parameters[0] == "tg")
                Timeline.AddEvent(OpenCommands.OpenTaskGroup);
        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
