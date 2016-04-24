using Aurora4xAutomation.Common;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command.Parser
{
    public class ReadDataCommand : CommandEvaluator
    {
        public ReadDataCommand(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        public override void Execute()
        {
            if (Parameters.Count != 2)
                throw new CommandExecutionException(2, Parameters.Count, Text);

            if (Parameters[0] == "research")
                Timeline.AddEvent(OpenCommands.OpenResearchCategory, Parameters[1]);
        }
    }
}
