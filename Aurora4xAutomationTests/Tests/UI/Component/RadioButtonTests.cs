using Aurora4xAutomation.Common;
using Aurora4xAutomation.IO.UI.Controls;
using Aurora4xAutomation.IO.UI.Display;
using Aurora4xAutomationTests.ScriptFramework;
using NSubstitute;
using NUnit.Framework;

namespace Aurora4xAutomationTests.Tests.UI.Component
{
    [TestFixture]
    public class RadioButtonTests
    {
        private class CivilianTabInputDevice : HijackableInputDevice
        {
            private bool _supplySelected;

            public CivilianTabInputDevice(HijackableScreenShotCapturer screenshot, bool supplySelected)
                : base(screenshot)
            {
                _supplySelected = supplySelected;

                if (supplySelected)
                    SetScreen(Properties.Resources.window_civiliantab_supply);
                else
                    SetScreen(Properties.Resources.window_civiliantab_demand);
            }

            public override void Click(int x, int y, int wait)
            {
                if (Within(x, y, 220, 231, 696, 707))
                    _supplySelected = true;
                else if (Within(x, y, 221, 232, 784, 795))
                    _supplySelected = false;
                else
                    base.Click(x, y, wait);

                if (_supplySelected)
                    SetScreen(Properties.Resources.window_civiliantab_supply);
                else
                    SetScreen(Properties.Resources.window_civiliantab_demand);
            }
        }

        [Test]
        public void SelectsDemandWhenSupplyIsSet()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new CivilianTabInputDevice(screenshot, true);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));

            var supply = new RadioButton(screen, inputDevice, 220, 231, 696, 707);
            var demand = new RadioButton(screen, inputDevice, 221, 232, 784, 795);

            Assert.IsTrue(supply.Selected);
            Assert.IsFalse(demand.Selected);
            demand.Select();
            Assert.IsTrue(demand.Selected);
            Assert.IsFalse(supply.Selected);
        }

        [Test]
        public void SelectsSupplyWhenDemandIsSet()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new CivilianTabInputDevice(screenshot, false);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));

            var supply = new RadioButton(screen, inputDevice, 220, 231, 696, 707);
            var demand = new RadioButton(screen, inputDevice, 221, 232, 784, 795);

            Assert.IsFalse(supply.Selected);
            Assert.IsTrue(demand.Selected);
            supply.Select();
            Assert.IsFalse(demand.Selected);
            Assert.IsTrue(supply.Selected);
        }

        [Test]
        public void SelectsSupplyWhenSupplyIsSet()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new CivilianTabInputDevice(screenshot, true);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));

            var supply = new RadioButton(screen, inputDevice, 220, 231, 696, 707);
            var demand = new RadioButton(screen, inputDevice, 221, 232, 784, 795);

            Assert.IsTrue(supply.Selected);
            Assert.IsFalse(demand.Selected);
            supply.Select();
            Assert.IsTrue(supply.Selected);
            Assert.IsFalse(demand.Selected);
        }

        [Test]
        public void SelectsDemandWhenDemandIsSet()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new CivilianTabInputDevice(screenshot, false);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));

            var supply = new RadioButton(screen, inputDevice, 220, 231, 696, 707);
            var demand = new RadioButton(screen, inputDevice, 221, 232, 784, 795);

            Assert.IsFalse(supply.Selected);
            Assert.IsTrue(demand.Selected);
            demand.Select();
            Assert.IsFalse(supply.Selected);
            Assert.IsTrue(demand.Selected);
        }
    }
}
