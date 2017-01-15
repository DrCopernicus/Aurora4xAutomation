using Client.ClientUI.Terminal;
using NUnit.Framework;

namespace Tests.Tests.ClientTests
{
    [TestFixture]
    public class ClientTerminalTests
    {
        [Test]
        public void StartsWithEmptyBuffer()
        {
            var terminal = new ClientTerminal();

            Assert.AreEqual(0, terminal.GetBuffer().Count);
        }

        [Test]
        public void WritesMessageToBuffer()
        {
            var terminal = new ClientTerminal();
            terminal.WriteLine("hello");

            Assert.AreEqual(1, terminal.GetBuffer().Count);
            Assert.AreEqual("hello", terminal.GetBuffer()[0].Message);
        }

        [Test]
        public void WritesTwoMessagesToBuffer()
        {
            var terminal = new ClientTerminal();
            terminal.WriteLine("hello");
            terminal.WriteLine("world");

            Assert.AreEqual(2, terminal.GetBuffer().Count);
            Assert.AreEqual("hello", terminal.GetBuffer()[0].Message);
            Assert.AreEqual("world", terminal.GetBuffer()[1].Message);
        }

        [Test]
        public void StartsWithEmptyCurrentLine()
        {
            var terminal = new ClientTerminal();

            Assert.AreEqual("", terminal.GetCurrentLine());
        }

        [Test]
        public void BufferEmptyAfterAppendingToCurrentLine()
        {
            var terminal = new ClientTerminal();
            terminal.AppendToCurrentLine("hello");

            Assert.AreEqual(0, terminal.GetBuffer().Count);
        }

        [Test]
        public void AppendsTextToCurrentLine()
        {
            var terminal = new ClientTerminal();
            terminal.AppendToCurrentLine("hello");

            Assert.AreEqual("hello", terminal.GetCurrentLine());
        }

        [Test]
        public void AppendsTextToCurrentLineTwice()
        {
            var terminal = new ClientTerminal();
            terminal.AppendToCurrentLine("hello");
            terminal.AppendToCurrentLine("world");

            Assert.AreEqual("helloworld", terminal.GetCurrentLine());
        }

        [Test]
        public void AppendsSingleCharacterToString()
        {
            var terminal = new ClientTerminal();
            terminal.AppendToCurrentLine('h');

            Assert.AreEqual("h", terminal.GetCurrentLine());
        }

        [Test]
        public void WritesCurrentLineIntoBuffer()
        {
            var terminal = new ClientTerminal();
            terminal.AppendToCurrentLine("hello");
            terminal.WriteCurrentLine(TerminalStyle.Default);

            Assert.AreEqual(1, terminal.GetBuffer().Count);
            Assert.AreEqual("hello", terminal.GetBuffer()[0].Message);
        }

        [Test]
        public void WritesCurrentLineIntoBufferWithCommandStyle()
        {
            var terminal = new ClientTerminal();
            terminal.AppendToCurrentLine("hello");
            terminal.WriteCurrentLine(TerminalStyle.Command);

            Assert.AreEqual(1, terminal.GetBuffer().Count);
            Assert.AreEqual("hello", terminal.GetBuffer()[0].Message);
            Assert.AreEqual(TerminalStyle.Command, terminal.GetBuffer()[0].Style);
        }

        [Test]
        public void CurrentLineIsEmptyAfterWritingIntoBuffer()
        {
            var terminal = new ClientTerminal();
            terminal.AppendToCurrentLine("hello");
            terminal.WriteCurrentLine(TerminalStyle.Default);

            Assert.AreEqual("", terminal.GetCurrentLine());
        }

        [Test]
        public void BackspaceRemovesOneCharacterFromCurrentLine()
        {
            var terminal = new ClientTerminal();
            terminal.AppendToCurrentLine("world");
            terminal.Backspace();

            Assert.AreEqual("worl", terminal.GetCurrentLine());
        }

        [Test]
        public void BackspaceRemovesNothingOnEmptyCurrentLine()
        {
            var terminal = new ClientTerminal();
            terminal.Backspace();

            Assert.AreEqual("", terminal.GetCurrentLine());
        }
    }
}
