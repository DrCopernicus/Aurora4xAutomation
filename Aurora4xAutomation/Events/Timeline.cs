using System;
using System.Collections.Generic;
using System.Linq;
using Aurora4xAutomation.UI;

namespace Aurora4xAutomation.Events
{
    public static class Timeline
    {
        public static List<AuroraEvent> Events = new List<AuroraEvent>();

        public static AuroraEvent NextActiveEvent
        {
            get
            {
                var ev = Events.FirstOrDefault(x => x.Time <= new Time(UIMap.SystemMap.GetTime()));

                if (ev == null)
                    return null;

                Events.Remove(ev);

                return ev;
            }
        }

        public static void AddEvent(EventHandler action, string args = "", Time time = null)
        {
            var ev = new AuroraEvent(time ?? new Time(), action) { Args = args };
            Events.Add(ev);
        }

        public static void AddEvent(Action action, string args = "", Time time = null)
        {
            var ev = new AuroraEvent(time ?? new Time(), (sender, eventArgs) => action()) { Args = args };
            Events.Add(ev);
        }
    }
}
