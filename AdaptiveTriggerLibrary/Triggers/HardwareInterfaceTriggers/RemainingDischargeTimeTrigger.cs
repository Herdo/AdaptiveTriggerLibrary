namespace AdaptiveTriggerLibrary.Triggers.HardwareInterfaceTriggers
{
    using System;
    using Windows.System.Power;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current remaining discharge time
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class RemainingDischargeTimeTrigger : AdaptiveTriggerBase<TimeSpan, IComparableModifier>,
                                                 IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="RemainingDischargeTimeTrigger"/> class.
        /// Default modifier: <see cref="LessThanEqualToModifier"/>.
        /// </summary>
        public RemainingDischargeTimeTrigger()
            : base(new LessThanEqualToModifier())
        {
            // Subscribe to state changed event
            PowerManager.RemainingDischargeTimeChanged += PowerManager_RemainingDischargeTimeChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static TimeSpan GetCurrentValue()
        {
            return PowerManager.RemainingDischargeTime;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler
            
        private void PowerManager_RemainingDischargeTimeChanged(object sender, object e)
        {
            CurrentValue = GetCurrentValue();
        }

        #endregion
    }
}