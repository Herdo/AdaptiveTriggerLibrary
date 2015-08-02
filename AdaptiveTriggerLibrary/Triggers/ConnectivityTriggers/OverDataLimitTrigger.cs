namespace AdaptiveTriggerLibrary.Triggers.ConnectivityTriggers
{
    using Windows.Networking.Connectivity;
    using ConditionModifiers.GenericModifiers;

    /// <summary>
    /// This trigger activates, if the connection being over to the data limit
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class OverDataLimitTrigger : AdaptiveTriggerBase<bool, IGenericModifier>
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OverDataLimitTrigger"/> class.
        /// Default modifier: <see cref="EqualsModifier{Boolean}"/>.
        /// </summary>
        public OverDataLimitTrigger()
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
            return profile?.GetConnectionCost().OverDataLimit ?? false;
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