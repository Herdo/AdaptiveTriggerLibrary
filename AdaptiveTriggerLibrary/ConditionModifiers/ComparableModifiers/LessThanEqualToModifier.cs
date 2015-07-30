namespace AdaptiveTriggerLibrary.ConditionModifiers.ComparableModifiers
{
    using System;
    using Interfaces;

    public class LessThanEqualToModifier : IConditionModifier<IComparable>
    {
        ///////////////////////////////////////////////////////////////////
        #region IConditionModifier<IComparable> Members

        public bool IsConditionMet(IComparable value, IComparable condition)
        {
            return Equals(value, condition) || value.CompareTo(condition) < 0 ;
        }

        #endregion
    }
}