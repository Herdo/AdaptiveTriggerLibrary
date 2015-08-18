namespace AdaptiveTriggerLibrary.Triggers.HardwareInterfaceTriggers
{
    using Windows.System.Power;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current battery status
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class BatteryStatusTrigger : AdaptiveTriggerBase<BatteryStatus, IComparableModifier>,
                                        IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="BatteryStatusTrigger"/> class.
        /// Default modifier: <see cref="EqualToModifier"/>.
        /// </summary>
        public BatteryStatusTrigger()
            : base(new EqualToModifier())
        {
            // Subscribe to state changed event
            PowerManager.BatteryStatusChanged += PowerManager_BatteryStatusChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static BatteryStatus GetCurrentValue()
        {
            return PowerManager.BatteryStatus;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler
            
        private void PowerManager_BatteryStatusChanged(object sender, object e)
        {
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region IDynamicTrigger Members

        void IDynamicTrigger.ForceValidation()
        {
            CurrentValue = GetCurrentValue();
        }

        void IDynamicTrigger.SuspendUpdates()
        {
            PowerManager.BatteryStatusChanged -= PowerManager_BatteryStatusChanged;
        }

        void IDynamicTrigger.ResumeUpdates()
        {
            PowerManager.BatteryStatusChanged += PowerManager_BatteryStatusChanged;
        }

        #endregion
    }
}