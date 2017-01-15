using NSubstitute;
using NUnit.Framework;
using Server.Common;
using Server.IO.UI.Controls;
using Server.IO.UI.Display;
using Tests.ScriptFramework;

namespace Tests.Tests.UI.Component
{
    [TestFixture]
    public class ButtonTests
    {
        private class SetButtonInputDevice : HijackableInputDevice
        {
            public SetButtonInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.button_set);
            }

            public override void Click(int x, int y, int wait)
            {
                if (Within(x, y, 0, 24, 0, 39))
                    return;

                base.Click(x, y, wait);
            }
        }

        private class DeployEscortsButtonInputDevice : HijackableInputDevice
        {
            public DeployEscortsButtonInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.button_deployescorts);
            }

            public override void Click(int x, int y, int wait)
            {
                if (Within(x, y, 7, 31, 13, 93))
                    return;

                base.Click(x, y, wait);
            }
        }

        private class FourButtonInputDevice : HijackableInputDevice
        {
            public FourButtonInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.button_fourbuttons);
            }

            public override void Click(int x, int y, int wait)
            {
                if (Within(x, y, 0, 24, 88, 168))
                    return;

                base.Click(x, y, wait);
            }
        }

        private class ProductionWindowInputDevice : HijackableInputDevice
        {
            public ProductionWindowInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.buttons_01);
            }

            public override void Click(int x, int y, int wait)
            {
                if (Within(x, y, 874, 915, 419, 491))
                    return;

                base.Click(x, y, wait);
            }
        }

        private class TaskGroupWindowInputDevice : HijackableInputDevice
        {
            public TaskGroupWindowInputDevice(HijackableScreenShotCapturer screenshot)
                : base(screenshot)
            {
                SetScreen(Properties.Resources.buttons_01);
            }

            public override void Click(int x, int y, int wait)
            {
                if (Within(x, y, 906, 930, 211, 291))
                    return;

                base.Click(x, y, wait);
            }
        }

        [Test]
        public void ClicksSetButton()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new SetButtonInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));

            var button = new Button(screen, inputDevice, 0, 24, 0, 39);
            button.Click();
        }

        [Test]
        public void ClicksDeployEscortsButton()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new DeployEscortsButtonInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));

            var button = new Button(screen, inputDevice, 7, 31, 13, 39);
            button.Click();
        }

        [Test]
        public void ClicksEqualizeMaintButton()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new FourButtonInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));

            var button = new Button(screen, inputDevice, 0, 24, 88, 168);
            button.Click();
        }

        [Test]
        public void ClicksRefreshAllButton()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new ProductionWindowInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));

            var button = new Button(screen, inputDevice, 874, 915, 419, 491);
            button.Click();
        }

        [Test]
        public void ClicksOOBButton()
        {
            var screenshot = new HijackableScreenShotCapturer();
            var inputDevice = new TaskGroupWindowInputDevice(screenshot);
            var screen = new Screen(new ScreenDataRetriever(Substitute.For<ISleeper>(), screenshot));

            var button = new Button(screen, inputDevice, 906, 930, 211, 291);
            button.Click();
        }
    }
}
