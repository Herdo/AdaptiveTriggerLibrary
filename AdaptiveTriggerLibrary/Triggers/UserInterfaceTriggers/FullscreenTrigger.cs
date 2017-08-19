namespace AdaptiveTriggerLibrary.Triggers.UserInterfaceTriggers
{
    using Windows.UI.Core;
    using Windows.UI.ViewManagement;
    using Windows.UI.Xaml;
    using ConditionModifiers.GenericModifiers;

    /// <summary>
    /// This trigger activates, if the current application view Fullscreen status
    /// matches the specified <see cref="AdaptiveTriggerBase{TCondition,TConditionModifier}.Condition"/>.
    /// </summary>
    public class FullScreenTrigger : AdaptiveTriggerBase<bool, IGenericModifier>,
                                     IDynamicTrigger
    {
        ///////////////////////////////////////////////////////////////////
        #region Fields

        private bool _useFullScreenModeProperty;
        
        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Properties

        /// <summary>
        /// Gets or sets if the <see cref="ApplicationView.IsFullScreenMode"/> property should be used for the check,
        /// rather than the <see cref="ApplicationView.IsFullScreen"/> property (default).
        /// </summary>
        /// <remarks>Set this to true, if you're using the <see cref="ApplicationView.TryEnterFullScreenMode"/>
        /// or <see cref="ApplicationView.ExitFullScreenMode"/> method.</remarks>
        public bool UseFullScreenModeProperty
        {
            get => _useFullScreenModeProperty;
            set
            {
                if(value == _useFullScreenModeProperty) return;
                _useFullScreenModeProperty = value;
                CurrentValue = GetCurrentValue();
            }
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FullScreenTrigger"/> class.
        /// Default condition: true.
        /// Default modifier: <see cref="EqualsModifier{Boolean}"/>.
        /// </summary>
        public FullScreenTrigger()
            : base(true, new EqualsModifier<bool>())
        {
            _useFullScreenModeProperty = false;
            
            Window.Current.SizeChanged += MainWindow_SizeChanged;

            // Set initial value
            CurrentValue = GetCurrentValue();
        }

        #endregion

        ///////////////////////////////////////////////////////////////////
        #region Private Methods

        private bool GetCurrentValue()
        {
            var currentView = ApplicationView.GetForCurrentView();
            return UseFullScreenModeProperty
                ? currentView?.IsFullScreenMode ?? false
                : currentView?.IsFullScreen ?? false;
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