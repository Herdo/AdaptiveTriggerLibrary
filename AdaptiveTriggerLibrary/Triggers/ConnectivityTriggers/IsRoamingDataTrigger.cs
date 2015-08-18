namespace AdaptiveTriggerLibrary.Triggers.ConnectivityTriggers
{
    using Windows.Networking.Connectivity;
    using ConditionModifiers.GenericModifiers;

    /// <summary>
    /// This trigger activates, if the connection being established is outside of the home provider
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class IsRoamingDataTrigger : AdaptiveTriggerBase<bool, IGenericModifier>,
                                        IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IsRoamingDataTrigger"/> class.
        /// Default modifier: <see cref="EqualsModifier{Boolean}"/>.
        /// </summary>
        public IsRoamingDataTrigger()
            : base(new EqualsModifier<bool>())
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
            return profile?.GetConnectionCost().Roaming ?? false;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler

        private void NetworkInformation_NetworkStatusChanged(object sender)
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
            NetworkInformation.NetworkStatusChanged -= NetworkInformation_NetworkStatusChanged;
        }

        void IDynamicTrigger.ResumeUpdates()
        {
            NetworkInformation.NetworkStatusChanged += NetworkInformation_NetworkStatusChanged;
        }

        #endregion
    }
}