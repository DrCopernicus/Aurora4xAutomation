using System.Collections.Generic;
using Client.ClientUI.Client;
using Client.ClientUI.Terminal;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Tests.ClientTests
{
    [TestFixture]
    public class ClientConsoleTests
    {
        [Test]
        public void WritesCharacterToTerminalsCurrentLine()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var formatter = Substitute.For<IConsoleFormatter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, formatter, client);
            console.WriteToCurrentLine("a");

            terminal.Received(1).AppendToCurrentLine('a');
        }

        [Test]
        public void WritesStringToTerminalsBuffer()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var formatter = Substitute.For<IConsoleFormatter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, formatter, client);
            console.WriteToBuffer("a");

            terminal.Received(1).WriteLine("a", Arg.Any<TerminalStyle>());
        }

        [Test]
        public void WritesBackspaceToCurrentLine()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var formatter = Substitute.For<IConsoleFormatter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, formatter, client);
            console.WriteToCurrentLine("\b");

            terminal.Received(1).Backspace();
        }

        [Test]
        public void WritesLongStringToCurrentLine()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var formatter = Substitute.For<IConsoleFormatter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, formatter, client);
            console.WriteToCurrentLine("hello");

            terminal.Received(5).AppendToCurrentLine(Arg.Any<char>());
        }

        [Test]
        public void WritesLongStringToCurrentLineWithBackspaces()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var formatter = Substitute.For<IConsoleFormatter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, formatter, client);
            console.WriteToCurrentLine("ha\belloo\b");

            terminal.Received(7).AppendToCurrentLine(Arg.Any<char>());
            terminal.Received(2).Backspace();
        }

        [Test]
        public void PushesCurrentLineIntoBufferOnNewline()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var formatter = Substitute.For<IConsoleFormatter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, formatter, client);
            console.WriteToCurrentLine("\n");

            terminal.Received(1).WriteCurrentLine(TerminalStyle.Command);
        }

        [Test]
        public void DoesNotModifyTerminalOutputWhenNormalCharactersTyped()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var formatter = Substitute.For<IConsoleFormatter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, formatter, client);

            formatter.ClearReceivedCalls();

            console.WriteToCurrentLine("h");

            formatter.Received(0).WriteLine(Arg.Any<TerminalMessage>());
            formatter.Received(0).Backspace();
        }

        [Test]
        public void ModifiesTerminalOutputWhenBackspaceIsPressed()
        {
            var terminal = Substitute.For<ITerminal>();
            terminal.GetBuffer().Returns(new List<TerminalMessage>());
            var formatter = Substitute.For<IConsoleFormatter>();
            var client = Substitute.For<IClientWrapper>();
            var console = new ClientConsole(terminal, formatter, client);
            console.WriteToCurrentLine("h\b");

            formatter.Received(1).Backspace();
        }
    }
}
