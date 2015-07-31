namespace AdaptiveTriggerLibrary.UnitTestApp.Tests.TriggerTests
{
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using Triggers.HardwareInterfaceTriggers;

    [TestClass]
    public class DeviceFamilyTriggerTest
    {
        // This test will currently fail, due to the test requests UITestMethod attribute, which is not available
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
