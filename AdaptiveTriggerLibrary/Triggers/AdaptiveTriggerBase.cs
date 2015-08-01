namespace AdaptiveTriggerLibrary.Triggers
{
    using System;
    using Windows.UI.Xaml;
    using ConditionModifiers;

    /// <summary>
    /// Base class for all adaptive triggers in the project.
    /// </summary>
    /// <typeparam name="TCondition">The type of the <see cref="Condition"/>.</typeparam>
    /// <typeparam name="TConditionModifier">The type of the <see cref="ConditionModifier"/>, that can influence the way that the <see cref="Condition"/> is treated.</typeparam>
    public abstract class AdaptiveTriggerBase<TCondition, TConditionModifier> : StateTriggerBase,
                                                                                IAdaptiveTrigger<TCondition, TConditionModifier>
        where TConditionModifier : IConditionModifier
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private readonly bool _initialConditionProvided;
        private bool _isConditionSet;
        private bool _isConditionModifierSet;
        private bool _isCurrentValueSet;

        private TCondition _currentValue;
        private bool _isActive;
        private TCondition _condition;
        private TConditionModifier _conditionModifier;

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// Sets the current value.
        /// </summary>
        /// <exception cref="InvalidOperationException"><see cref="Condition"/> is not set and no static value is provided.</exception>
        protected TCondition CurrentValue
        {
            private get { return _currentValue; }
            set
            {
                _currentValue = value;
                _isCurrentValueSet = true;
                ValidateIfActive();
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveTriggerBase{TCondition, TConditionModifier}"/> class with a given default modifier.
        /// </summary>
        /// <param name="defaultModifier">The default modifier.</param>
        protected AdaptiveTriggerBase(TConditionModifier defaultModifier)
        {
            _initialConditionProvided = false;
            _isActive = false;
            _condition = default(TCondition);
            _conditionModifier = defaultModifier;
            _isConditionSet =
                _isConditionModifierSet =
                    _isCurrentValueSet = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveTriggerBase{TCondition, TConditionModifier}"/> class with a given default condition and default modifier.
        /// </summary>
        /// <param name="defaultCondition">The default condition.</param>
        /// <param name="defaultModifier">The default modifier.</param>
        protected AdaptiveTriggerBase(TCondition defaultCondition, TConditionModifier defaultModifier)
            : this(defaultModifier)
        {
            _condition = defaultCondition;
            _initialConditionProvided = true;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private void ValidateIfActive()
        {
            if ((!_initialConditionProvided && !_isConditionSet)
             || !_isCurrentValueSet)
                return;

            IsActive = ConditionModifier.IsConditionMet(Condition, CurrentValue);
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region IAdaptiveTrigger<TCondition, TConditionModifier> Members

        /// <summary>
        /// Occurs when the value of <see cref="IAdaptiveTrigger.IsActive"/> changed.
        /// </summary>
        public event EventHandler IsActiveChanged;

        /// <summary>
        /// Gets if the trigger is currently active.
        /// </summary>
        public bool IsActive
        {
            get { return _isActive; }
            private set
            {
                if (value == _isActive) return;
                _isActive = value;
                SetActive(IsActive);
                IsActiveChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the condition that must be met, in order to set <see cref="IAdaptiveTrigger.IsActive"/> to true.
        /// </summary>
        /// <remarks>This property can only be set once.</remarks>
        /// <exception cref="InvalidOperationException"><see cref="IAdaptiveTrigger{TCondition,TConditionModifier}.Condition"/> is set more than once.</exception>
        public TCondition Condition
        {
            get { return _condition; }
            set
            {
                if (_isConditionSet)
                    throw new InvalidOperationException("Condition has already been set.");
                _condition = value;
                _isConditionSet = true;
                ValidateIfActive();
            }
        }

        /// <summary>
        /// Gets or sets the modifier that will be applied to the validation of the <see cref="IAdaptiveTrigger{TCondition,TConditionModifier}.Condition"/>.
        /// </summary>
        /// <remarks>This property can only be set once.
        /// If no condition modifier is specified, the default modifier for the <typeparamref name="TConditionModifier"/> will be used.</remarks>
        /// <exception cref="InvalidOperationException"><see cref="IAdaptiveTrigger{TCondition,TConditionModifier}.ConditionModifier"/> is set more than once.</exception>
        public TConditionModifier ConditionModifier
        {
            get { return _conditionModifier; }
            set
            {
                if (_isConditionModifierSet)
                    throw new InvalidOperationException("ConditionModifier has already been set.");
                _conditionModifier = value;
                _isConditionModifierSet = true;
            }
        }

        #endregion
    }
}