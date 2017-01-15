using NUnit.Framework;
using Server.Messages;

namespace Aurora4xAutomationTests.Tests.Messages
{
    [TestFixture]
    public class MessageManagerTests
    {
        [Test]
        public void EmptyMessageListInitialMessageIdIsNegativeOne()
        {
            var messageManager = new MessageManager();

            Assert.AreEqual(-1, messageManager.GetLastId());
        }

        [Test]
        public void IncrementsIdCounterAfterFirstMessage()
        {
            var messageManager = new MessageManager();

            messageManager.AddMessage(MessageType.Information, "hello");

            Assert.AreEqual(0, messageManager.GetLastId());
        }

        [Test]
        public void IncrementsIdCounterAfterTenMessages()
        {
            var messageManager = new MessageManager();

            for (var i = 0; i < 10; i++)
                messageManager.AddMessage(MessageType.Information, "hello");

            Assert.AreEqual(9, messageManager.GetLastId());
        }

        [Test]
        public void GetsMessageAfterAdded()
        {
            var messageManager = new MessageManager();

            messageManager.AddMessage(MessageType.Information, "hello");
            var messages = messageManager.GetMessagesAfterId(-1, 0);

            Assert.AreEqual(1, messages.Count);
        }

        [Test]
        public void GetsMessagesAfterTenAdded()
        {
            var messageManager = new MessageManager();

            for (var i = 0; i < 10; i++)
                messageManager.AddMessage(MessageType.Information, "hello");

            var messages = messageManager.GetMessagesAfterId(-1, 10);

            Assert.AreEqual(10, messages.Count);
        }

        [Test]
        public void GetsFirstFiveMessagesAfterTenAdded()
        {
            var messageManager = new MessageManager();

            for (var i = 0; i < 10; i++)
                messageManager.AddMessage(MessageType.Information, "hello");

            var messages = messageManager.GetMessagesAfterId(-1, 4);

            Assert.AreEqual(5, messages.Count);
        }

        [Test]
        public void GetsMiddleFiveMessagesAfterTenAdded()
        {
            var messageManager = new MessageManager();

            for (var i = 0; i < 10; i++)
                messageManager.AddMessage(MessageType.Information, "hello");

            var messages = messageManager.GetMessagesAfterId(2, 7);

            Assert.AreEqual(5, messages.Count);
        }

        [Test]
        public void GetsLastFiveMessagesAfterTenAdded()
        {
            var messageManager = new MessageManager();

            for (var i = 0; i < 10; i++)
                messageManager.AddMessage(MessageType.Information, "hello");

            var messages = messageManager.GetMessagesAfterId(4, 9);

            Assert.AreEqual(5, messages.Count);
        }

        [Test]
        public void AddsMessageTypeTagToStartOfMessage()
        {
            var messageManager = new MessageManager();
            messageManager.AddMessage(MessageType.Information, "hello");

            Assert.AreEqual("[INFO] hello", messageManager.GetMessagesAfterId(-1, 100)[0]);
        }

        [Test]
        public void AddsMessageWithLotsOfSpecialCharacters()
        {
            var messageManager = new MessageManager();
            messageManager.AddMessage(MessageType.Information, "lij ;rtgliu1234 :''''!  23897LKA@J*u  o8A @#%,__mn,+==|| auih      ALLIJliij \"\"ijIA LI--@#JILJ ");

            Assert.AreEqual("[INFO] lij ;rtgliu1234 :''''!  23897LKA@J*u  o8A @#%,__mn,+==|| auih      ALLIJliij \"\"ijIA LI--@#JILJ ", messageManager.GetMessagesAfterId(-1, 100)[0]);
        }
    }
}
