namespace AdaptiveTriggerLibrary.Triggers.HardwareInterfaceTriggers
{
    using ConditionModifiers.ComparableModifiers;

    public class DeviceFamilyTrigger : AdaptiveTriggerBase<string, IComparableModifier>
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        public DeviceFamilyTrigger()
            : base(new EqualToModifier())
        {
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static string GetCurrentValue()
        {
            var qualifiers = Windows.ApplicationModel.Resources
                                    .Core.ResourceContext
                                    .GetForCurrentView().QualifierValues;
            return qualifiers.ContainsKey("DeviceFamily")
                ? qualifiers["DeviceFamily"]
                : null;
        }

        #endregion
    }
}