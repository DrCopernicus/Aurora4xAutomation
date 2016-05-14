using System;

namespace Aurora4xAutomation.Command.Parser
{
    public class OpenPopulationCommand : CommandEvaluator
    {
        public OpenPopulationCommand(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        public override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameter, got {0} in function name {1}.",
                    Parameters.Count, Text));

            new OpenCommands().SelectColony(Parameters[0]);
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
