namespace AdaptiveTriggerLibrary.ConditionModifiers.ComparableModifiers
{
    using System;

    public class GreaterThanModifier : IComparableModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region IComparableModifier Members

        public bool IsConditionMet(IComparable condition, params IComparable[] values)
        {
            var value = values.Length >= 1
                ? values[0]
                : null;
            if (value == null)
                return false;
            return !ReferenceEquals(condition, value)
                && !Equals(condition, value)
                && value.CompareTo(condition) > 0;
        }

        #endregion
    }
}