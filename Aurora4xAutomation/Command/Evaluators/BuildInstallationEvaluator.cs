using System;
using Aurora4xAutomation.Command.Parser;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class BuildInstallationEvaluator : Evaluator
    {
        public BuildInstallationEvaluator(string text, CommandEvaluatorType type)
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
