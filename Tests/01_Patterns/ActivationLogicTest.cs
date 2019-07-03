using NUnit.Framework;
using StatePattern;

namespace Tests
{
    [TestFixture]
    public class ActivationLogicTest
    {
        string EventRaised;
        ActivationLogic Logic;

        [SetUp]
        public void Setup()
        {
            EventRaised = string.Empty;
            Logic = new ActivationLogic();

            Logic.Enabled += () => EventRaised += "Enabled";
            Logic.Disabled += () => EventRaised += "Disabled";
        }

        [Test]
        public void DisabledState_EnableEvent_EnableActionCalled()
        {
            Logic.Enable();
            Assert.AreEqual("Enabled", EventRaised);
        }

        [Test]
        public void DisabledState_DisableEvent_NothingCalled()
        {
            Logic.Disable();
            Assert.AreEqual(string.Empty, EventRaised);
        }

        [Test]
        public void EnabledState_DisableEvent_DisableActionCalled()
        {
            Logic.SetState(State.Enabled);
            Logic.Disable();
            Assert.AreEqual("Disabled", EventRaised);
        }

        [Test]
        public void EnabledState_EnableEvent_NothingCalled()
        {
            Logic.SetState(State.Enabled);
            Logic.Enable();
            Assert.AreEqual(string.Empty, EventRaised);
        }

        [Test]
        public void DisabledState_FullPathToDisabledStateTwice_CallsEnableAndDisableInTurns()
        {
            Logic.Enable();
            Logic.Disable();
            Logic.Enable();
            Logic.Disable();

            Assert.AreEqual("EnabledDisabledEnabledDisabled", EventRaised);
        }
    }
}
