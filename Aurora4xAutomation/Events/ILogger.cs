using System;

namespace Aurora4xAutomation.Events
{
    public interface ILogger
    {
        void Handle(Exception e);
    }
}
