using Aurora4xAutomation.Common;

namespace Aurora4xAutomationClient.ClientUI.Client
{
    public interface IClientWrapper
    {
        string GetMessages(string uri, Args args = null);
        string Request(string command);
    }
}
