namespace AdaptiveTriggerLibrary.ConditionModifiers
{
    using System;

    /// <summary>
    /// Base class for modifiers.
    /// </summary>
    public abstract class ModifierBase : IConditionModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        /// <summary>
        /// Backing field for derived classes accessing <see cref="IConditionModifier.TreatNullAsConditionMet"/>.
        /// </summary>
        protected bool TreatNullAsConditionIsMet;
        /// <summary>
        /// Backing field for derived classes accessing <see cref="IConditionModifier.TreatNullAsConditionNotMet"/>.
        /// </summary>
        protected bool TreatNullAsConditionIsNotMet;

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// Message that will be displayed when all calls to <see cref="TryGetParsedParameters{TConditionType,TValueType}"/> fail.
        /// </summary>
        protected const string InvalidCastMessage =
            "Parameters could not be parsed to a type that the method can handle.";

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Creates a new instance of the <see cref="ModifierBase"/> class.
        /// </summary>
        protected ModifierBase()
        {
            TreatNullAsConditionIsMet = false;
            TreatNullAsConditionIsNotMet = false;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Protected Methods

        /// <summary>
        /// Trys to parse the <paramref name="condition"/> and <paramref name="value"/>
        /// into the underlying types <typeparamref name="TConditionType"/> and <typeparamref name="TValueType"/>.
        ///  </summary>
        /// <typeparam name="TConditionType">The underlying type for <paramref name="condition"/>.</typeparam>
        /// <typeparam name="TValueType">The underlying type for <paramref name="value"/>.</typeparam>
        /// <param name="condition">The condition to parse.</param>
        /// <param name="value">The value to parse.</param>
        /// <param name="parsedParameters">The parsed objects on success, otherwise null.</param>
        /// <returns>True, if the parsing was successfull, otherwise false.</returns>
        protected bool TryGetParsedParameters<TConditionType, TValueType>(object condition, object value, out Tuple<TConditionType, TValueType> parsedParameters)
        {
            var conditionConvertible = condition is TConditionType;
            var valueConvertible = value is TValueType;

            if (conditionConvertible && valueConvertible)
            {
                parsedParameters = new Tuple<TConditionType, TValueType>((TConditionType)condition, (TValueType)value);
                return true;
            }
            parsedParameters = null;
            return false;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region IConditionModifier Members

        bool IConditionModifier.TreatNullAsConditionMet
        {
            get { return TreatNullAsConditionIsMet; }
            set
            {
                if (value == TreatNullAsConditionIsMet) return;
                if (value && TreatNullAsConditionIsNotMet)
                    throw new InvalidOperationException(
                        "Cannot set 'IConditionModifier.TreatNullAsConditionMet' to true, while IConditionModifier.TreatNullAsConditionNotMet is already true.");
                TreatNullAsConditionIsMet = value;
            }
        }

        bool IConditionModifier.TreatNullAsConditionNotMet
        {
            get { return TreatNullAsConditionIsNotMet; }
            set
            {
                if (value == TreatNullAsConditionIsNotMet) return;
                if (value && TreatNullAsConditionIsMet)
                    throw new InvalidOperationException(
                        "Cannot set 'IConditionModifier.TreatNullAsConditionNotMet' to true, while IConditionModifier.TreatNullAsConditionMet is already true.");
                TreatNullAsConditionIsNotMet = value;
            }
        }

        /// <summary>
        /// Checks if the <paramref name="value"/> meets the specified <paramref name="condition"/>.
        /// </summary>
        /// <param name="condition">The condition.</param>
        /// <param name="value">The actual value.</param>
        /// <exception cref="ArgumentException">The underlying type of <paramref name="condition"/> doesn't match expected condition type,
        /// or the underlying type of<paramref name="value"/> doesn't match the expected value type.</exception>
        /// <exception cref="InvalidCastException">Either <paramref name="condition"/> or <paramref name="value"/> cannot be casted to the specified underlying type.</exception>
        /// <returns>True, if the <paramref name="value"/> meets the specified <paramref name="condition"/>, otherwise false.</returns>
        public abstract bool IsConditionMet(object condition, object value);

        #endregion
    }
}