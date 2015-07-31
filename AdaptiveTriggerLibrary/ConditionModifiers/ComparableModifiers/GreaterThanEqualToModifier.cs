﻿namespace AdaptiveTriggerLibrary.ConditionModifiers.ComparableModifiers
{
    using System;

    public class GreaterThanEqualToModifier : IComparableModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region IConditionModifier<IComparable> Members

        public bool IsConditionMet(IComparable value, IComparable condition)
        {
            return Equals(value, condition) || value.CompareTo(condition) > 0;
        }

        #endregion
    }
}