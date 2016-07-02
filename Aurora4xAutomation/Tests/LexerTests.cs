﻿using System;
using Aurora4xAutomation.Command.Parser;
using NUnit.Framework;

namespace Aurora4xAutomation.Tests
{
    [TestFixture]
    public class LexerTests
    {
        [Test]
        public void TestNonsensicalInput()
        {
            Assert.Throws(typeof(Exception), () => CommandLexer.Lex("awdh3ukhij 3lij 3lij5 la j3il li5 (a24 **248 **@&( kjahkuh"));
        }

        [Test]
        public void TestSimpleAdvanceCommand()
        {
            var command = CommandLexer.Lex("adv go");
            Assert.AreEqual("adv", command.Text);
            Assert.AreEqual("go", command.Body.Text);
            Assert.AreEqual(null, command.Body.Next);
            Assert.AreEqual(null, command.Next);
        }

        [Test]
        public void TestAdvanceMoreThanMaxParameters()
        {
            var command = CommandLexer.Lex("adv go go go go go!!!!!");
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
            var command = CommandLexer.Lex("adv go ; adv go");
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
            var command = CommandLexer.Lex("( 15s ) => { adv go } ");
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
            var command = CommandLexer.Lex("( 15s ) => { ( 8s ) => { ( 729689s ) => { adv go } } } ");
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