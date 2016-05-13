using System;

namespace Aurora4xAutomation.Command.Parser
{
    public class BuildInstallationCommand : CommandEvaluator
    {
        public BuildInstallationCommand(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        public override void Evaluate()
        {
            InfrastructureCommands.BuildInstallation(Parameters[0], Parameters[1], Parameters[2]);
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
