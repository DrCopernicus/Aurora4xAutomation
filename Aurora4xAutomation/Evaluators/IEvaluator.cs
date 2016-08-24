namespace Aurora4xAutomation.Evaluators
{
    public interface IEvaluator
    {
        void Execute();
        string Help { get; }

        string Text { get; }
        IEvaluator Body { get; set; }
        IEvaluator Next { get; set; }

        CommandEvaluatorType GetEvaluatorType();
    }
}
