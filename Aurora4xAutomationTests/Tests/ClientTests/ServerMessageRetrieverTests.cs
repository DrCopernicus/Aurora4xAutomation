using Aurora4xAutomation.Common;
using Aurora4xAutomationClient.ClientUI;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Aurora4xAutomationTests.Tests.ClientTests
{
    [TestFixture]
    public class ServerMessageRetrieverTests
    {
        private class TestClientWrapper : IClientWrapper
        {
            public string SendRequest(string uri, Args args)
            {
                if (uri == "/messages")
                {
                    var returnedMessages = new List<string>();
                    for (var i = Convert.ToInt32(args["after"]) + 1; i <= Convert.ToInt32(args["upto"]); i++)
                        returnedMessages.Add(Messages[i]);
                    return string.Join("\n", returnedMessages);
                }
                if (uri == "/lastmessage")
                    return string.Format("{0}", Messages.Count - 1);
                return "";
            }

            public readonly List<string> Messages = new List<string>();
        }

        [Test]
        public void GetsOneMessage()
        {
            var clientWrapper = new TestClientWrapper();
            clientWrapper.Messages.Add("Message One");

            var serverMessagePrinter = new ServerMessageRetriever(clientWrapper);

            Assert.AreEqual(1, serverMessagePrinter.GetNewMessages().Count);
        }

        [Test]
        public void GetsFiveMessages()
        {
            var clientWrapper = new TestClientWrapper();
            clientWrapper.Messages.Add("Message One");
            clientWrapper.Messages.Add("Message Two");
            clientWrapper.Messages.Add("Message Three");
            clientWrapper.Messages.Add("Message Four");
            clientWrapper.Messages.Add("Message Five");

            var serverMessagePrinter = new ServerMessageRetriever(clientWrapper);

            Assert.AreEqual(5, serverMessagePrinter.GetNewMessages().Count);
        }

        [Test]
        public void GetsOneMessageThenGetsAnother()
        {
            var clientWrapper = new TestClientWrapper();
            clientWrapper.Messages.Add("Message One");

            var serverMessagePrinter = new ServerMessageRetriever(clientWrapper);

            Assert.AreEqual(1, serverMessagePrinter.GetNewMessages().Count);

            clientWrapper.Messages.Add("Message Two");

            Assert.AreEqual(1, serverMessagePrinter.GetNewMessages().Count);
        }
    }
}
