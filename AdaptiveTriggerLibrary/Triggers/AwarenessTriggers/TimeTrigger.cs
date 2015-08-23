namespace AdaptiveTriggerLibrary.Triggers.AwarenessTriggers
{
    using System;
    using Windows.UI.Xaml;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the current time of the day
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class TimeTrigger : AdaptiveTriggerBase<TimeSpan, IComparableModifier>,
        IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private static bool _useSharedTimer;
        private static DispatcherTimer _sharedTimer;
        private static TimeSpan _sharedTimerInterval;

        private readonly bool _useSharedTimerInInstance;
        private readonly DispatcherTimer _timer;
        private TimeSpan _timerInterval;
        private bool _useUtc;

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// Gets or sets if a shared timer for all instances shall be used, 
        /// rather than timer for each instance.
        /// </summary>
        /// <remarks>Default is false.
        /// Triggers created before this property is changed won't be affected by the change.</remarks>
        public static bool UseSharedTimer
        {
            get { return _useSharedTimer; }
            set
            {
                if (value == _useSharedTimer) return;
                _useSharedTimer = value;
                if (_useSharedTimer && _sharedTimer == null)
                {
                    _sharedTimer = new DispatcherTimer
                    {
                        Interval = SharedTimerInterval
                    };
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="TimeSpan"/> in which the time will be updated.
        /// </summary>
        /// <remarks>Default is 10 seconds.</remarks>
        public static TimeSpan SharedTimerInterval
        {
            get { return _sharedTimerInterval; }
            set
            {
                if (value == _sharedTimerInterval) return;
                _sharedTimerInterval = value;

                if (_sharedTimer == null) return;
                _sharedTimer.Stop();
                _sharedTimer.Interval = _sharedTimerInterval;
                _sharedTimer.Start();
            }
        }

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

        static TimeTrigger()
        {
            UseSharedTimer = false;
            SharedTimerInterval = new TimeSpan(0, 0, 10);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeTrigger"/> class.
        /// Default modifier: <see cref="GreaterThanEqualToModifier"/>.
        /// </summary>
        public TimeTrigger()
            : base(new GreaterThanEqualToModifier())
        {
            _useSharedTimerInInstance = UseSharedTimer;
            _useUtc = false;

            if (_useSharedTimerInInstance)
            {
                // Subscribe to state changed events
                _sharedTimer.Tick += Timer_Tick;
            }
            else
            {
                _timer = new DispatcherTimer();
                TimerInterval = new TimeSpan(0, 0, 10);

                // Subscribe to state changed events
                _timer.Tick += Timer_Tick;
            }


            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private TimeSpan GetCurrentValue()
        {
            var now = UseUTC
                ? DateTime.UtcNow
                : DateTime.Now;

            return new TimeSpan(now.Hour, now.Minute, now.Second);
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
            if (_useSharedTimerInInstance)
                _sharedTimer.Tick -= Timer_Tick;
            else
                _timer.Tick -= Timer_Tick;
        }

        void IDynamicTrigger.ResumeUpdates()
        {
            if (_useSharedTimerInInstance)
                _sharedTimer.Tick += Timer_Tick;
            else
                _timer.Tick += Timer_Tick;
        }

        #endregion
    }
}