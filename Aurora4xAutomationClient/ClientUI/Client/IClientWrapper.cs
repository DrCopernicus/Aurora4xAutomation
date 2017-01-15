using Aurora4xAutomation.Common;

namespace Aurora4xAutomationClient.ClientUI
{
    public interface IClientWrapper
    {
        string SendRequest(string uri, Args args = null);
    }
}
