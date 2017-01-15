using Aurora4xAutomationClient.ClientUI.Terminal;
using System.Collections.Generic;

namespace Aurora4xAutomationClient.ClientUI
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