using System;

namespace Aurora4xAutomation.Events
{
    public enum AuroraEventType
    {
        NonStopping,
        Research,
        MineralsLocated,
        Unspecified
    }

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

        public AuroraEventType Type;
        public string Text;
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
