﻿using Client.Listeners;
using Client.REST;
using Common;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Tests.ClientTests
{
    [TestFixture]
    public class ServerMessageRetrieverTests
    {
        [Test]
        public void GetsOneMessage()
        {
            var client = Substitute.For<IClientWrapper>();
            client.GetMessages("/messages", Arg.Any<Args>()).Returns("this is one message");
            client.GetMessages("/lastmessage", Arg.Any<Args>()).Returns("0");
            
            var serverMessagePrinter = new ServerMessageRetriever(client);
            Assert.AreEqual(1, serverMessagePrinter.GetNewMessages().Count);
        }

        [Test]
        public void GetsFiveMessages()
        {
            var client = Substitute.For<IClientWrapper>();
            client.GetMessages("/messages", Arg.Any<Args>()).Returns("one\ntwo\nthree\nfour\nfive");
            client.GetMessages("/lastmessage", Arg.Any<Args>()).Returns("0");

            var serverMessagePrinter = new ServerMessageRetriever(client);
            Assert.AreEqual(5, serverMessagePrinter.GetNewMessages().Count);
        }
    }
}
