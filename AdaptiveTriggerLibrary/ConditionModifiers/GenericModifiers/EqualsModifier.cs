namespace AdaptiveTriggerLibrary.ConditionModifiers.GenericModifiers
{
    using System;

    /// <summary>
    /// A modifier where the value must be equal to the condition.
    /// </summary>
    /// <typeparam name="T">Any <see cref="System.Type"/> derived from <see cref="object"/>.</typeparam>
    public class EqualsModifier<T> : ModifierBase,
                                     IGenericModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static bool IsConditionMet(T condition, T value)
        {
            return Equals(condition, value);
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region IGenericModifier<T> Members

        #endregion

        /// <summary>
        /// Checks if the <paramref name="value"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The actual value.</param>
        /// <exception cref="ArgumentException">The underlying type of <paramref name="condition"/> doesn't match expected condition type,
        /// or the underlying type of<paramref name="value"/> doesn't match the expected value type.</exception>
        /// <exception cref="InvalidCastException">Either <paramref name="condition"/> or <paramref name="value"/> cannot be casted to the specified underlying type.</exception>
        /// <returns>True, if the <paramref name="value"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        bool IConditionModifier.IsConditionMet(object condition, object value)
        {
            Tuple<T, T> singleValueParameters;

            var parsed = TryGetParsedParameters(condition, value, out singleValueParameters);

            // Support single values
            if (parsed)
                return IsConditionMet(singleValueParameters.Item1, singleValueParameters.Item2);

            // Cast failed
            throw new InvalidCastException(InvalidCastMessage);
        }
    }
}