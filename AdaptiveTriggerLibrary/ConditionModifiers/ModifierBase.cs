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

        protected const string InvalidCastMessage =
            "Parameters could not be parsed to a type that the method can handle.";

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Protected Methods

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