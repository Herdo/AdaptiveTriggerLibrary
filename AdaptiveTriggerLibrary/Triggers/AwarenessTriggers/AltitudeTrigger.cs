namespace AdaptiveTriggerLibrary.Triggers.AwarenessTriggers
{
    using System;
    using System.Threading.Tasks;
    using Windows.Devices.Geolocation;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current altitude
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class AltitudeTrigger : AdaptiveTriggerBase<double?, IComparableModifier>,
        IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private readonly Geolocator _geolocator;

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AltitudeTrigger"/> class.
        /// Default modifier: <see cref="GreaterThanEqualToModifier"/>.
        /// </summary>
        public AltitudeTrigger()
            : base(new GreaterThanEqualToModifier())
        {
            _geolocator = new Geolocator();

            // Subscribe to state changed events
            _geolocator.PositionChanged += Geolocator_PositionChanged;
            _geolocator.StatusChanged += Geolocator_StatusChanged;

            // Set initial value
            CurrentValue = GetCurrentValue().Result;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private async Task<double?> GetCurrentValue()
        {
            var pos = await _geolocator.GetGeopositionAsync();
            return pos.Coordinate.Point.Position.Altitude;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler
            
        private async void Geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            CurrentValue = await GetCurrentValue();
        }

        private async void Geolocator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            switch (args.Status)
            {
                case PositionStatus.Ready:
                    CurrentValue = await GetCurrentValue();
                    break;
                case PositionStatus.Initializing:
                case PositionStatus.NoData:
                case PositionStatus.Disabled:
                case PositionStatus.NotInitialized:
                case PositionStatus.NotAvailable:
                    CurrentValue = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region IDynamicTrigger Members

        async void IDynamicTrigger.ForceValidation()
        {
            CurrentValue = await GetCurrentValue();
        }

        void IDynamicTrigger.SuspendUpdates()
        {
            _geolocator.PositionChanged -= Geolocator_PositionChanged;
        }

        void IDynamicTrigger.ResumeUpdates()
        {
            _geolocator.PositionChanged += Geolocator_PositionChanged;
        }

        #endregion
    }
}