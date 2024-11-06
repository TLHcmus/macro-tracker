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
using MacroTracker.ViewModel;
using MacroTracker.Helpers;
using Microsoft.UI.Xaml.Media.Animation;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTracker.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Login : Page
    {
        private LoginViewModel ViewModel { get; } = new LoginViewModel();

        private Frame RootFrame { get; set; }

        private Frame LoginShellFrame {  get; set; }

        public Login()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Login button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Username = ViewModel.Username;
            ViewModel.DatabaseEncryptedPassword = 
                PasswordEncryptionHelper.EncryptPasswordToDatabase(ViewModel.Password); // Encrypt the password

            // Check if the user matches the password
            if (ViewModel.DoesUserMatchPassword())
            {
                if (RememberMeBox.IsChecked == true)
                    StoreLoginInfoInLocalStorage();

                // Navigate to the main page
                RootFrame.Navigate(typeof(MainPage), RootFrame);
            }
            else // If not, show a dialog
            {
                var contentDialog = new ContentDialog
                {
                    XamlRoot = this.XamlRoot,
                    Content = "Wrong username or password!",
                    CloseButtonText = "OK",
                };

                await contentDialog.ShowAsync();
            }
        }

        /// <summary>
        /// Store the login info in the local storage
        /// </summary>
        private void StoreLoginInfoInLocalStorage()
        {
            // Save the username and password to the local settings
            Windows.Storage.ApplicationDataContainer localSettings =
                Windows.Storage.ApplicationData.Current.LocalSettings;

            localSettings.Values["Username"] = ViewModel.Username;

            (string localStorageEncryptedPassword, string entropy) password =
                PasswordEncryptionHelper.EncryptPasswordToLocalStorage(ViewModel.Password);

            localSettings.Values["Password"] = password.localStorageEncryptedPassword;
            localSettings.Values["Entropy"] = password.entropy;
        }

        /// <summary>
        /// Set up configurations when the page is navigated to
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

            // Check if the user is already logged in and clicked "Remember me" button
            DidUserClickRemember();
        }

        /// <summary>
        /// Set up configurations when the page is navigated from
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // Reset box contents
            ViewModel.Username = "";
            ViewModel.Password = "";
        }

        /// <summary>
        /// Check if the user is already clicked "Remember me" button
        /// If yes, set the username and password to the view model
        /// </summary>
        /// <returns></returns>
        private void DidUserClickRemember()
        {
            Windows.Storage.ApplicationDataContainer localSettings =
                Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Values.ContainsKey("Username") &&
                localSettings.Values.ContainsKey("Password") &&
                localSettings.Values.ContainsKey("Entropy"))
            {
                ViewModel.Username = localSettings.Values["Username"].ToString();
                ViewModel.Password = PasswordEncryptionHelper.DecryptPasswordFromLocalStorage(
                    localSettings.Values["Password"].ToString(),
                    localSettings.Values["Entropy"].ToString());
                RememberMeBox.IsChecked = true;
            }
            else CleanUpLocalStorage(localSettings);
      
        }

        private static void CleanUpLocalStorage(ApplicationDataContainer localSettings)
        {
            localSettings.Values.Remove("Username");
            localSettings.Values.Remove("Password");
            localSettings.Values.Remove("Entropy");
        }



        /// <summary>
        /// Sign up link click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void SignUpLink_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            LoginShellFrame.Navigate(typeof(SignUp), (RootFrame, LoginShellFrame), new SlideNavigationTransitionInfo()
            {
                Effect = SlideNavigationTransitionEffect.FromRight
            });
        }

        /// <summary>
        /// Handle the action: checked to unchecked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RememberMeBox_Uncheck(object sender, RoutedEventArgs e)
        {
            Windows.Storage.ApplicationDataContainer localSettings =
                Windows.Storage.ApplicationData.Current.LocalSettings;

            CleanUpLocalStorage(localSettings);
        }
    }
}
