using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private async void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        Debug.WriteLine(ViewModel.Username);
        Debug.WriteLine(ViewModel.Password);

        if (ViewModel.LoginInfoNull())
        {
            await ShowContentDialog("Please enter your username or password!");
            return;
        }

        if (ViewModel.DoesUserMatchPassword())
        {
            if (RememberMeBox.IsChecked == true)
                StoreLoginInfoInLocalStorage();

            LogInClickEvent?.Invoke(sender, e);
        }
        else
        {
            await ShowContentDialog("Wrong username or password!");
        }
    }

    /// <summary>
    /// Shows a content dialog with the specified message.
    /// </summary>
    /// <param name="message">The message to display.</param>
    private async Task ShowContentDialog(string message)
    {
        var contentDialog = new ContentDialog
        {
            XamlRoot = this.XamlRoot,
            Content = message,
            CloseButtonText = "OK",
        };
        await contentDialog.ShowAsync();
    }

    /// <summary>
    /// Store the login info in the local storage
    /// </summary>
    private void StoreLoginInfoInLocalStorage()
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        localSettings.Values["Username"] = ViewModel.Username;

        (string encryptedPassword, string entropy) = ViewModel.EncryptionSender.EncryptPasswordToLocalStorage(ViewModel.Password);
        localSettings.Values["Password"] = encryptedPassword;
        localSettings.Values["Entropy"] = entropy;
    }

    /// <summary>
    /// Set up configurations when the page is navigated to
    /// </summary>
    /// <param name="e">The navigation event arguments.</param>
    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        DidUserClickRemember();
    }

    /// <summary>
    /// Set up configurations when the page is navigated from
    /// </summary>
    /// <param name="e">The navigation event arguments.</param>
    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);
        ViewModel.Username = string.Empty;
        ViewModel.Password = string.Empty;
    }

    /// <summary>
    /// Check if the user clicked "Remember me" button
    /// If yes, set the username and password to the view model
    /// </summary>
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
        else
        {
            CleanUpLocalStorage(localSettings);
        }
    }

    /// <summary>
    /// Cleans up the local storage by removing stored login information.
    /// </summary>
    /// <param name="localSettings">The local settings container.</param>
    private static void CleanUpLocalStorage(ApplicationDataContainer localSettings)
    {
        localSettings.Values.Remove("Username");
        localSettings.Values.Remove("Password");
        localSettings.Values.Remove("Entropy");
    }

    /// <summary>
    /// Sign up link click event handler
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="args">The event arguments.</param>
    private void SignUpLink_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
    {
        SignUpLinkClickEvent?.Invoke(sender, args);
    }

    /// <summary>
    /// Handle the action: checked to unchecked
    /// </summary>
    /// <param name="sender">The event sender.</param>
    /// <param name="e">The event arguments.</param>
    private void RememberMeBox_Uncheck(object sender, RoutedEventArgs e)
    {
        ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        CleanUpLocalStorage(localSettings);
    }
}
