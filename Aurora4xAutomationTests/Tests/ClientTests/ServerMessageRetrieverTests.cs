﻿using Aurora4xAutomation.Common;
using Aurora4xAutomationClient.ClientUI.Client;
using Aurora4xAutomationClient.ClientUI.Listeners;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Aurora4xAutomationTests.Tests.ClientTests
{
    [TestFixture]
    public class ServerMessageRetrieverTests
    {
        [Test]
        public void GetsOneMessage()
        {
            var client = Substitute.For<IClientWrapper>();
            client.SendRequest("/messages", Arg.Any<Args>()).Returns("this is one message");
            client.SendRequest("/lastmessage", Arg.Any<Args>()).Returns("0");
            
            var serverMessagePrinter = new ServerMessageRetriever(client);
            Assert.AreEqual(1, serverMessagePrinter.GetNewMessages().Count);
        }

        [Test]
        public void GetsFiveMessages()
        {
            var client = Substitute.For<IClientWrapper>();
            client.SendRequest("/messages", Arg.Any<Args>()).Returns("one\ntwo\nthree\nfour\nfive");
            client.SendRequest("/lastmessage", Arg.Any<Args>()).Returns("0");

            var serverMessagePrinter = new ServerMessageRetriever(client);
            Assert.AreEqual(5, serverMessagePrinter.GetNewMessages().Count);
        }
    }
}
