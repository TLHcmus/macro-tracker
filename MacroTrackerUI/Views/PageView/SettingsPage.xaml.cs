using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace MacroTrackerUI.Views.PageView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        private Frame RootFrame { get; set; }

        public SettingsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Log out click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void LogOut_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            while (RootFrame.CanGoBack)
            {
                RootFrame.GoBack();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            RootFrame = e.Parameter as Frame;
        }

        private void LogIn_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            RootFrame.Navigate(typeof(LoginPageShell), RootFrame);
        }
    }
}
