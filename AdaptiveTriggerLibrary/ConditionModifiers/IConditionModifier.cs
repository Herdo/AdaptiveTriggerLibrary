namespace AdaptiveTriggerLibrary.ConditionModifiers
{
    using Triggers;

    /// <summary>
    /// Interface for confition modifiers.
    /// </summary>
    /// <seealso cref="IAdaptiveTrigger{TCondition,TConditionModifier}"/>
    public interface IConditionModifier<in TCondition>
    {
        /// <summary>
        /// Checks if the <paramref name="values"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="values">The actual value(s).</param>
        /// <returns>True, if the <paramref name="values"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        bool IsConditionMet(TCondition condition, params TCondition[] values);
    }
}