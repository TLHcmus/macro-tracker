using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace MacroTrackerUI.Views.PageView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPageShell : Page
    {
        private Frame RootFrame { get; set; }

        public LoginPageShell()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            RootFrame = e.Parameter as Frame;
            LoginMode.Navigate(typeof(Login), (RootFrame, LoginMode));
        }
    }
}
