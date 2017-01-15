
namespace Aurora4xAutomation.Events
{
    public interface ILogger
    {
        void Error(string errorMessage, string stackTrace);
        void Write(string message);
    }
}
