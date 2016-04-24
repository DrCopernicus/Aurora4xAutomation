using Aurora4xAutomation.Common;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command.Parser
{
    public class OpenWindowCommand : CommandEvaluator
    {
        public OpenWindowCommand(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        public override void Execute()
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
    }
}
