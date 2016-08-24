using System;
using Aurora4xAutomation.Command;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.Evaluators
{
    public class MoveEvaluator : UIEvaluator
    {
        public MoveEvaluator(string text, IUIMap uiMap)
            : base(text, uiMap)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 4)
                throw new Exception(string.Format("Expected 4 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            new InfrastructureCommands(UIMap).MakeCivilianContract(Parameters[0],
                Parameters[2],
                int.Parse(Parameters[3]),
                true);

            new InfrastructureCommands(UIMap).MakeCivilianContract(Parameters[1],
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
