using Server.Evaluators.Helpers;

namespace Server.Evaluators
{
    public interface IEvaluator
    {
        void Execute();
        HelpText Help { get; }

        string Text { get; }
        IEvaluator Body { get; set; }
        IEvaluator Next { get; set; }

        CommandEvaluatorType GetEvaluatorType();
    }
}
