namespace Client.Terminal
{
    public interface IConsoleFormatter
    {
        void WriteLine(TerminalMessage message);
        void Backspace();
        string ReadLine();
        int Read();
        void RewriteConsole(ITerminal terminal);
    }
}