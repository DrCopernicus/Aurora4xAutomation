using Aurora4xAutomation.Command;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Common.Exceptions;
using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.Evaluators
{
    public class ReadDataEvaluator : UIEvaluator
    {
        public ReadDataEvaluator(string text, IUIMap uiMap)
            : base(text, uiMap)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 2)
                throw new CommandExecutionException(2, Parameters.Count, Text);

            if (Parameters[0] == "research")
                new OpenCommands(UIMap).OpenResearchCategory(Parameters[1]);
        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}
