namespace AdaptiveTriggerLibrary.Triggers.HardwareInterfaceTriggers
{
    using ConditionModifiers.ComparableModifiers;

    public class DeviceFamilyTrigger : AdaptiveTriggerBase<string>
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        public DeviceFamilyTrigger()
            : base(GetStaticValue(), new EqualToModifier())
        {
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static string GetStaticValue()
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