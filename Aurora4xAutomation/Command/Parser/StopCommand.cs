using System;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command.Parser
{
    public class StopCommand : CommandEvaluator
    {
        public StopCommand(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        public override void Evaluate()
        {
            if (Parameters.Count != 0)
                throw new Exception(string.Format("Expected 0 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            Timeline.AddEvent(SettingsCommands.Stop);
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
