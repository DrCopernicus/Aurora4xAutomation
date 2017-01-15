using System;
using Server.Command;
using Server.IO;

namespace Server.Evaluators
{
    public class OpenPopulationEvaluator : UIEvaluator
    {
        public OpenPopulationEvaluator(string text, IUIMap uiMap)
            : base(text, uiMap)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameter, got {0} in function name {1}.",
                    Parameters.Count, Text));

            new OpenCommands(UIMap).SelectColony(Parameters[0]);
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
