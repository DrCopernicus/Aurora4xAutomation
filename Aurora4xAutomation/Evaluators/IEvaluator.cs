namespace Aurora4xAutomation.Evaluators
{
    public interface IEvaluator
    {
        void Execute();
        string Help { get; }
    }
}
