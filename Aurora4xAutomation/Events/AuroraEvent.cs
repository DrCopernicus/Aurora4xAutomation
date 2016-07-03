using System;

namespace Aurora4xAutomation.Events
{
    public class AuroraEvent
    {
        public AuroraEvent()
        {

        }

        public AuroraEvent(Time time, EventHandler action)
        {
            EventHappened += action;
            Time = time;
        }

        public string Args;
        public Time Time;
        public event EventHandler EventHappened;

        public void Invoke()
        {
            if (EventHappened != null)
                EventHappened(this, new MessageEventArgs(Args));
        }
    }
}
