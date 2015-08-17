namespace AdaptiveTriggerLibrary.Triggers.HardwareInterfaceTriggers
{
    using Windows.System.Power;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current remaining charge percent
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class RemainingChargePercentTrigger : AdaptiveTriggerBase<int, IComparableModifier>,
                                                 IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RemainingChargePercentTrigger"/> class.
        /// Default modifier: <see cref="LessThanEqualToModifier"/>.
        /// </summary>
        public RemainingChargePercentTrigger()
            : base(new LessThanEqualToModifier())
        {
            // Subscribe to state changed event
            PowerManager.RemainingChargePercentChanged += PowerManager_RemainingChargePercentChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static int GetCurrentValue()
        {
            return PowerManager.RemainingChargePercent;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler
            
        private void PowerManager_RemainingChargePercentChanged(object sender, object e)
        {
            CurrentValue = GetCurrentValue();
        }

        #endregion
    }
}