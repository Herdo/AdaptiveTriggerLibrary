namespace AdaptiveTriggerLibrary.ConditionModifiers.ComparableModifiers
{
    using System;

    public class EqualToModifier : IComparableModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region IComparableModifier Members

        public bool IsConditionMet(IComparable value, IComparable condition)
        {
            return ReferenceEquals(value, condition)
                || Equals(value, condition)
                || value.CompareTo(condition) == 0;
        }

        #endregion
    }
}