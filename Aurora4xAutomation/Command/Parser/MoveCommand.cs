using System;

namespace Aurora4xAutomation.Command.Parser
{
    public class MoveCommand : CommandEvaluator
    {
        public MoveCommand(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        public override void Evaluate()
        {
            if (Parameters.Count != 4)
                throw new Exception(string.Format("Expected 4 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            InfrastructureCommands.MakeCivilianContract(Parameters[0],
                Parameters[2],
                int.Parse(Parameters[3]),
                true);

            InfrastructureCommands.MakeCivilianContract(Parameters[1],
                Parameters[2],
                int.Parse(Parameters[3]),
                false);
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
