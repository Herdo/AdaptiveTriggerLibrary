namespace AdaptiveTriggerLibrary.UnitTestApp.Tests.ConditionModifierTests
{
    using Windows.UI.ViewManagement;
    using ConditionModifiers;
    using ConditionModifiers.GenericModifiers;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    [TestClass]
    public class EqualsModifierTest
    {
        [TestMethod]
        public void Equals_True()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualsModifier<UserInteractionMode>();

            // Act
            result = modifier.IsConditionMet(UserInteractionMode.Mouse, UserInteractionMode.Mouse);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualsModifier<UserInteractionMode>();

            // Act
            result = modifier.IsConditionMet(UserInteractionMode.Mouse, UserInteractionMode.Touch);

            // Assert
            Assert.IsFalse(result);
        }
    }
}