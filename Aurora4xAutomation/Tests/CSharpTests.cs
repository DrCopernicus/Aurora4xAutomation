using NUnit.Framework;

namespace Aurora4xAutomation.Tests
{
    /// <summary>
    /// made this so i remember how C# handles a couple of things
    /// </summary>
    [TestFixture]
    public class CSharpTests
    {
        private class FeetHolder
        {
            public int NumberOfFeet { get; set; }
        }

        private abstract class Dog
        {
            public Dog(FeetHolder feet)
            {
                Feet = feet;
            }

            public FeetHolder Feet;
        }

        private class ActualDog : Dog
        {
            public ActualDog(FeetHolder feet) : base(feet)
            {
            }
        }

        [Test]
        public void FieldAsAReference()
        {
            var feet = new FeetHolder();
            feet.NumberOfFeet = 4;
            var mutantDog = new ActualDog(feet);
            feet.NumberOfFeet = 5;

            Assert.AreEqual(5, mutantDog.Feet.NumberOfFeet);
        }
    }
}
