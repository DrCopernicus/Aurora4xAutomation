using NUnit.Framework;
using Tests.ScriptFramework.AcceptableInputs;
using Tests.ScriptFramework.ScriptedInputs;

namespace Tests.Tests.ScriptFrameworkTests
{
    [TestFixture]
    public class AcceptableKeysTests
    {
        [Test]
        public void AcceptsKeys()
        {
            var keys = new ScriptedKeys("e");
            var acceptableKeys = new AcceptableKeys("e");

            Assert.IsTrue(acceptableKeys.Accepts(keys));
        }

        [Test]
        public void AcceptsLongerKeys()
        {
            var keys = new ScriptedKeys("efg");
            var acceptableKeys = new AcceptableKeys("efg");

            Assert.IsTrue(acceptableKeys.Accepts(keys));
        }

        [Test]
        public void RejectsSubstring()
        {
            var keys = new ScriptedKeys("e");
            var acceptableKeys = new AcceptableKeys("efg");

            Assert.IsFalse(acceptableKeys.Accepts(keys));
        }

        [Test]
        public void RejectsWrongKey()
        {
            var keys = new ScriptedKeys("e");
            var acceptableKeys = new AcceptableKeys("a");

            Assert.IsFalse(acceptableKeys.Accepts(keys));
        }

        [Test]
        public void RejectsClick()
        {
            var click = new ScriptedClick(20, 45);
            var acceptableKeys = new AcceptableKeys("e");

            Assert.IsFalse(acceptableKeys.Accepts(click));
        }
    }
}
