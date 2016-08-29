
using Aurora4xAutomation.Evaluators;

namespace Aurora4xAutomation.Events
{
    public interface IEventManager
    {
        void AddEvent(IEvaluator evaluator, Time time);
        void Begin(ILogger logger);
        void Stop();
    }
}
