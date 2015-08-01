namespace AdaptiveTriggerLibrary.Triggers.UserInterfaceTriggers
{
    using System;
    using Windows.ApplicationModel.Resources.Core;
    using ConditionModifiers.ComparableModifiers;
    using Enums;

    /// <summary>
    /// This trigger activates, if the current scale
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class LayoutDirectionTrigger : AdaptiveTriggerBase<LayoutDirection, IComparableModifier>
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="LayoutDirectionTrigger"/> class.
        /// Default modifier: <see cref="EqualToModifier"/>.
        /// </summary>
        public LayoutDirectionTrigger()
            : base(new EqualToModifier())
        {
            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private LayoutDirection GetCurrentValue()
        {
            var result = LayoutDirection.LTR;
            var qualifiers = ResourceContext.GetForCurrentView().QualifierValues;
            if (qualifiers.ContainsKey("LayoutDirection"))
            {
                LayoutDirection parsed;
                if (Enum.TryParse(qualifiers["LayoutDirection"], out parsed))
                    result = parsed;
            }
            return result;
        }

        #endregion
    }
}