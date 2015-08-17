namespace AdaptiveTriggerLibrary.Triggers.ConnectivityTriggers
{
    using Windows.Networking.Connectivity;
    using ConditionModifiers.GenericModifiers;

    /// <summary>
    /// This trigger activates, if the current internet connection profile being a WWAN (mobile) connection profile
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class WWANConnectionAvailableTrigger : AdaptiveTriggerBase<bool, IGenericModifier>,
                                                  IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WWANConnectionAvailableTrigger"/> class.
        /// Default condition: true.
        /// Default modifier: <see cref="EqualsModifier{Boolean}"/>.
        /// </summary>
        public WWANConnectionAvailableTrigger()
            : base(true, new EqualsModifier<bool>())
        {
            // Subscribe to state changed event
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static bool GetCurrentValue()
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            return profile?.IsWwanConnectionProfile ?? false;
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