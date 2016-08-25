using Aurora4xAutomation.Messages;
using NUnit.Framework;

namespace Aurora4xAutomation.Tests.Messages
{
    [TestFixture]
    public class MessageManagerTests
    {
        [Test]
        public void IncrementsIdCounterAfterFirstMessage()
        {
            var messageManager = new MessageManager();

            messageManager.AddMessage("hello");
            Assert.AreEqual(0, messageManager.GetLastId());
        }

        [Test]
        public void GetsMessageAfterAdded()
        {
            var messageManager = new MessageManager();

            messageManager.AddMessage("hello");
            var messages = messageManager.GetMessagesAfterId(-1, 0);

            Assert.AreEqual(1, messages.Count);
        }

        [Test]
        public void GetsMessagesAfterTenAdded()
        {
            var messageManager = new MessageManager();

            for (var i = 0; i < 10; i++)
                messageManager.AddMessage("hello");

            var messages = messageManager.GetMessagesAfterId(-1, 10);

            Assert.AreEqual(10, messages.Count);
        }
    }
}
