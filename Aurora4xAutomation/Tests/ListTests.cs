using Aurora4xAutomation.Common;
using NUnit.Framework;

namespace Aurora4xAutomation.Tests
{
    [TestFixture]
    public class ListTests
    {
        [Test]
        public void TestSubsetExtension()
        {
            var list = new[] {"a", "b", "c", "d", "e"};
            var sublist = list.Subset(1, 3);
            Assert.IsTrue(sublist.Length == 3);
            Assert.IsTrue(sublist[0] == "b");
            Assert.IsTrue(sublist[1] == "c");
            Assert.IsTrue(sublist[2] == "d");
        }
    }
}
