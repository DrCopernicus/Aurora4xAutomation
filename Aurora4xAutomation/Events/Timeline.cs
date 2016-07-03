using System.Collections.Generic;
using System.Linq;
using Aurora4xAutomation.Command.Evaluators;

namespace Aurora4xAutomation.Events
{
    public static class Timeline
    {
        public static List<AuroraEvent> Events = new List<AuroraEvent>();
        
        public static Evaluator PopNextActiveEvent(Time time)
        {
            var ev = Events.OrderBy(evaluator => evaluator.Time).FirstOrDefault(x => x.Time <= time);

            if (ev == null)
                return null;

            Events.Remove(ev);

            return ev.Evaluator;
        }

        public static void AddEvent(Evaluator evaluator, Time time = null)
        {
            Events.Add(new AuroraEvent(time ?? new Time(), evaluator));
        }
    }
}
