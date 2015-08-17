namespace AdaptiveTriggerLibrary.Triggers.ConnectivityTriggers
{
    using Windows.Networking.Connectivity;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the network authentication type
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class NetworkAuthenticationTypeTrigger : AdaptiveTriggerBase<NetworkAuthenticationType, IComparableModifier>,
                                                    IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NetworkAuthenticationTypeTrigger"/> class.
        /// Default modifier: <see cref="EqualToModifier"/>.
        /// </summary>
        public NetworkAuthenticationTypeTrigger()
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

        private static NetworkAuthenticationType GetCurrentValue()
        {
            var profile = NetworkInformation.GetInternetConnectionProfile();
            return profile?.NetworkSecuritySettings.NetworkAuthenticationType ?? default(NetworkAuthenticationType);
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