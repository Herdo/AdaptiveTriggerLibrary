namespace AdaptiveTriggerLibrary.Triggers.ConnectivityTriggers
{
    using Windows.Networking.Connectivity;
    using ConditionModifiers.GenericModifiers;

    /// <summary>
    /// This trigger activates, if a network connection state
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class NetworkConnectionStateTrigger : AdaptiveTriggerBase<NetworkConnectivityLevel, IGenericModifier>,
                                                 IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkConnectionStateTrigger"/> class.
        /// Default condition: <see cref="NetworkConnectivityLevel.InternetAccess"/>.
        /// Default modifier: <see cref="EqualsModifier{NetworkConnectivityLevel}"/>.
        /// </summary>
        public NetworkConnectionStateTrigger()
            : base(NetworkConnectivityLevel.InternetAccess, new EqualsModifier<NetworkConnectivityLevel>())
        {
            // Subscribe to state changed event
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private NetworkConnectivityLevel GetCurrentValue()
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            return profile?.GetNetworkConnectivityLevel() ?? NetworkConnectivityLevel.None;
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