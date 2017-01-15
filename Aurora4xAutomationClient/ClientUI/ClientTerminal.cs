﻿using Aurora4xAutomationClient.ClientUI.Terminal;
using System.Collections.Generic;

namespace Aurora4xAutomationClient.ClientUI
{
    public class ClientTerminal : ITerminal
    {
        private List<TerminalMessage> _buffer;
        private string _currentLine;

        public ClientTerminal()
        {
            _buffer = new List<TerminalMessage>();
            _currentLine = "";
        }

        public void WriteLine(string message, TerminalColor color = TerminalColor.Default)
        {
            _buffer.Add(new TerminalMessage {Message = message, Color = color});
        }

        public List<TerminalMessage> GetBuffer()
        {
            return _buffer;
        }

        public string GetCurrentLine()
        {
            return _currentLine;
        }

        public void AppendToCurrentLine(string message)
        {
            _currentLine += message;
        }

        public void AppendToCurrentLine(char character)
        {
            _currentLine += character;
        }

        public void WriteCurrentLine()
        {
            _buffer.Add(new TerminalMessage {Message = _currentLine, Color = TerminalColor.Default});
            _currentLine = "";
        }

        public void Backspace()
        {
            if (_currentLine.Length > 0)
                _currentLine = _currentLine.Substring(0, _currentLine.Length - 1);
        }
    }
}