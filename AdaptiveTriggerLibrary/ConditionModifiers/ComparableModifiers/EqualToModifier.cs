namespace AdaptiveTriggerLibrary.ConditionModifiers.ComparableModifiers
{
    using System;
    using System.Linq;

    /// <summary>
    /// A modifier where the first value of the values must be equal to the condition.
    /// </summary>
    public class EqualToModifier : IComparableModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private bool IsConditionMet(IComparable condition, params IComparable[] values)
        {
            var value = values.Length >= 1
                ? values[0]
                : null;
            if (value == null)
                return false;
            return ReferenceEquals(condition, value)
                || Equals(condition, value)
                || value.CompareTo(condition) == 0;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region IComparableModifier Members

        /// <summary>
        /// Checks if the <paramref name="values"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="values">The actual value(s).</param>
        /// <exception cref="ArgumentException">The underlying types of <paramref name="condition"/> and <paramref name="values"/> doesn't match.</exception>
        /// <exception cref="InvalidCastException">Either <paramref name="condition"/> or an element in the sequence of <paramref name="values"/> cannot be casted to the underlying type.</exception>
        /// <returns>True, if the <paramref name="values"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        public bool IsConditionMet(object condition, params object[] values)
        {
            return IsConditionMet((IComparable) condition, values?.Cast<IComparable>().ToArray());
        }

        #endregion
    }
}