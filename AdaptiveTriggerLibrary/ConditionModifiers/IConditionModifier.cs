namespace AdaptiveTriggerLibrary.ConditionModifiers
{
    using Triggers;

    /// <summary>
    /// Interface for confition modifiers.
    /// </summary>
    /// <seealso cref="IAdaptiveTrigger{TCondition}"/>
    public interface IConditionModifier<in TCondition>
    {
        /// <summary>
        /// Checks if the <paramref name="value"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="value">The actual value.</param>
        /// <param name="condition">The condition.</param>
        /// <returns>True, if the <paramref name="value"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        bool IsConditionMet(TCondition value, TCondition condition);
    }
}