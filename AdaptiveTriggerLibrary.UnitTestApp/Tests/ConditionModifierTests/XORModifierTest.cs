namespace AdaptiveTriggerLibrary.UnitTestApp.Tests.ConditionModifierTests
{
    using System;
    using System.Linq;
    using ConditionModifiers;
    using ConditionModifiers.LogicalModifiers;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

    [TestClass]
    public class XORModifierTest
    {
        [TestMethod]
        public void XOR_InvalidCast()
        {
            // Arrange
            IConditionModifier modifier = new XORModifier();

            // Act
            Action action = () => modifier.IsConditionMet(null, null);

            // Assert
            Assert.ThrowsException<InvalidCastException>(action);
        }

        [TestMethod]
        public void XOR_AllowNullValue_ConditionMet()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new XORModifier();
            modifier.TreatNullAsConditionMet = true;

            // Act
            result = modifier.IsConditionMet(null, null);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void XOR_AllowNullValue_ConditionNotMet()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new XORModifier();
            modifier.TreatNullAsConditionNotMet = true;

            // Act
            result = modifier.IsConditionMet(null, null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void XOR_AllTrue_Single()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new XORModifier();

            // Act
            result = modifier.IsConditionMet(true, true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void XOR_AllTrue_Multiple()
        {
            // Arrange
            bool result;
            var inputs = new[] { true, true, true };
            IConditionModifier modifier = new XORModifier();

            // Act
            result = modifier.IsConditionMet(true, inputs.ToArray());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void XOR_AllTrue_Null()
        {
            // Arrange
            IConditionModifier modifier = new XORModifier();

            // Act
            Action action = () => modifier.IsConditionMet(true, null);

            // Assert
            Assert.ThrowsException<InvalidCastException>(action);
        }

        [TestMethod]
        public void XOR_NotAllTrue_Single()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new XORModifier();

            // Act
            result = modifier.IsConditionMet(true, false);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void XOR_NotAllTrue_Multiple()
        {
            // Arrange
            bool result;
            var inputs = new[] { true, false, true };
            IConditionModifier modifier = new XORModifier();

            // Act
            result = modifier.IsConditionMet(false, inputs);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void XOR_NotAllTrue_MultipleInverted()
        {
            // Arrange
            bool result;
            var inputs = new[] { true, false, true };
            IConditionModifier modifier = new XORModifier();

            // Act
            result = modifier.IsConditionMet(true, inputs);

            // Assert
            Assert.IsFalse(result);
        }
    }
}