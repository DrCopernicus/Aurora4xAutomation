using Aurora4xAutomation.Command;
using Aurora4xAutomation.Common;

namespace Aurora4xAutomation.Evaluators
{
    public class ReadDataEvaluator : Evaluator
    {
        public ReadDataEvaluator(string text)
            : base(text)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 2)
                throw new CommandExecutionException(2, Parameters.Count, Text);

            if (Parameters[0] == "research")
                OpenCommands.OpenResearchCategory(Parameters[1]);
        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
