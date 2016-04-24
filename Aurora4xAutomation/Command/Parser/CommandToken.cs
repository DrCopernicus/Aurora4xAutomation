namespace Aurora4xAutomation.Command.Parser
{
    public enum CommandTokenType
    {
        Text,
        LeftParenthesis,
        RightParenthesis,
        LeftBrace,
        RightBrace,
        Semicolon,
        Comma,
        Arrow,
        EOF
    }

    public class CommandToken
    {
        public CommandToken(string text, CommandTokenType type)
        {
            Text = text;
            Type = type;
        }
        
        public string Text { get; private set; }
        public CommandTokenType Type { get; private set; }
    }
}
