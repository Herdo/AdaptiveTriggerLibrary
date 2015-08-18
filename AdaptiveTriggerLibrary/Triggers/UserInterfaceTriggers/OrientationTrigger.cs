namespace AdaptiveTriggerLibrary.Triggers.UserInterfaceTriggers
{
    using Windows.UI.Core;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using ConditionModifiers.GenericModifiers;

    /// <summary>
    /// This trigger activates, if current window orientation
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class OrientationTrigger : AdaptiveTriggerBase<ApplicationViewOrientation, IGenericModifier>,
                                      IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="OrientationTrigger"/> class.
        /// Default modifier: <see cref="EqualsModifier{T}"/>.
        /// </summary>
        public OrientationTrigger()
            : base(new EqualsModifier<ApplicationViewOrientation>())
        {
            Window.Current.SizeChanged += MainWindow_SizeChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private ApplicationViewOrientation GetCurrentValue()
        {
            var window = Window.Current;
            var currentOrientation = window.Bounds.Width >= window.Bounds.Height
                ? ApplicationViewOrientation.Landscape
                : ApplicationViewOrientation.Portrait;
            return currentOrientation;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler

        private void MainWindow_SizeChanged(object sender, WindowSizeChangedEventArgs windowSizeChangedEventArgs)
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
            Window.Current.SizeChanged -= MainWindow_SizeChanged;
        }

        void IDynamicTrigger.ResumeUpdates()
        {
            Window.Current.SizeChanged += MainWindow_SizeChanged;
        }

        #endregion
    }
}