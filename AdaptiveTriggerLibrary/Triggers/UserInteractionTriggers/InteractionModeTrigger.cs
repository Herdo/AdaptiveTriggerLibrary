namespace AdaptiveTriggerLibrary.Triggers.UserInteractionTriggers
{
    using Windows.UI.Core;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using ConditionModifiers.GenericModifiers;

    /// <summary>
    /// This trigger activates, if the user interaction mode
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class InteractionModeTrigger : AdaptiveTriggerBase<UserInteractionMode, IGenericModifier>,
                                          IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InteractionModeTrigger"/> class.
        /// Default modifier: <see cref="EqualsModifier{T}"/>.
        /// </summary>
        public InteractionModeTrigger()
            : base(new EqualsModifier<UserInteractionMode>())
        {
            Window.Current.SizeChanged += MainWindow_SizeChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private static UserInteractionMode GetCurrentValue()
        {
            return UIViewSettings.GetForCurrentView().UserInteractionMode;
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Event Handler

        private void MainWindow_SizeChanged(object sender, WindowSizeChangedEventArgs e)
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