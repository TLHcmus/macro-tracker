using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media; 
using System;
using System.Threading.Tasks;

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SignUp : Page
{
    /// <summary>
    /// Delegate for handling click events.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    public delegate void ClickEvent(object sender, RoutedEventArgs e);

    /// <summary>
    /// Event triggered when the login link is clicked.
    /// </summary>
    public event ClickEvent LoginLinkClickEvent;

    /// <summary>
    /// Event triggered when the sign-up button is clicked.
    /// </summary>
    public event ClickEvent SignUpClickEvent;

    /// <summary>
    /// Gets the ViewModel for the SignUp page.
    /// </summary>
    private SignUpViewModel ViewModel { get; } = new SignUpViewModel();

    /// <summary>
    /// Initializes a new instance of the <see cref="SignUp"/> class.
    /// </summary>
    public SignUp()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Sign up button click event handler.
    /// Checks if the user already exists and if the password and reentered password match.
    /// If not, shows a dialog.
    /// If yes, adds the user to the database and navigates to the login page.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private async void SignUpButton_Click(object sender, RoutedEventArgs e)
    {
        bool? statusSignUp = ViewModel.IsSignUpValid(out string promptMessage);

        if (statusSignUp.HasValue)
        {
            await ShowContentDialog(promptMessage);
        }

        if (statusSignUp == true)
        {
            SignUpClickEvent?.Invoke(sender, e);
        }
    }

    /// <summary>
    /// Shows a content dialog with the specified message.
    /// </summary>
    /// <param name="message">The message to display in the dialog.</param>
    private async Task ShowContentDialog(string message)
    {
        var contentDialog = new ContentDialog
        {
            XamlRoot = this.XamlRoot,
            Content = message,
            CloseButtonText = "OK"
        };
        await contentDialog.ShowAsync();
    }

    /// <summary>
    /// Handles the click event of the login link.
    /// Navigates back to the login page.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The event data.</param>
    private void LoginLink_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
    {
        LoginLinkClickEvent?.Invoke(sender, args);
    }

    /// <summary>
    /// Handles the text changed event for the username field.
    /// Validates the username and updates the prompt message accordingly.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void Username_TextChanged(object sender, TextChangedEventArgs e)
    {
        UpdateUsernamePrompt();
    }

    /// <summary>
    /// Updates the username prompt based on the validation result.
    /// </summary>
    private void UpdateUsernamePrompt()
    {
        if (string.IsNullOrEmpty(ViewModel.Username))
        {
            SetPromptMessage(UsernamePrompt, "Username cannot be empty!", Microsoft.UI.Colors.LightPink);
        }
        else if (ViewModel.DoesUsernameExist())
        {
            SetPromptMessage(UsernamePrompt, "Username has already existed!", Microsoft.UI.Colors.LightPink);
        }
        else
        {
            SetPromptMessage(UsernamePrompt, "Accepted", Microsoft.UI.Colors.LightGreen);
        }
    }

    /// <summary>
    /// Handles the text changed event for the password field.
    /// Validates the password strength and updates the prompt message accordingly.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void Password_TextChanged(object sender, RoutedEventArgs e)
    {
        UpdatePasswordPrompt();
    }

    /// <summary>
    /// Updates the password prompt based on the validation result.
    /// </summary>
    private void UpdatePasswordPrompt()
    {
        if (string.IsNullOrEmpty(ViewModel.Password))
        {
            SetPromptMessage(PasswordPrompt, "Password cannot be empty!", Microsoft.UI.Colors.LightPink);
        }
        else if (!ViewModel.IsPasswordStrong())
        {
            SetPromptMessage(PasswordPrompt, "Password is not strong!", Microsoft.UI.Colors.LightPink);
        }
        else
        {
            SetPromptMessage(PasswordPrompt, "Accepted", Microsoft.UI.Colors.LightGreen);
        }
    }

    /// <summary>
    /// Handles the password changed event for the reentered password field.
    /// Validates if the reentered password matches the original password and updates the prompt message accordingly.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void ReenteredPassword_PasswordChanged(object sender, RoutedEventArgs e)
    {
        UpdateReenteredPasswordPrompt();
    }

    /// <summary>
    /// Updates the reentered password prompt based on the validation result.
    /// </summary>
    private void UpdateReenteredPasswordPrompt()
    {
        if (string.IsNullOrEmpty(ViewModel.ReenteredPassword))
        {
            SetPromptMessage(ReenteredPasswordPrompt, "Reentered password cannot be empty!", Microsoft.UI.Colors.LightPink);
        }
        else if (!ViewModel.DoPasswordsMatch())
        {
            SetPromptMessage(ReenteredPasswordPrompt, "Reentered password does not match!", Microsoft.UI.Colors.LightPink);
        }
        else
        {
            SetPromptMessage(ReenteredPasswordPrompt, "Accepted", Microsoft.UI.Colors.LightGreen);
        }
    }

    /// <summary>
    /// Sets the prompt message and its foreground color.
    /// </summary>
    /// <param name="textBlock">The TextBlock to update.</param>
    /// <param name="message">The message to display.</param>
    /// <param name="color">The color of the message.</param>
    private void SetPromptMessage(TextBlock textBlock, string message, Windows.UI.Color color)
    {
        textBlock.Text = message;
        textBlock.Foreground = new SolidColorBrush(color);
    }
}
