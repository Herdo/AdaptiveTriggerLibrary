namespace AdaptiveTriggerLibrary.Triggers
{
    using System;
    using ConditionModifiers;

    /// <summary>
    /// Base interface for all adaptive triggers in the project.
    /// </summary>
    public interface IAdaptiveTrigger
    {
        /// <summary>
        /// Occurs when the value of <see cref="IsActive"/> changed.
        /// </summary>
        event EventHandler IsActiveChanged;

        /// <summary>
        /// Gets if the trigger is currently active.
        /// </summary>
        bool IsActive { get; }
    }

    /// <summary>
    /// Interface for all adaptive triggers in the project.
    /// </summary>
    /// <typeparam name="TCondition">The type of the <see cref="Condition"/>.</typeparam>
    /// <typeparam name="TConditionModifier">The type of the <see cref="ConditionModifier"/>, that can influence the way that the <see cref="Condition"/> is treated.</typeparam>
    public interface IAdaptiveTrigger<TCondition, TConditionModifier> : IAdaptiveTrigger
        where TConditionModifier : IConditionModifier<TCondition>
    {
        /// <summary>
        /// Gets or sets the condition that must be met, in order to set <see cref="IAdaptiveTrigger.IsActive"/> to true.
        /// </summary>
        /// <remarks>This property can only be set once.</remarks>
        /// <exception cref="InvalidOperationException"><see cref="Condition"/> is set more than once.</exception>
        TCondition Condition { get; set; }

        /// <summary>
        /// Gets or sets the modifier that will be applied to the validation of the <see cref="Condition"/>.
        /// </summary>
        /// <remarks>This property can only be set once.
        /// If no condition modifier is specified, the default modifier for the <typeparamref name="TConditionModifier"/> will be used.</remarks>
        /// <exception cref="InvalidOperationException"><see cref="ConditionModifier"/> is set more than once.</exception>
        TConditionModifier ConditionModifier { get; set; }
    }
}