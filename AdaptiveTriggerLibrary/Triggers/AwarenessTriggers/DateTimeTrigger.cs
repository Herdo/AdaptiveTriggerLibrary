namespace AdaptiveTriggerLibrary.Triggers.AwarenessTriggers
{
    using System;
    using Windows.UI.Xaml;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current date and time
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class DateTimeTrigger : AdaptiveTriggerBase<DateTime, IComparableModifier>,
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
        /// <remarks>Default is 10 seconds.</remarks>
        public TimeSpan TimerInterval
        {
            get { return _timerInterval; }
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
            get { return _useUtc; }
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
        /// Initializes a new instance of the <see cref="DateTimeTrigger"/> class.
        /// Default modifier: <see cref="GreaterThanEqualToModifier"/>.
        /// </summary>
        public DateTimeTrigger()
            : base(new GreaterThanEqualToModifier())
        {
            _timer = new DispatcherTimer();
            _useUtc = false;
            TimerInterval = new TimeSpan(0, 0, 10);

            // Subscribe to state changed events
            _timer.Tick += Timer_Tick;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private DateTime GetCurrentValue()
        {
            return UseUTC
                ? DateTime.UtcNow
                : DateTime.Now;
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