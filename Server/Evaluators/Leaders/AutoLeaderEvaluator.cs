using Server.Evaluators.Helpers;
using Server.IO;
using System;

namespace Server.Evaluators.Leaders
{
    public class AutoLeaderEvaluator : UIEvaluator
    {
        public AutoLeaderEvaluator(string text, IUIMap uiMap) : base(text, uiMap)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 1)
                throw new Exception(string.Format("Expected 1 parameter, got {0} in function name {1}.",
                    Parameters.Count, Text));

            UIMap.Leaders.MakeActive();
            UIMap.Leaders.SetAutomatedAssignments(ParameterTextParser.ReadBoolean(Parameters[0]));
        }

        public override string Help
        {
            get { throw new System.NotImplementedException(); }
        }
    }
}