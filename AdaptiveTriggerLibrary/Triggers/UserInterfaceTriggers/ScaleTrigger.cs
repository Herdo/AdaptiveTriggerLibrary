namespace AdaptiveTriggerLibrary.Triggers.UserInterfaceTriggers
{
    using Windows.ApplicationModel.Resources.Core;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current scale
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class ScaleTrigger : AdaptiveTriggerBase<int, IComparableModifier>
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ScaleTrigger"/> class.
        /// Default modifier: <see cref="EqualToModifier"/>.
        /// </summary>
        /// <remarks>Possible values for <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>,
        /// if <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.ConditionModifier"/> is <see cref="EqualToModifier"/>:
        /// 80
        /// 100
        /// 120
        /// 125 (Windows 10 only)
        /// 140
        /// 150 (Windows 10 only)
        /// 160
        /// 175 (Windows 10 only)
        /// 180
        /// 200
        /// 225 (Windows 10 only)
        /// 250 (Windows 10 only)
        /// 300 (Windows 10 only)
        /// 350 (Windows 10 only)
        /// 400 (Windows 10 only)
        /// 450 (Windows 10 only)</remarks>
        public ScaleTrigger()
            : base(new EqualToModifier())
        {
            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private int GetCurrentValue()
        {
            var result = 100;
            var qualifiers = ResourceContext.GetForCurrentView().QualifierValues;
            if (qualifiers.ContainsKey("Scale"))
            {
                int parsed;
                if (int.TryParse(qualifiers["Scale"], out parsed))
                    result = parsed;
            }
            return result;
        }

        #endregion
    }
}