namespace AdaptiveTriggerLibrary.UnitTestApp.Tests.ConditionModifierTests
{
    using System;
    using ConditionModifiers;
    using ConditionModifiers.LogicalModifiers;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    [TestClass]
    public class OrModifierTest
    {
        [TestMethod]
        public void Or_InvalidCast()
        {
            // Arrange
            IConditionModifier modifier = new OrModifier();

            // Act
            Action action = () => modifier.IsConditionMet(null, null);

            // Assert
            Assert.ThrowsException<InvalidCastException>(action);
        }

        [TestMethod]
        public void Or_AllowNullValue_ConditionMet()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new OrModifier();
            modifier.TreatNullAsConditionMet = true;

            // Act
            result = modifier.IsConditionMet(null, null);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Or_AllowNullValue_ConditionNotMet()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new OrModifier();
            modifier.TreatNullAsConditionNotMet = true;

            // Act
            result = modifier.IsConditionMet(null, null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Or_AllTrue_Single()
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
        public void Or_AllTrue_Multiple()
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
        public void Or_AllTrue_Null()
        {
            // Arrange
            IConditionModifier modifier = new OrModifier();

            // Act
            Action action = () => modifier.IsConditionMet(true, null);

            // Assert
            Assert.ThrowsException<InvalidCastException>(action);
        }

        [TestMethod]
        public void Or_NotAllTrue_Single()
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
        public void Or_NotAllTrue_Multiple()
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