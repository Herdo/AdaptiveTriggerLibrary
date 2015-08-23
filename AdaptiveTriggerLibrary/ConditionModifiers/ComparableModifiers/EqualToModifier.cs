namespace AdaptiveTriggerLibrary.ConditionModifiers.ComparableModifiers
{
    using System;

    /// <summary>
    /// A modifier where the value must be equal to the condition.
    /// </summary>
    public class EqualToModifier : ModifierBase,
                                   IComparableModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static bool IsConditionMet(IComparable condition, IComparable value)
        {
            return ReferenceEquals(condition, value)
                || Equals(condition, value)
                || value.CompareTo(condition) == 0;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region IComparableModifier Members

        /// <summary>
        /// Checks if the <paramref name="value"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The actual value.</param>
        /// <exception cref="ArgumentException">The underlying type of <paramref name="condition"/> doesn't match expected condition type,
        /// or the underlying type of<paramref name="value"/> doesn't match the expected value type.</exception>
        /// <exception cref="InvalidCastException">Either <paramref name="condition"/> or <paramref name="value"/> cannot be casted to the specified underlying type.</exception>
        /// <returns>True, if the <paramref name="value"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        public override bool IsConditionMet(object condition, object value)
        {
            // Null handling
            if (value == null)
            {
                if (TreatNullAsConditionIsMet)
                    return true;
                if (TreatNullAsConditionIsNotMet)
                    return false;
            }

            Tuple<IComparable, IComparable> singleValueParameters;

            var parsed = TryGetParsedParameters(condition, value, out singleValueParameters);

            // Support single values
            if (parsed)
                return IsConditionMet(singleValueParameters.Item1, singleValueParameters.Item2);

            // Cast failed
            throw new InvalidCastException(InvalidCastMessage);
        }

        #endregion
    }
}