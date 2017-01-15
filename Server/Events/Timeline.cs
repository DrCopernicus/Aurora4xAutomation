using System.Collections.Generic;
using System.Linq;
using Server.Evaluators;

namespace Server.Events
{
    public class Timeline
    {
        public List<AuroraEvent> Events = new List<AuroraEvent>();
        
        public IEvaluator PopNextActiveEvent(Time time)
        {
            lock (_lock)
            {
                var ev = Events.OrderBy(evaluator => evaluator.Time).FirstOrDefault(x => x.Time <= time);

                if (ev == null)
                    return null;

                Events.Remove(ev);

                return ev.Evaluator;
            }
        }

        public void AddEvent(IEvaluator evaluator, Time time = null)
        {
            lock (_lock)
            {
                Events.Add(new AuroraEvent(time ?? new Time(), evaluator));
            }
        }

        private readonly object _lock = new object();
    }
}
