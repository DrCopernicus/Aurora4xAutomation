using System.Collections.Generic;

namespace Client.ClientUI.Terminal
{
    public interface ITerminal
    {
        void WriteLine(string message, TerminalStyle style);
        List<TerminalMessage> GetBuffer();
        string GetCurrentLine();
        void AppendToCurrentLine(string message);
        void AppendToCurrentLine(char character);
        void WriteCurrentLine(TerminalStyle style);
        void Backspace();
    }
}