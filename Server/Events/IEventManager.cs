
using Server.Evaluators;

namespace Server.Events
{
    public interface IEventManager
    {
        void AddEvent(IEvaluator evaluator, Time time);
        void Begin(ILogger logger);
        void Stop();
    }
}
