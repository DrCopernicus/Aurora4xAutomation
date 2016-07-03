using System;
using Aurora4xAutomation.Command.Evaluators;

namespace Aurora4xAutomation.Events
{
    public class AuroraEvent
    {
        public AuroraEvent()
        {

        }

        public AuroraEvent(Time time, Evaluator evaluator)
        {
            Evaluator = evaluator;
            Time = time;
        }

        public Time Time;
        public Evaluator Evaluator;
    }
}
