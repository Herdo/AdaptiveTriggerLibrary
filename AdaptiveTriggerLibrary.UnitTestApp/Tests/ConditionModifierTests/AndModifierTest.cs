namespace AdaptiveTriggerLibrary.UnitTestApp.Tests.ConditionModifierTests
{
    using System;
    using System.Linq;
    using ConditionModifiers;
    using ConditionModifiers.LogicalModifiers;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    [TestClass]
    public class AndModifierTest
    {
        [TestMethod]
        public void GreaterThanEqualTo_InvalidCast()
        {
            // Arrange
            IConditionModifier modifier = new AndModifier();

            // Act
            Action action = () => modifier.IsConditionMet(null, null);

            // Assert
            Assert.ThrowsException<InvalidCastException>(action);
        }

        [TestMethod]
        public void And_AllTrue_Single()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new AndModifier();

            // Act
            result = modifier.IsConditionMet(true, true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void And_AllTrue_Multiple()
        {
            // Arrange
            bool result;
            var inputs = new[] { true, true, true };
            IConditionModifier modifier = new AndModifier();

            // Act
            result = modifier.IsConditionMet(true, inputs.ToArray());

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void And_AllTrue_Null()
        {
            // Arrange
            IConditionModifier modifier = new AndModifier();

            // Act
            Action action = () => modifier.IsConditionMet(true, null);

            // Assert
            Assert.ThrowsException<InvalidCastException>(action);
        }

        [TestMethod]
        public void And_NotAllTrue_Single()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new AndModifier();

            // Act
            result = modifier.IsConditionMet(true, false);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void And_NotAllTrue_Multiple()
        {
            // Arrange
            bool result;
            var inputs = new[] { true, false, true };
            IConditionModifier modifier = new AndModifier();

            // Act
            result = modifier.IsConditionMet(true, inputs);

            // Assert
            Assert.IsFalse(result);
        }
    }
}