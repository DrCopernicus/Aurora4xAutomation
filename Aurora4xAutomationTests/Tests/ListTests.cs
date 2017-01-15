using NUnit.Framework;
using Server.Common;

namespace Aurora4xAutomationTests.Tests
{
    [TestFixture]
    public class ListTests
    {
        [Test]
        public void TestSubsetExtension()
        {
            var list = new[] { "a", "b", "c", "d", "e" };
            var sublist = list.Subset(1, 3);
            Assert.AreEqual(3, sublist.Length);
            Assert.AreEqual("b", sublist[0]);
            Assert.AreEqual("c", sublist[1]);
            Assert.AreEqual("d", sublist[2]);
        }

        [Test]
        public void TestSubsetExtensionOnlyStart()
        {
            var list = new[] { "a", "b", "c", "d", "e" };
            var sublist = list.Subset(3);
            Assert.AreEqual(2, sublist.Length);
            Assert.AreEqual("d", sublist[0]);
            Assert.AreEqual("e", sublist[1]);
        }
    }
}
