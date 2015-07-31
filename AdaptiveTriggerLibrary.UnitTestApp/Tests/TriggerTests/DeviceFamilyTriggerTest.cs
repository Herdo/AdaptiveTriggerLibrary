namespace AdaptiveTriggerLibrary.UnitTestApp.Tests.TriggerTests
{
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using Triggers.HardwareInterfaceTriggers;

    [TestClass]
    public class DeviceFamilyTriggerTest
    {
        [TestMethod]
        public void IsActive_Ok()
        {
            // Arrange
            var trigger = new DeviceFamilyTrigger();

            // Act
            trigger.Condition = "PC";

            // Assert
            Assert.IsTrue(trigger.IsActive);
        }
    }
}
