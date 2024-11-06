using MacroTracker.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTracker.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignUp : Page
    {
        private Frame RootFrame { get; set; }

        private Frame LoginShellFrame { get; set; }

        private SignUpViewModel ViewModel { get; } = new SignUpViewModel();

        public SignUp()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Set configuration for the navigation to
        /// </summary>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Get the root frame and the login shell frame
            var pairFrame = e.Parameter as (Frame, Frame)?;
            if (pairFrame.HasValue)
            {
                RootFrame = pairFrame.Value.Item1 as Frame;
                LoginShellFrame = pairFrame.Value.Item2 as Frame;
            }
            else throw new Exception("Cannot cast to Frame");
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
                LoginShellFrame.GoBack();
            }
        }

        /// <summary>
        /// Return to the login page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void LoginLink_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            LoginShellFrame.GoBack();
        }
    }
}
