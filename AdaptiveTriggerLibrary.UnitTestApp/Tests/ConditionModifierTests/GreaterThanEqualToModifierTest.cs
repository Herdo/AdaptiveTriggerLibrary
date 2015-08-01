namespace AdaptiveTriggerLibrary.UnitTestApp.Tests.ConditionModifierTests
{
    using System;
    using ConditionModifiers;
    using ConditionModifiers.ComparableModifiers;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using Mocks;

    [TestClass]
    public class GreaterThanEqualToModifierTest
    {
        [TestMethod]
        public void GreaterThanEqualTo_Bool_True()
        {
            // Arrange
            bool result1;
            bool result2;
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            result1 = modifier.IsConditionMet(false, true);
            result2 = modifier.IsConditionMet(false, false);

            // Assert
            Assert.IsTrue(result1, "GreaterThan failed.");
            Assert.IsTrue(result2, "EqualTo failed.");
        }

        [TestMethod]
        public void GreaterThanEqualTo_Bool_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            result = modifier.IsConditionMet(true, false);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GreaterThanEqualTo_Bool_InvalidType_ArgumentTypeMismatch()
        {
            // Arrange
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", false);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void GreaterThanEqualTo_Int32_True()
        {
            // Arrange
            bool result1;
            bool result2;
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            result1 = modifier.IsConditionMet(12, 15);
            result2 = modifier.IsConditionMet(12, 12);

            // Assert
            Assert.IsTrue(result1, "GreaterThan failed.");
            Assert.IsTrue(result2, "EqualTo failed.");
        }

        [TestMethod]
        public void GreaterThanEqualTo_Int32_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            result = modifier.IsConditionMet(15, 12);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GreaterThanEqualTo_Int32_InvalidType_ArgumentTypeMismatch()
        {
            // Arrange
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", 15);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void GreaterThanEqualTo_Double_True()
        {
            // Arrange
            bool result1;
            bool result2;
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            result1 = modifier.IsConditionMet(12.0, 15.0);
            result2 = modifier.IsConditionMet(12.0, 12.0);

            // Assert
            Assert.IsTrue(result1, "GreaterThan failed.");
            Assert.IsTrue(result2, "EqualTo failed.");
        }

        [TestMethod]
        public void GreaterThanEqualTo_Double_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            result = modifier.IsConditionMet(15.0, 12.0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GreaterThanEqualTo_Double_InvalidType_ArgumentTypeMismatch()
        {
            // Arrange
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", 15.0);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void GreaterThanEqualTo_String_True()
        {
            // Arrange
            bool result1;
            bool result2;
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            result1 = modifier.IsConditionMet("bar", "foo");
            result2 = modifier.IsConditionMet("bar", "bar");

            // Assert
            Assert.IsTrue(result1, "GreaterThan failed.");
            Assert.IsTrue(result2, "EqualTo failed.");
        }

        [TestMethod]
        public void GreaterThanEqualTo_String_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            result = modifier.IsConditionMet("foo", "bar");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GreaterThanEqualTo_String_InvalidType_ArgumentTypeMismatch()
        {
            // Arrange
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet(42, "bar");

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void GreaterThanEqualTo_DateTime_True()
        {
            // Arrange
            bool result1;
            bool result2;
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            result1 = modifier.IsConditionMet(DateTime.MinValue, DateTime.MaxValue);
            result2 = modifier.IsConditionMet(DateTime.MinValue, DateTime.MinValue);

            // Assert
            Assert.IsTrue(result1, "GreaterThan failed.");
            Assert.IsTrue(result2, "EqualTo failed.");
        }

        [TestMethod]
        public void GreaterThanEqualTo_DateTime_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            result = modifier.IsConditionMet(DateTime.MaxValue, DateTime.MinValue);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GreaterThanEqualTo_DateTime_InvalidType_ArgumentTypeMismatch()
        {
            // Arrange
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", DateTime.MinValue);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void GreaterThanEqualTo_CustomIComparableImplementation_True()
        {
            // Arrange
            bool result1;
            bool result2;
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            result1 = modifier.IsConditionMet(new CustomIComparableImplementation(5), new CustomIComparableImplementation(15));
            result2 = modifier.IsConditionMet(new CustomIComparableImplementation(5), new CustomIComparableImplementation(5));

            // Assert
            Assert.IsTrue(result1, "GreaterThan failed.");
            Assert.IsTrue(result2, "EqualTo failed.");
        }

        [TestMethod]
        public void GreaterThanEqualTo_CustomIComparableImplementation_False()
        {
            // Arrange
            bool result;
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            result = modifier.IsConditionMet(new CustomIComparableImplementation(15), new CustomIComparableImplementation(12));

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GreaterThanEqualTo_CustomIComparableImplementation_InvalidType_ArgumentTypeMismatch()
        {
            // Arrange
            IConditionModifier modifier = new GreaterThanEqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", new CustomIComparableImplementation(15));

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }
    }
}