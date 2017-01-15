using System;

namespace Aurora4xAutomationClient.ClientUI.Terminal
{
    public class ConsoleInputListener
    {
        private IConsoleWriter _writer;

        public ConsoleInputListener(IConsoleWriter writer)
        {
            _writer = writer;
        }
        
        public void Listen()
        {
            var readCharacter = _writer.Read();
            if (readCharacter == '\b')
                BackspacePressed(this, EventArgs.Empty);
        }

        public event EventHandler BackspacePressed;

        protected virtual void OnBackspacePressed()
        {
            var handler = BackspacePressed;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}