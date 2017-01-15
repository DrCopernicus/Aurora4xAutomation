using Server.Common;

namespace Client.REST
{
    public interface IClientWrapper
    {
        string GetMessages(string uri, Args args = null);
        string Request(string command);
    }
}
