using System;

namespace Aurora4xAutomation.Events
{
    public interface ILogger
    {
        void Error(Exception e);
        void Write(string message);
    }
}
