using NUnit.Framework;
using Server.Evaluators.Helpers;
using System;

namespace Tests.Tests.HelpTextTests
{
    [TestFixture]
    public class HelpTextExampleTests
    {
        [Test]
        public void CannotCreateHelpTextExampleWithoutAnEffect()
        {
            Assert.Throws<Exception>(() => new HelpTextExample("command"));
        }

        [Test]
        public void CreatesHelpTextExampleWithEffect()
        {
            var example = new HelpTextExample("command", "effect");

            Assert.AreEqual("command", example.Invokation);
            Assert.AreEqual("effect", example.Effect);
        }

        [Test]
        public void CreatesHelpTextExampleWithOneParameter()
        {
            var example = new HelpTextExample("destroy", "moon", "Destroys the moon.");

            Assert.AreEqual("destroy moon", example.Invokation);
            Assert.AreEqual("Destroys the moon.", example.Effect);
        }

        [Test]
        public void CreatesHelpTextExampleWithOneReferencedParameter()
        {
            var example = new HelpTextExample("destroy", "<planetary body>", "Destroys a given {0}.");

            Assert.AreEqual("destroy <planetary body>", example.Invokation);
            Assert.AreEqual("Destroys a given <planetary body>.", example.Effect);
        }
    }
}
