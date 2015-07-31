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
    public class InteractionModeTrigger : AdaptiveTriggerBase<UserInteractionMode, IGenericModifier<UserInteractionMode>>
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
            CurrentValue = GetCurrentValue();
            Window.Current.SizeChanged += CurrentWindow_SizeChanged;
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

        private void CurrentWindow_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            CurrentValue = GetCurrentValue();
        }

        #endregion
    }
}