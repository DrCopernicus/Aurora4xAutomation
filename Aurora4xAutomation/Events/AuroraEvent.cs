using Aurora4xAutomation.Evaluators;

namespace Aurora4xAutomation.Events
{
    public class AuroraEvent
    {
        public AuroraEvent()
        {

        }

        public AuroraEvent(Time time, IEvaluator evaluator)
        {
            Evaluator = evaluator;
            Time = time;
        }

        public Time Time;
        public IEvaluator Evaluator;
    }
}
