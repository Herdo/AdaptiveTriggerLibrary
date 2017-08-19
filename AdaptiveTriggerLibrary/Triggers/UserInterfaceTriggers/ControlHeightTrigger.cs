﻿namespace AdaptiveTriggerLibrary.Triggers.UserInterfaceTriggers
{
    using System;
    using Windows.UI.Xaml;
    using ConditionModifiers.ComparableModifiers;

    /// <summary>
    /// This trigger activates, if the control height
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class ControlHeightTrigger : AdaptiveTriggerBase<double, IComparableModifier>,
                                        IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private FrameworkElement _targetElement;
        private bool _useActualHeight;

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="FrameworkElement"/> to validate the height of.
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
        /// Gets or sets if the <see cref="FrameworkElement.ActualHeight"/> should be used, or the <see cref="FrameworkElement.Height"/>.
        /// Default is true.
        /// </summary>
        public bool UseActualHeight
        {
            get => _useActualHeight;
            set
            {
                if (value == _useActualHeight) return;
                _useActualHeight = value;
                CurrentValue = GetCurrentValue();
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlHeightTrigger"/> class.
        /// Default modifier: <see cref="GreaterThanEqualToModifier"/>.
        /// </summary>
        public ControlHeightTrigger()
            : base(new GreaterThanEqualToModifier())
        {
            _useActualHeight = true;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private double GetCurrentValue()
        {
            return TargetElement == null
                ? Double.NaN
                : UseActualHeight
                    ? TargetElement.ActualHeight
                    : TargetElement.Height;
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