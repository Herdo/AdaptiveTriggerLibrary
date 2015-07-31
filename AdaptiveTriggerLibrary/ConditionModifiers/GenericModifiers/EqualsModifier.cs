namespace AdaptiveTriggerLibrary.ConditionModifiers.GenericModifiers
{
    public class EqualsModifier<T> : IGenericModifier<T>
    {
        ///////////////////////////////////////////////////////////////////
        #region IGenericModifier<T> Members

        public bool IsConditionMet(T condition, params T[] values)
        {
            return values.Length >= 1
                && Equals(condition, values[0]);
        }

        #endregion
    }
}