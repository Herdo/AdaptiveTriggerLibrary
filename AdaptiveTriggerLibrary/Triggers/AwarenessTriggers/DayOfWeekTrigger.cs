namespace AdaptiveTriggerLibrary.Triggers.AwarenessTriggers
{
    using System;
    using Windows.UI.Xaml;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current day of the week
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class DayOfWeekTrigger : AdaptiveTriggerBase<DayOfWeek, IComparableModifier>,
        IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private readonly DispatcherTimer _timer;
        private TimeSpan _timerInterval;
        private bool _useUtc;

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="TimeSpan"/> in which the time will be updated.
        /// </summary>
        /// <remarks>Default is 1 hour.</remarks>
        public TimeSpan TimerInterval
        {
            get => _timerInterval;
            set
            {
                if (value == _timerInterval) return;
                _timerInterval = value;
                _timer.Stop();
                _timer.Interval = _timerInterval;
                _timer.Start();
            }
        }

        /// <summary>
        /// Gets or sets wether to use UTC time or the local time.
        /// </summary>
        public bool UseUTC
        {
            get => _useUtc;
            set
            {
                if (value == _useUtc) return;
                _useUtc = value;
                CurrentValue = GetCurrentValue();
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DayOfWeekTrigger"/> class.
        /// Default modifier: <see cref="EqualToModifier"/>.
        /// </summary>
        public DayOfWeekTrigger()
            : base(new EqualToModifier())
        {
            _timer = new DispatcherTimer();
            _useUtc = false;
            TimerInterval = new TimeSpan(1, 0, 0);

            // Subscribe to state changed events
            _timer.Tick += Timer_Tick;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private DayOfWeek GetCurrentValue()
        {
            return UseUTC
                ? DateTime.UtcNow.DayOfWeek
                : DateTime.Now.DayOfWeek;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler

        private void Timer_Tick(object sender, object e)
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
            _timer.Tick -= Timer_Tick;
        }

        void IDynamicTrigger.ResumeUpdates()
        {
            _timer.Tick += Timer_Tick;
        }

        #endregion
    }
}