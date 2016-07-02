using System.Net;
using Aurora4xAutomation.Settings;
using Grapevine;
using Grapevine.Server;

namespace Aurora4xAutomation.REST
{
    public sealed class Resources : RESTResource
    {
        [RESTRoute(Method = HttpMethod.GET, PathInfo = @"^/$")]
        public void HandleAllGetRequests(HttpListenerContext context)
        {
            SendTextResponse(context, string.Format("Current status: {0}\nMessages: {1}\n{2}\n{3}", SettingsStore.StatusMessage, SettingsStore.ErrorMessage, SettingsStore.InterruptMessage, SettingsStore.FeedbackMessage));
        }
    }
}
