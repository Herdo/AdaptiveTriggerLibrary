namespace AdaptiveTriggerLibrary.Interfaces
{
    using System;

    /// <summary>
    /// The base interface for all adaptive triggers in the project.
    /// </summary>
    /// <typeparam name="TCondition">The type of the <see cref="Condition"/>.</typeparam>
    /// <typeparam name="TConditionModifier">The type of the <see cref="ConditionModifier"/>, that can influence the way that the <see cref="Condition"/> is treated.</typeparam>
    public interface IAdaptiveTrigger<TCondition, TConditionModifier>
        where TConditionModifier : IConditionModifier
    {
        event EventHandler IsActiveChanged;

        bool IsActive { get; }

        TCondition Condition { get; set; }

        TConditionModifier ConditionModifier { get; set; }
    }
}