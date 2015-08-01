namespace AdaptiveTriggerLibrary.ConditionModifiers.GenericModifiers
{
    /// <summary>
    /// A modifier where the first value of the values must be equal to the condition.
    /// </summary>
    /// <typeparam name="T">Any <see cref="System.Type"/> derived from <see cref="object"/>.</typeparam>
    public class EqualsModifier<T> : IGenericModifier<T>
    {
        ///////////////////////////////////////////////////////////////////
        #region IGenericModifier<T> Members

        /// <summary>
        /// Checks if the <paramref name="values"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="values">The actual value(s).</param>
        /// <returns>True, if the <paramref name="values"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        public bool IsConditionMet(T condition, params T[] values)
        {
            return values.Length >= 1
                && Equals(condition, values[0]);
        }

        #endregion
    }
}