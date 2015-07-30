namespace AdaptiveTriggerLibrary.Interfaces
{
    using System;

    public interface IAdaptiveTrigger<TCondition, TConditionModifier>
        where TConditionModifier : IConditionModifier
    {
        event EventHandler IsActiveChanged;

        bool IsActive { get; }
    }
}