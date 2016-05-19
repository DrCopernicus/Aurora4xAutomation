using System;
using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class ContractEvaluator : Evaluator
    {
        public ContractEvaluator(string text, CommandEvaluatorType type)
            : base(text, type)
        {
        }

        public override void Evaluate()
        {
            if (Parameters.Count != 4)
                throw new Exception(string.Format("Expected 4 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            if (Parameters[3] != "s" && Parameters[3] != "d" && Parameters[3] != "supply" && Parameters[3] != "demand")
                throw new CommandInvalidParameterException(4, "Expected either s(upply) or d(emand).");

            InfrastructureCommands.MakeCivilianContract(Parameters[0],
                Parameters[1],
                int.Parse(Parameters[2]),
                Parameters[3] == "s" || Parameters[3] == "supply");
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
