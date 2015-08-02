namespace AdaptiveTriggerLibrary.ConditionModifiers
{
    using System;

    /// <summary>
    /// Base class for modifiers.
    /// </summary>
    public abstract class ModifierBase
    {
        ///////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// Message that will be displayed when all calls to <see cref="TryGetParsedParameters{TConditionType,TValueType}"/> fail.
        /// </summary>
        protected const string InvalidCastMessage =
            "Parameters could not be parsed to a type that the method can handle.";

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
    }
}