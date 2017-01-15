namespace Aurora4xAutomationClient.ClientUI.Terminal
{
    public struct TerminalMessage
    {
        public TerminalColor Color;
        public string Message;
    }

    public enum TerminalColor
    {
        Default, Error, Warning
    }
}