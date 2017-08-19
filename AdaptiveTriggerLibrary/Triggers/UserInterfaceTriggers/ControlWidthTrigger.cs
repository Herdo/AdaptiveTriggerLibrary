namespace AdaptiveTriggerLibrary.Triggers.UserInterfaceTriggers
{
    using System;
    using Windows.UI.Xaml;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the control width
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class ControlWidthTrigger : AdaptiveTriggerBase<double, IComparableModifier>,
                                       IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private FrameworkElement _targetElement;
        private bool _useActualWidth;

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="FrameworkElement"/> to validate the width of.
        /// </summary>
        public FrameworkElement TargetElement
        {
            get => _targetElement;
            set
            {
                if (value == _targetElement) return;

                if (_targetElement != null)
                    _targetElement.SizeChanged -= TargetElement_SizeChanged;

                _targetElement = value;

                if (_targetElement != null)
                    _targetElement.SizeChanged += TargetElement_SizeChanged;

                CurrentValue = GetCurrentValue();
            }
        }

        /// <summary>
        /// Gets or sets if the <see cref="FrameworkElement.ActualWidth"/> should be used, or the <see cref="FrameworkElement.Width"/>.
        /// Default is true.
        /// </summary>
        public bool UseActualWidth
        {
            get => _useActualWidth;
            set
            {
                if (value == _useActualWidth) return;
                _useActualWidth = value;
                CurrentValue = GetCurrentValue();
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlWidthTrigger"/> class.
        /// Default modifier: <see cref="GreaterThanEqualToModifier"/>.
        /// </summary>
        public ControlWidthTrigger()
            : base(new GreaterThanEqualToModifier())
        {
            _useActualWidth = true;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private double GetCurrentValue()
        {
            return TargetElement == null
                ? Double.NaN
                : UseActualWidth
                    ? TargetElement.ActualWidth
                    : TargetElement.Width;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler

        private void TargetElement_SizeChanged(object sender, SizeChangedEventArgs e)
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
            if (_targetElement != null)
                _targetElement.SizeChanged -= TargetElement_SizeChanged;
        }

        void IDynamicTrigger.ResumeUpdates()
        {
            if (_targetElement != null)
                _targetElement.SizeChanged += TargetElement_SizeChanged;
        }

        #endregion
    }
}