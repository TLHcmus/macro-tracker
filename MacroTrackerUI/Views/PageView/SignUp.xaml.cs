using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;

namespace MacroTrackerUI.Views.PageView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUp : Page
    {
        private SignUpViewModel ViewModel { get; } = new SignUpViewModel();

        public SignUp()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Sign up button click event handler
        /// Check if the user has already existed and the password and reentered password match
        /// If not, show a dialog
        /// If yes, add to users database and navigate to the login page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            bool? statusSignUp = ViewModel.IsSignUpValid(out string promptMessage);

            if (statusSignUp.HasValue)
            {
                // Show the status dialog
                var contentDialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Content = promptMessage,
                    CloseButtonText = "OK"
                };
                await contentDialog.ShowAsync();
            }

            if (statusSignUp == true)
            {
                Frame.GoBack();
            }
        }

        /// <summary>
        /// Return to the login page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void LoginLink_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            Frame.GoBack();
        }
    }
}
