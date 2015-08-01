namespace AdaptiveTriggerLibrary.ConditionModifiers.LogicalModifiers
{
    using System;
    using System.Linq;

    /// <summary>
    /// A modifier where any value of the values must meet the condition.
    /// </summary>
    public class OrModifier : ILogicalModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private bool IsConditionMet(bool condition, params bool[] values)
        {
            return values?.Any(m => m == condition) ?? false;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region ILogicalModifier Members

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
            return IsConditionMet((bool) condition, values?.Cast<bool>().ToArray());
        }

        #endregion
    }
}