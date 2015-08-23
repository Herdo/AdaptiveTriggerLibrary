namespace AdaptiveTriggerLibrary.ConditionModifiers.LogicalModifiers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A modifier where only a single value must meet the condition.
    /// If more than one value meets the condition, the condition is not met.
    /// </summary>
    public class XORModifier : ModifierBase,
                               ILogicalModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static bool IsConditionMet(bool condition, IEnumerable<bool> values)
        {
            return values?.Count(m => m == condition) == 1;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region ILogicalModifier

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

            Tuple<bool, bool> singleValueParameters;
            Tuple<bool, IEnumerable<bool>> multiValueParameters;

            // Support multiple values
            var parsed = TryGetParsedParameters(condition, value, out multiValueParameters);
            if (parsed)
                return IsConditionMet(multiValueParameters.Item1, multiValueParameters.Item2);

            // Support single values
            parsed = TryGetParsedParameters(condition, value, out singleValueParameters);
            if (parsed)
                return IsConditionMet(singleValueParameters.Item1, new[] {singleValueParameters.Item2});

            // Cast failed
            throw new InvalidCastException(InvalidCastMessage);
        }

        #endregion
    }
}