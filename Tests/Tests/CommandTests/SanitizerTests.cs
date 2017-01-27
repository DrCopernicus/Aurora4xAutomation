using Common;
using NUnit.Framework;
using Server.Command.Parser;

namespace Tests.Tests.CommandTests
{
    [TestFixture]
    public class SanitizerTests
    {
        [Test]
        public void DoesntChangePassableString()
        {
            var sanitizer = new Sanitizer();
            Assert.AreEqual("hello world", sanitizer.Sanitize("hello world"));
        }

        [Test]
        public void RemovesTerminals()
        {
            var sanitizer = new Sanitizer();
            Assert.AreEqual("hello world", sanitizer.Sanitize("hello\0 world"));
        }

        [Test]
        public void RemovesNewlines()
        {
            var sanitizer = new Sanitizer();
            Assert.AreEqual("hello world", sanitizer.Sanitize("hello\n world"));
        }
    }
}
