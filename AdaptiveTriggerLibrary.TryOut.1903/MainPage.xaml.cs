using Windows.UI.Xaml;

namespace AdaptiveTriggerLibrary.TryOut._1903
{
    using Windows.UI.ViewManagement;

    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Set target elements
            HorizontalRectangleFillTrigger.TargetElement = HorizontalRectangle;
            VerticalRectangleFillTrigger.TargetElement = VerticalRectangle;
        }

        private void FullscreenButton_Click(object sender, RoutedEventArgs e)
        {
            var currentView = ApplicationView.GetForCurrentView();
            if (currentView.IsFullScreenMode)
                currentView.ExitFullScreenMode();
            else
                currentView.TryEnterFullScreenMode();
        }
    }
}
