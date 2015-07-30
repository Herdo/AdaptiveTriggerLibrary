namespace AdaptiveTriggerLibrary.ConditionModifiers.ComparableModifiers
{
    using System;
    using Interfaces;

    public class LessThanModifier : IConditionModifier<IComparable>
    {
        ///////////////////////////////////////////////////////////////////
        #region IConditionModifier<IComparable> Members

        public bool IsConditionMet(IComparable value, IComparable condition)
        {
            return value.CompareTo(condition) < 0;
        }

        #endregion
    }
}