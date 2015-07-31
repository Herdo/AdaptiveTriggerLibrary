namespace AdaptiveTriggerLibrary.ConditionModifiers.ComparableModifiers
{
    using System;

    public class LessThanModifier : IComparableModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region IComparableModifier Members

        public bool IsConditionMet(IComparable condition, params IComparable[] values)
        {
            var value = values.Length >= 1
                ? values[0]
                : null;
            return !ReferenceEquals(condition, value)
                && !Equals(condition, value)
                && condition.CompareTo(value) < 0;
        }

        #endregion
    }
}