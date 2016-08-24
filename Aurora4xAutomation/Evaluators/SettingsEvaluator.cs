using Aurora4xAutomation.Settings;

namespace Aurora4xAutomation.Evaluators
{
    public abstract class SettingsEvaluator : Evaluator
    {
        public SettingsEvaluator(string text, SettingsStore settings)
            : base(text)
        {
            Settings = settings;
        }

        protected SettingsStore Settings { get; set; }
    }
}
