using Aurora4xAutomation.IO;

namespace Aurora4xAutomation.Evaluators
{
    public abstract class UIEvaluator : Evaluator
    {
        public UIEvaluator(string text, IUIMap uiMap) : base(text)
        {
            UIMap = uiMap;
        }

        protected IUIMap UIMap { get; set; }
    }
}
