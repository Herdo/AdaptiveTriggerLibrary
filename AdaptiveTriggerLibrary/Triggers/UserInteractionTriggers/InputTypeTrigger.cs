namespace AdaptiveTriggerLibrary.Triggers.UserInteractionTriggers
{
    using Windows.Devices.Input;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Input;
    using ConditionModifiers;
    using ConditionModifiers.GenericModifiers;
    using Functional;

    /// <summary>
    /// This trigger activates, if the current input type
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class InputTypeTrigger : AdaptiveTriggerBase<PointerDeviceType, IConditionModifier>
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private FrameworkElement _targetElement;
        private PointerDeviceType _lastPointerType;

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="FrameworkElement"/> to validate the input type of.
        /// </summary>
        public FrameworkElement TargetElement
        {
            get { return _targetElement; }
            set
            {
                if (value == _targetElement) return;

                if (_targetElement != null)
                    WeakEvent.Unsubscribe<PointerEventHandler>(_targetElement, nameof(FrameworkElement.PointerPressed),
                        TargetElement_PointerPressed);

                _targetElement = value;
                
                if (_targetElement != null)
                    WeakEvent.Subscribe<PointerEventHandler>(_targetElement, nameof(FrameworkElement.PointerPressed),
                        TargetElement_PointerPressed);

                CurrentValue = GetCurrentValue();
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InputTypeTrigger"/> class.
        /// Default modifier: <see cref="EqualsModifier{T}"/>.
        /// </summary>
        public InputTypeTrigger()
            : base(new EqualsModifier<PointerDeviceType>())
        {
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private PointerDeviceType GetCurrentValue()
        {
            return _lastPointerType;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler

        private void TargetElement_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            _lastPointerType = e.Pointer.PointerDeviceType;
            CurrentValue = GetCurrentValue();
        }

        #endregion
    }
}