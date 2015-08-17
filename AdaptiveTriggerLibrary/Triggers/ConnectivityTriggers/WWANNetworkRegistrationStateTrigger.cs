namespace AdaptiveTriggerLibrary.Triggers.ConnectivityTriggers
{
    using Windows.Networking.Connectivity;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current WWAN network registration state
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class WWANNetworkRegistrationStateTrigger : AdaptiveTriggerBase<WwanNetworkRegistrationState, IComparableModifier>,
                                                       IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WWANNetworkRegistrationStateTrigger"/> class.
        /// Default modifier: <see cref="EqualToModifier"/>.
        /// </summary>
        public WWANNetworkRegistrationStateTrigger()
            : base(new EqualToModifier())
        {
            // Subscribe to state changed event
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static WwanNetworkRegistrationState GetCurrentValue()
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            return profile?.WwanConnectionProfileDetails?.GetNetworkRegistrationState() ?? default(WwanNetworkRegistrationState);
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler
            
        private void NetworkInformation_NetworkStatusChanged(object sender)
        {
            CurrentValue = GetCurrentValue();
        }

        #endregion
    }
}