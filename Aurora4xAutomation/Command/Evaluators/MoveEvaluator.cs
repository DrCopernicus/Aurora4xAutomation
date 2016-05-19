using System;
using Aurora4xAutomation.Command.Parser;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class MoveEvaluator : Evaluator
    {
        public MoveEvaluator(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        protected override void Evaluate()
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
