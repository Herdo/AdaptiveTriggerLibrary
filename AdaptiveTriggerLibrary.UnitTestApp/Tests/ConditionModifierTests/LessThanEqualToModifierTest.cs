namespace AdaptiveTriggerLibrary.UnitTestApp.Tests.ConditionModifierTests
{
    using System;
    using ConditionModifiers.ComparableModifiers;
    using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
    using Mocks;

    [TestClass]
    public class LessThanEqualToModifierTest
    {
        [TestMethod]
        public void LessThanEqualTo_Bool_True()
        {
            // Arrange
            bool result1;
            bool result2;
            var modifier = new LessThanEqualToModifier();

            // Act
            result1 = modifier.IsConditionMet(true, false);
            result2 = modifier.IsConditionMet(false, false);

            // Assert
            Assert.IsTrue(result1, "LessThan failed.");
            Assert.IsTrue(result2, "EqualTo failed.");
        }

        [TestMethod]
        public void LessThanEqualTo_Bool_False()
        {
            // Arrange
            bool result;
            var modifier = new LessThanEqualToModifier();

            // Act
            result = modifier.IsConditionMet(false, true);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LessThanEqualTo_Bool_InvalidType()
        {
            // Arrange
            var modifier = new LessThanEqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", false);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void LessThanEqualTo_Int32_True()
        {
            // Arrange
            bool result1;
            bool result2;
            var modifier = new LessThanEqualToModifier();

            // Act
            result1 = modifier.IsConditionMet(15, 12);
            result2 = modifier.IsConditionMet(15, 15);

            // Assert
            Assert.IsTrue(result1, "LessThan failed.");
            Assert.IsTrue(result2, "EqualTo failed.");
        }

        [TestMethod]
        public void LessThanEqualTo_Int32_False()
        {
            // Arrange
            bool result;
            var modifier = new LessThanEqualToModifier();

            // Act
            result = modifier.IsConditionMet(12, 15);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LessThanEqualTo_Int32_InvalidType()
        {
            // Arrange
            var modifier = new LessThanEqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", 15);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void LessThanEqualTo_Double_True()
        {
            // Arrange
            bool result1;
            bool result2;
            var modifier = new LessThanEqualToModifier();

            // Act
            result1 = modifier.IsConditionMet(15.0, 12.0);
            result2 = modifier.IsConditionMet(15.0, 15.0);

            // Assert
            Assert.IsTrue(result1, "LessThan failed.");
            Assert.IsTrue(result2, "EqualTo failed.");
        }

        [TestMethod]
        public void LessThanEqualTo_Double_False()
        {
            // Arrange
            bool result;
            var modifier = new LessThanEqualToModifier();

            // Act
            result = modifier.IsConditionMet(12.0, 15.0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LessThanEqualTo_Double_InvalidType()
        {
            // Arrange
            var modifier = new LessThanEqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", 15.0);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void LessThanEqualTo_String_True()
        {
            // Arrange
            bool result1;
            bool result2;
            var modifier = new LessThanEqualToModifier();

            // Act
            result1 = modifier.IsConditionMet("foo", "bar");
            result2 = modifier.IsConditionMet("foo", "foo");

            // Assert
            Assert.IsTrue(result1, "LessThan failed.");
            Assert.IsTrue(result2, "EqualTo failed.");
        }

        [TestMethod]
        public void LessThanEqualTo_String_False()
        {
            // Arrange
            bool result;
            var modifier = new LessThanEqualToModifier();

            // Act
            result = modifier.IsConditionMet("bar", "foo");

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LessThanEqualTo_String_InvalidType()
        {
            // Arrange
            var modifier = new LessThanEqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet(42, "bar");

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void LessThanEqualTo_DateTime_True()
        {
            // Arrange
            bool result1;
            bool result2;
            var modifier = new LessThanEqualToModifier();

            // Act
            result1 = modifier.IsConditionMet(DateTime.MaxValue, DateTime.MinValue);
            result2 = modifier.IsConditionMet(DateTime.MaxValue, DateTime.MaxValue);

            // Assert
            Assert.IsTrue(result1, "LessThan failed.");
            Assert.IsTrue(result2, "EqualTo failed.");
        }

        [TestMethod]
        public void LessThanEqualTo_DateTime_False()
        {
            // Arrange
            bool result;
            var modifier = new LessThanEqualToModifier();

            // Act
            result = modifier.IsConditionMet(DateTime.MinValue, DateTime.MaxValue);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LessThanEqualTo_DateTime_InvalidType()
        {
            // Arrange
            var modifier = new LessThanEqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet("foo", DateTime.MinValue);

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }

        [TestMethod]
        public void LessThanEqualTo_CustomIComparableImplementation_True()
        {
            // Arrange
            bool result1;
            bool result2;
            var modifier = new LessThanEqualToModifier();

            // Act
            result1 = modifier.IsConditionMet(new CustomIComparableImplementation(15), new CustomIComparableImplementation(5));
            result2 = modifier.IsConditionMet(new CustomIComparableImplementation(15), new CustomIComparableImplementation(15));

            // Assert
            Assert.IsTrue(result1, "LessThan failed.");
            Assert.IsTrue(result2, "EqualTo failed.");
        }

        [TestMethod]
        public void LessThanEqualTo_CustomIComparableImplementation_False()
        {
            // Arrange
            bool result;
            var modifier = new LessThanEqualToModifier();

            // Act
            result = modifier.IsConditionMet(new CustomIComparableImplementation(5), new CustomIComparableImplementation(15));

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LessThanEqualTo_CustomIComparableImplementation_InvalidType()
        {
            // Arrange
            var modifier = new LessThanEqualToModifier();

            // Act
            Action action = () => modifier.IsConditionMet(DateTime.Now, new CustomIComparableImplementation(15));

            // Assert
            Assert.ThrowsException<ArgumentException>(action);
        }
    }
}