namespace Aurora4xAutomationClient.ClientUI.Terminal
{
    public struct TerminalMessage
    {
        public TerminalStyle Style;
        public string Message;

        public TerminalMessage(string message, TerminalStyle style = TerminalStyle.Default)
        {
            Style = style;
            Message = message;
        }
    }

    public enum TerminalStyle
    {
        Default, Error, Warning, Command
    }
}