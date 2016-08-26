using Aurora4xAutomation.Common.Converters;
using Aurora4xAutomation.Messages;
using NUnit.Framework;
using System;
using System.ComponentModel;

namespace Aurora4xAutomation.Tests.ConverterTests
{
    [TestFixture]
    public class MessageTypeConverterTests
    {
        [Test]
        public void ConvertsInfoTypeToString()
        {
            Assert.AreEqual("INFO", MessageTypeConverter.ToString(MessageType.Information));
        }

        [Test]
        public void ConvertsInfoStringToType()
        {
            Assert.AreEqual(MessageType.Information, MessageTypeConverter.ToType("INFO"));
        }

        [Test]
        public void ConvertsInfoTypeToStringAndBack()
        {
            Assert.AreEqual(MessageType.Information, MessageTypeConverter.ToType(MessageTypeConverter.ToString(MessageType.Information)));
        }

        [Test]
        public void ThrowsAfterAttemptingToConvertNonsensicalString()
        {
            Assert.Throws<ArgumentException>(() => MessageTypeConverter.ToType("nonsensical message"));
        }

        [Test]
        public void ThrowsAfterAttemptingToConvertNonsensicalType()
        {
            Assert.Throws<InvalidEnumArgumentException>(() => MessageTypeConverter.ToString((MessageType)10030238));
        }
    }
}
