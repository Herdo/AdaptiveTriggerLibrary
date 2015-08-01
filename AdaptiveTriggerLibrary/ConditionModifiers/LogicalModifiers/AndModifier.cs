namespace AdaptiveTriggerLibrary.ConditionModifiers.LogicalModifiers
{
    using System;
    using System.Linq;

    /// <summary>
    /// A modifier where all values must meet the condition.
    /// </summary>
    public class AndModifier : ILogicalModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private bool IsConditionMet(bool condition, params bool[] values)
        {
            return values?.All(m => m == condition) ?? false;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region ILogicalModifier

        /// <summary>
        /// Checks if the <paramref name="values"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="values">The actual value(s).</param>
        /// <exception cref="ArgumentException">The underlying types of <paramref name="condition"/> and <paramref name="values"/> doesn't match.</exception>
        /// <exception cref="InvalidCastException">Either <paramref name="condition"/> or an element in the sequence of <paramref name="values"/> cannot be casted to the underlying type.</exception>
        /// <returns>True, if the <paramref name="values"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        bool IConditionModifier.IsConditionMet(object condition, params object[] values)
        {
            return IsConditionMet((bool)condition, values?.Cast<bool>().ToArray());
        }

        #endregion
    }
}