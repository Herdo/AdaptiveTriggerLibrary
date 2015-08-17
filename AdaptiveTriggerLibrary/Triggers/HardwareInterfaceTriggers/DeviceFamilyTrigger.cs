namespace AdaptiveTriggerLibrary.Triggers.HardwareInterfaceTriggers
{
    using Windows.ApplicationModel.Resources.Core;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current device family
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class DeviceFamilyTrigger : AdaptiveTriggerBase<string, IComparableModifier>,
                                       IStaticTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceFamilyTrigger"/> class.
        /// Default modifier: <see cref="EqualToModifier"/>.
        /// </summary>
        public DeviceFamilyTrigger()
            : base(new EqualToModifier())
        {
            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static string GetCurrentValue()
        {
            var qualifiers = ResourceContext.GetForCurrentView().QualifierValues;
            return qualifiers.ContainsKey("DeviceFamily")
                ? qualifiers["DeviceFamily"]
                : null;
        }

        #endregion
    }
}