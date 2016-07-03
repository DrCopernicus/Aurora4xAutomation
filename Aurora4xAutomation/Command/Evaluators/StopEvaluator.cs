using System;
using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.Events;

namespace Aurora4xAutomation.Command.Evaluators
{
    public class StopEvaluator : Evaluator
    {
        public StopEvaluator(string text)
            : base(text)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 0)
                throw new Exception(string.Format("Expected 0 parameters, got {0} in function name {1}.",
                    Parameters.Count, Text));

            SettingsCommands.Stop();
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
