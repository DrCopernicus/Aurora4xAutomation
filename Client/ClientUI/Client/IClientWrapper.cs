using Server.Common;

namespace Client.ClientUI.Client
{
    public interface IClientWrapper
    {
        string GetMessages(string uri, Args args = null);
        string Request(string command);
    }
}
