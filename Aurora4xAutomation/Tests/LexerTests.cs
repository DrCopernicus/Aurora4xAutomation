using System;
using Aurora4xAutomation.Command.Parser;
using Aurora4xAutomation.IO;
using Aurora4xAutomation.IO.UI.Windows;
using Aurora4xAutomation.Settings;
using NUnit.Framework;

namespace Aurora4xAutomation.Tests
{
    [TestFixture]
    public class LexerTests
    {
        private class EmptyUIMap : IUIMap
        {
            public BaseAuroraWindow BaseAuroraWindow { get; private set; }
            public EventWindow EventWindow { get; private set; }
            public CommandersWindow Leaders { get; private set; }
            public SystemMapWindow SystemMap { get; private set; }
            public TaskGroupsWindow TaskGroups { get; private set; }
            public PopulationAndProductionWindow PopulationAndProductionWindow { get; private set; }
        }

        [Test]
        public void TestNonsensicalInput()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStore());
            Assert.Throws(typeof(Exception), () => lexer.Lex("awdh3ukhij 3lij 3lij5 la j3il li5 (a24 **248 **@&( kjahkuh"));
        }

        [Test]
        public void TestSimpleAdvanceCommand()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStore());
            var command = lexer.Lex("adv go");
            Assert.AreEqual("adv", command.Text);
            Assert.AreEqual("go", command.Body.Text);
            Assert.AreEqual(null, command.Body.Next);
            Assert.AreEqual(null, command.Next);
        }

        [Test]
        public void TestAdvanceMoreThanMaxParameters()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStore());
            var command = lexer.Lex("adv go go go go go!!!!!");
            Assert.AreEqual("adv", command.Text);
            Assert.AreEqual("go", command.Body.Text);
            Assert.AreEqual("go", command.Body.Next.Text);
            Assert.AreEqual("go", command.Body.Next.Next.Text);
            Assert.AreEqual("go", command.Body.Next.Next.Next.Text);
            Assert.AreEqual("go!!!!!", command.Body.Next.Next.Next.Next.Text);
            Assert.AreEqual(null, command.Body.Next.Next.Next.Next.Next);
            Assert.AreEqual(null, command.Next);
        }

        [Test]
        public void TestTwoAdvanceCommandsInARow()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStore());
            var command = lexer.Lex("adv go ; adv go");
            Assert.AreEqual("adv", command.Text);
            Assert.AreEqual("go", command.Body.Text);
            Assert.AreEqual("adv", command.Next.Text);
            Assert.AreEqual("go", command.Next.Body.Text);
            Assert.AreEqual(null, command.Next.Body.Next);
            Assert.AreEqual(null, command.Next.Next);
        }

        [Test]
        public void TestSimpleTimerCommand()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStore());
            var command = lexer.Lex("( 15s ) => { adv go } ");
            Assert.AreEqual("15s", command.Text);
            Assert.AreEqual("adv", command.Body.Text);
            Assert.AreEqual("go", command.Body.Body.Text);
            Assert.AreEqual(null, command.Body.Body.Next);
            Assert.AreEqual(null, command.Body.Next);
            Assert.AreEqual(null, command.Next);
        }

        [Test]
        public void TestNestedTimerCommand()
        {
            var lexer = new CommandLexer(new EmptyUIMap(), new SettingsStore());
            var command = lexer.Lex("( 15s ) => { ( 8s ) => { ( 729689s ) => { adv go } } } ");
            Assert.AreEqual("15s", command.Text);
            Assert.AreEqual("8s", command.Body.Text);
            Assert.AreEqual("729689s", command.Body.Body.Text);
            Assert.AreEqual("adv", command.Body.Body.Body.Text);
            Assert.AreEqual("go", command.Body.Body.Body.Body.Text);
            Assert.AreEqual(null, command.Body.Body.Body.Body.Next);
            Assert.AreEqual(null, command.Body.Body.Body.Next);
            Assert.AreEqual(null, command.Body.Body.Next);
            Assert.AreEqual(null, command.Body.Next);
            Assert.AreEqual(null, command.Next);
        }
    }
}
