using Aurora4xAutomationClient.Common.EArgs;
using System;

namespace Aurora4xAutomationClient.ClientUI.Listeners
{
    public interface IInputListener
    {
        event EventHandler<TerminalEventArgs> ReceivedText;
        void BeginListening();
    }
}