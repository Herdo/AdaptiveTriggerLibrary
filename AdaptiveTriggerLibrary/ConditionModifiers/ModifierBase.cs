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

        private bool _treatNullAsConditionMet;
        private bool _treatNullAsConditionNotMet;

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

        protected ModifierBase()
        {
            _treatNullAsConditionMet = false;
            _treatNullAsConditionNotMet = false;
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
            get { return _treatNullAsConditionMet; }
            set
            {
                if (value == _treatNullAsConditionMet) return;
                if (value && _treatNullAsConditionNotMet)
                    throw new InvalidOperationException(
                        "Cannot set 'IConditionModifier.TreatNullAsConditionMet' to true, while IConditionModifier.TreatNullAsConditionNotMet is already true.");
                _treatNullAsConditionMet = value;
            }
        }

        bool IConditionModifier.TreatNullAsConditionNotMet
        {
            get { return _treatNullAsConditionNotMet; }
            set
            {
                if (value == _treatNullAsConditionNotMet) return;
                if (value && _treatNullAsConditionMet)
                    throw new InvalidOperationException(
                        "Cannot set 'IConditionModifier.TreatNullAsConditionNotMet' to true, while IConditionModifier.TreatNullAsConditionMet is already true.");
                _treatNullAsConditionNotMet = value;
            }
        }

        public abstract bool IsConditionMet(object condition, object value);

        #endregion
    }
}