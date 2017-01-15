using Server.Evaluators;

namespace Server.Events
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
