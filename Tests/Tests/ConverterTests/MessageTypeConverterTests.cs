using System;
using System.ComponentModel;
using NUnit.Framework;
using Server.Common.Converters;
using Server.Messages;

namespace Tests.Tests.ConverterTests
{
    [TestFixture]
    public class MessageTypeConverterTests
    {
        [Test]
        public void ConvertsInformationTypeToString()
        {
            Assert.AreEqual("INFO", MessageTypeConverter.ToString(MessageType.Information));
        }

        [Test]
        public void ConvertsWarningTypeToString()
        {
            Assert.AreEqual("WARN", MessageTypeConverter.ToString(MessageType.Warning));
        }

        [Test]
        public void ConvertsErrorTypeToString()
        {
            Assert.AreEqual("CRIT", MessageTypeConverter.ToString(MessageType.Error));
        }

        [Test]
        public void ConvertsDebugTypeToString()
        {
            Assert.AreEqual("DBUG", MessageTypeConverter.ToString(MessageType.Debug));
        }

        [Test]
        public void ConvertsINFOToType()
        {
            Assert.AreEqual(MessageType.Information, MessageTypeConverter.ToType("INFO"));
        }

        [Test]
        public void ConvertsDBUGToType()
        {
            Assert.AreEqual(MessageType.Debug, MessageTypeConverter.ToType("DBUG"));
        }

        [Test]
        public void ConvertsCRITToType()
        {
            Assert.AreEqual(MessageType.Error, MessageTypeConverter.ToType("CRIT"));
        }

        [Test]
        public void ConvertsWARNToType()
        {
            Assert.AreEqual(MessageType.Warning, MessageTypeConverter.ToType("WARN"));
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
