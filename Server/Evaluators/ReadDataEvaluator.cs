using Server.Command;
using Server.Common.Exceptions;
using Server.IO;

namespace Server.Evaluators
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
