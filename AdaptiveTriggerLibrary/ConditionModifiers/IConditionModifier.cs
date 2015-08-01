namespace AdaptiveTriggerLibrary.ConditionModifiers
{
    using System;
    using Triggers;

    /// <summary>
    /// Interface for confition modifiers.
    /// </summary>
    /// <seealso cref="IAdaptiveTrigger{TCondition,TConditionModifier}"/>
    public interface IConditionModifier
    {
        /// <summary>
        /// Checks if the <paramref name="values"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="values">The actual value(s).</param>
        /// <exception cref="ArgumentException">The underlying types of <paramref name="condition"/> and <paramref name="values"/> doesn't match.</exception>
        /// <exception cref="InvalidCastException">Either <paramref name="condition"/> or an element in the sequence of <paramref name="values"/> cannot be casted to the underlying type.</exception>
        /// <returns>True, if the <paramref name="values"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        bool IsConditionMet(object condition, params object[] values);
    }
}