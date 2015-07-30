namespace AdaptiveTriggerLibrary.Triggers
{
    using System;
    using Windows.UI.Xaml;
    using Interfaces;

    /// <summary>
    /// Base class for all adaptive triggers in the project.
    /// </summary>
    /// <typeparam name="TCondition">The type of the <see cref="Condition"/>.</typeparam>
    public abstract class AdaptiveTriggerBase<TCondition> : StateTriggerBase,
                                                            IAdaptiveTrigger<TCondition>
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private readonly bool _staticValueProvided;
        private bool _isConditionSet;
        private bool _isConditionModifierSet;

        private TCondition _currentValue;
        private bool _isActive;
        private TCondition _condition;
        private IConditionModifier<TCondition> _conditionModifier;

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
                if (ReferenceEquals(value, _currentValue)
                 || Equals(value, _currentValue)) return;

                _currentValue = value;
                ValidateIfActive();
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveTriggerBase{TCondition}"/> class with a given default modifier.
        /// </summary>
        /// <param name="defaultModifier">The default modifier.</param>
        protected AdaptiveTriggerBase(IConditionModifier<TCondition> defaultModifier)
        {
            _staticValueProvided = false;
            _isActive = false;
            _condition = default(TCondition);
            _conditionModifier = defaultModifier;
            _isConditionSet =
                _isConditionModifierSet = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdaptiveTriggerBase{TCondition}"/> class with a static value and a given default modifier.
        /// </summary>
        /// <param name="staticValue">A static value for <see cref="CurrentValue"/>.</param>
        /// <param name="defaultModifier">The default modifier.</param>
        protected AdaptiveTriggerBase(TCondition staticValue, IConditionModifier<TCondition> defaultModifier)
            : this(defaultModifier)
        {
            CurrentValue = staticValue;
            _staticValueProvided = true;
        }  

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods
            
        private void ValidateIfActive()
        {
            if (!_isConditionSet)
            {
                if (_staticValueProvided)
                    // If a static value is set, it will be evaluated as soon as the condition is set.
                    return;
                throw new InvalidOperationException("Condition is not set.");
            }

            IsActive = ConditionModifier.IsConditionMet(CurrentValue, Condition);
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region IAdaptiveTrigger<TCondition, TConditionModifier> Members

        public event EventHandler IsActiveChanged;

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

        public TCondition Condition
        {
            get { return _condition; }
            set
            {
                if (_isConditionSet)
                    throw new InvalidOperationException("Condition has already been set.");
                _isConditionSet = true;
                _condition = value;
                if (_staticValueProvided)
                    ValidateIfActive();
            }
        }

        public IConditionModifier<TCondition> ConditionModifier
        {
            get { return _conditionModifier; }
            set
            {
                if (_isConditionModifierSet)
                    throw new InvalidOperationException("ConditionModifier has already been set.");
                _isConditionModifierSet = true;
                _conditionModifier = value;
            }
        }

        #endregion
    }
}