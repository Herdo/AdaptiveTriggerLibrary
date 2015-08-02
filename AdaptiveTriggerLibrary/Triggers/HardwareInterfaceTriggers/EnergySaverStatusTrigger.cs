namespace AdaptiveTriggerLibrary.Triggers.HardwareInterfaceTriggers
{
    using Windows.System.Power;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current energy saver status
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class EnergySaveStatusTrigger : AdaptiveTriggerBase<EnergySaverStatus, IComparableModifier>
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnergySaveStatusTrigger"/> class.
        /// Default modifier: <see cref="EqualToModifier"/>.
        /// </summary>
        public EnergySaveStatusTrigger()
            : base(new EqualToModifier())
        {
            // Subscribe to state changed event
            PowerManager.EnergySaverStatusChanged += PowerManager_EnergySaverStatusChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static EnergySaverStatus GetCurrentValue()
        {
            return PowerManager.EnergySaverStatus;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler
            
        private void PowerManager_EnergySaverStatusChanged(object sender, object e)
        {
            CurrentValue = GetCurrentValue();
        }

        #endregion
    }
}