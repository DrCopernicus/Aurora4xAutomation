using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Evaluators
{
    public abstract class SettingsEvaluator : Evaluator
    {
        public SettingsEvaluator(string text, ISettingsStore settings)
            : base(text)
        {
            Settings = settings;
        }

        protected ISettingsStore Settings { get; set; }
    }
}
