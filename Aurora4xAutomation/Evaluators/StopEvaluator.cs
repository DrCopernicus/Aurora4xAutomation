using System;
using Aurora4xAutomation.Common;
using Aurora4xAutomation.Common.Exceptions;
using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Evaluators
{
    public class StopEvaluator : SettingsEvaluator
    {
        public StopEvaluator(string text, ISettingsStore settings)
            : base(text, settings)
        {
        }

        protected override void Evaluate()
        {
            if (Parameters.Count != 0)
                throw new TooManyParametersException(0, Parameters.Count, Text);

            Settings.Stopped = true;
        }

        public override string Help
        {
            get { throw new NotImplementedException(); }
        }
    }
}
