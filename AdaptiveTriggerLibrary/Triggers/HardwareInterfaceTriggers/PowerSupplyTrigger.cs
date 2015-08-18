namespace AdaptiveTriggerLibrary.Triggers.HardwareInterfaceTriggers
{
    using Windows.System.Power;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current power supply
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class PowerSupplyTrigger : AdaptiveTriggerBase<PowerSupplyStatus, IComparableModifier>,
                                      IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerSupplyTrigger"/> class.
        /// Default modifier: <see cref="EqualToModifier"/>.
        /// </summary>
        public PowerSupplyTrigger()
            : base(new EqualToModifier())
        {
            // Subscribe to state changed event
            PowerManager.PowerSupplyStatusChanged += PowerManager_PowerSupplyStatusChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static PowerSupplyStatus GetCurrentValue()
        {
            return PowerManager.PowerSupplyStatus;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler
            
        private void PowerManager_PowerSupplyStatusChanged(object sender, object e)
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
            PowerManager.PowerSupplyStatusChanged -= PowerManager_PowerSupplyStatusChanged;
        }

        void IDynamicTrigger.ResumeUpdates()
        {
            PowerManager.PowerSupplyStatusChanged += PowerManager_PowerSupplyStatusChanged;
        }

        #endregion
    }
}