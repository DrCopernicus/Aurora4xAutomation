using Server.IO;

namespace Server.Evaluators
{
    public abstract class UIEvaluator : Evaluator
    {
        protected UIEvaluator(string text, IUIMap uiMap) : base(text)
        {
            UIMap = uiMap;
        }

        protected IUIMap UIMap { get; set; }
    }
}
