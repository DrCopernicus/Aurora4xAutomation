using NUnit.Framework;
using Tests.ScriptFramework;

namespace Tests.Tests.ScriptFrameworkTests
{
    [TestFixture]
    public class ScriptableStateTests
    {
        [Test]
        public void AcceptsAcceptableInput()
        {
            var state = new ScriptableState();
            state.Screen = null;
            state.RegisterClick(0, 0, 10, 10, new ScriptableState());
        }
    }
}
