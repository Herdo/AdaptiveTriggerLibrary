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
        where TConditionModifier : IConditionModifier<TCondition>
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

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
            _isActive = false;
            _condition = default(TCondition);
            _conditionModifier = defaultModifier;
            _isConditionSet =
                _isConditionModifierSet =
                    _isCurrentValueSet = false;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods
            
        private void ValidateIfActive()
        {
            if (!_isConditionSet
             || !_isCurrentValueSet)
                return;

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
                _condition = value;
                _isConditionSet = true;
                ValidateIfActive();
            }
        }

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