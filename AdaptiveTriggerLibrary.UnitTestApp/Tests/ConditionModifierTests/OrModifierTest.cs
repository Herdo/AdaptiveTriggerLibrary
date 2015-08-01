namespace AdaptiveTriggerLibrary.UnitTestApp.Tests.ConditionModifierTests
{
    using ConditionModifiers;
    using ConditionModifiers.LogicalModifiers;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    [TestClass]
    public class OrModifierTest
    {
        [TestMethod]
        public void AllTrue_Single()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new OrModifier();

            // Act
            result = modifier.IsConditionMet(true, true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AllTrue_Multiple()
        {
            // Arrange
            bool result;
            var inputs = new[] { true, true, true };
            IConditionModifier modifier = new OrModifier();

            // Act
            result = modifier.IsConditionMet(true, inputs);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void AllTrue_Null()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new OrModifier();

            // Act
            result = modifier.IsConditionMet(true, null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NotAllTrue_Single()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new OrModifier();

            // Act
            result = modifier.IsConditionMet(true, false);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NotAllTrue_Multiple()
        {
            // Arrange
            bool result;
            var inputs = new[] { true, false, true };
            IConditionModifier modifier = new OrModifier();

            // Act
            result = modifier.IsConditionMet(true, inputs);

            // Assert
            Assert.IsTrue(result);
        }
    }
}