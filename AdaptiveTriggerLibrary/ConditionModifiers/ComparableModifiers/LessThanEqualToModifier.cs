namespace AdaptiveTriggerLibrary.ConditionModifiers.ComparableModifiers
{
    using System;

    /// <summary>
    /// A modifier where the first value of the values must be less than or equal to the condition.
    /// </summary>
    public class LessThanEqualToModifier : IComparableModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region IComparableModifier Members

        /// <summary>
        /// Checks if the <paramref name="values"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="values">The actual value(s).</param>
        /// <returns>True, if the <paramref name="values"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        public bool IsConditionMet(IComparable condition, params IComparable[] values)
        {
            var value = values.Length >= 1
                ? values[0]
                : null;
            if (value == null)
                return false;
            return ReferenceEquals(condition, value)
                || Equals(condition, value)
                || value.CompareTo(condition) <= 0 ;
        }

        #endregion
    }
}