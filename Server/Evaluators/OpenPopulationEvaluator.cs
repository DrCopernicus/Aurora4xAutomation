using System;
using Server.Command;
using Server.Evaluators.Helpers;
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

            UIMap.PopulationAndProduction.MakeActive();
            UIMap.PopulationAndProduction.Populations.Select(Parameters[0]);
        }

        public override HelpText Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
