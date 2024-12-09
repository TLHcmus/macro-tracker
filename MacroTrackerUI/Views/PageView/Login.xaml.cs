using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using Windows.Storage;

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class Login : Page
{
    public delegate void ClickEvent(object sender, RoutedEventArgs e);
    public event ClickEvent SignUpLinkClickEvent;
    public event ClickEvent LogInClickEvent;
    private LoginViewModel ViewModel { get; } = new LoginViewModel();

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
        if (ViewModel.LoginInfoNull())
        {
            var contentDialog = new ContentDialog
            {
                XamlRoot = this.XamlRoot,
                Content = "Please enter your username or password!",
                CloseButtonText = "OK",
            };
            await contentDialog.ShowAsync();
            return;
        }

        // Check if the user matches the password
        if (ViewModel.DoesUserMatchPassword())
        {
            if (RememberMeBox.IsChecked == true)
                StoreLoginInfoInLocalStorage();

            // Navigate to the main page
            LogInClickEvent?.Invoke(sender, e);
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
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        localSettings.Values["Username"] = ViewModel.Username;

        (string localStorageEncryptedPassword, string entropy) password =
            ViewModel.EncryptionSender.EncryptPasswordToLocalStorage(ViewModel.Password);

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
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;

        if (localSettings.Values.ContainsKey("Username") &&
            localSettings.Values.ContainsKey("Password") &&
            localSettings.Values.ContainsKey("Entropy"))
        {
            ViewModel.Username = localSettings.Values["Username"].ToString();
            ViewModel.Password = ViewModel.EncryptionSender.DecryptPasswordFromLocalStorage(
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
        SignUpLinkClickEvent?.Invoke(sender, args);
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
