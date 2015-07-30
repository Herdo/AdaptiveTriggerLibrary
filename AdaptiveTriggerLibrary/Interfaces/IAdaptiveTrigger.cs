namespace AdaptiveTriggerLibrary.Interfaces
{
    using System;

    /// <summary>
    /// Interface for all adaptive triggers in the project.
    /// </summary>
    /// <typeparam name="TCondition">The type of the <see cref="Condition"/>.</typeparam>
    public interface IAdaptiveTrigger<TCondition>
    {
        /// <summary>
        /// Occurs when the value of <see cref="IsActive"/> changed.
        /// </summary>
        event EventHandler IsActiveChanged;

        /// <summary>
        /// Gets if the trigger is currently active.
        /// </summary>
        bool IsActive { get; }

        /// <summary>
        /// Gets or sets the condition that must be met, in order to set <see cref="IsActive"/> to true.
        /// </summary>
        /// <remarks>This property can only be set once.</remarks>
        /// <exception cref="InvalidOperationException"><see cref="Condition"/> is set more than once.</exception>
        TCondition Condition { get; set; }

        /// <summary>
        /// Gets or sets the modifier that will be applied to the validation of the <see cref="Condition"/>.
        /// </summary>
        /// <remarks>This property can only be set once.</remarks>
        /// <exception cref="InvalidOperationException"><see cref="ConditionModifier"/> is set more than once.</exception>
        IConditionModifier<TCondition> ConditionModifier { get; set; }
    }
}