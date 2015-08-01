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
        /// Checks if the <paramref name="value"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The actual value.</param>
        /// <exception cref="ArgumentException">The underlying type of <paramref name="condition"/> doesn't match expected condition type,
        /// or the underlying type of<paramref name="value"/> doesn't match the expected value type.</exception>
        /// <exception cref="InvalidCastException">Either <paramref name="condition"/> or <paramref name="value"/> cannot be casted to the specified underlying type.</exception>
        /// <returns>True, if the <paramref name="value"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        bool IsConditionMet(object condition, object value);
    }
}