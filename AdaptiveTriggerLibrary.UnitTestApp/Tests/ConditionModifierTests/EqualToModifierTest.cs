namespace AdaptiveTriggerLibrary.UnitTestApp.Tests.ConditionModifierTests
{
    using System;
    using ConditionModifiers;
    using ConditionModifiers.ComparableModifiers;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using Mocks;

    [TestClass]
    public class EqualToModifierTest
    {
        [TestMethod]
        public void EqualTo_InvalidCast()
        {
            // Arrange
            IConditionModifier modifier = new EqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet(null, null);

            // Assert
            Assert.ThrowsException<InvalidCastException>(action);
        }

        [TestMethod]
        public void EqualTo_AllowNullValue_ConditionMet()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();
            modifier.TreatNullAsConditionMet = true;

            // Act
            result = modifier.IsConditionMet(null, null);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualTo_AllowNullValue_ConditionNotMet()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();
            modifier.TreatNullAsConditionNotMet = true;

            // Act
            result = modifier.IsConditionMet(null, null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EqualTo_Bool_True()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet(true, true);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualTo_Bool_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet(true, false);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EqualTo_Bool_InvalidType_ArgumentTypeMismatch()
        {
            // Arrange
            IConditionModifier modifier = new EqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", false);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void EqualTo_Int32_True()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet(15, 15);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualTo_Int32_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet(12, 15);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EqualTo_Int32_InvalidType_ArgumentTypeMismatch()
        {
            // Arrange
            IConditionModifier modifier = new EqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", 15);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void EqualTo_Double_True()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet(15.0, 15.0);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualTo_Double_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet(12.0, 15.0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EqualTo_Double_InvalidType_ArgumentTypeMismatch()
        {
            // Arrange
            IConditionModifier modifier = new EqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", 15.0);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void EqualTo_String_True()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet("bar", "bar");

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualTo_String_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet("foo", "bar");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EqualTo_String_InvalidType_ArgumentTypeMismatch()
        {
            // Arrange
            IConditionModifier modifier = new EqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet(42, "bar");

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void EqualTo_DateTime_True()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet(DateTime.MinValue, DateTime.MinValue);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualTo_DateTime_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet(DateTime.MaxValue, DateTime.MinValue);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EqualTo_DateTime_InvalidType_ArgumentTypeMismatch()
        {
            // Arrange
            IConditionModifier modifier = new EqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", DateTime.MinValue);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void EqualTo_CustomIComparableImplementation_True()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet(new CustomIComparableImplementation(15), new CustomIComparableImplementation(15));

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void EqualTo_CustomIComparableImplementation_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new EqualToModifier();

            // Act
            result = modifier.IsConditionMet(new CustomIComparableImplementation(5), new CustomIComparableImplementation(15));

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void EqualTo_CustomIComparableImplementation_InvalidType_ArgumentTypeMismatch()
        {
            // Arrange
            IConditionModifier modifier = new EqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", new CustomIComparableImplementation(15));

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }
    }
}