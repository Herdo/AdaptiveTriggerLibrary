namespace AdaptiveTriggerLibrary.UnitTestApp.Tests.ConditionModifierTests
{
    using ConditionModifiers.ComparableModifiers;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    [TestClass]
    public class EqualToModifierTest
    {
        [TestMethod]
        public void Equals_Bool_True()
        {
            // Arrange
            bool result;
            var modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet(true, true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Equals_Bool_False()
        {
            // Arrange
            bool result;
            var modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet(true, false);

            // Assert
            Assert.IsFalse(result);
        }
    }
}