using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media; 
using System;

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
            SignUpClickEvent?.Invoke(sender, e);
        }
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
        if (string.IsNullOrEmpty(ViewModel.Username))
        {
            UsernamePrompt.Text = "Username cannot be empty!";
            UsernamePrompt.Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightPink);
        }
        else
        {
            if (ViewModel.DoesUsernameExist())
            {
                UsernamePrompt.Text = "Username has already existed!";
                UsernamePrompt.Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightPink);
            }
            else
            {
                UsernamePrompt.Text = "Accepted";
                UsernamePrompt.Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightGreen);
            }
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
        if (string.IsNullOrEmpty(ViewModel.Password))
        {
            PasswordPrompt.Text = "Password cannot be empty!";
            PasswordPrompt.Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightPink);
        }
        else
        {
            if (!ViewModel.IsPasswordStrong())
            {
                PasswordPrompt.Text = "Password is not strong!";
                PasswordPrompt.Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightPink);
            }
            else
            {
                PasswordPrompt.Text = "Accepted";
                PasswordPrompt.Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightGreen);
            }
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
        if (string.IsNullOrEmpty(ViewModel.ReenteredPassword))
        {
            ReenteredPasswordPrompt.Text = "Reentered password cannot be empty!";
            ReenteredPasswordPrompt.Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightPink);
        }
        else
        {
            if (!ViewModel.DoPasswordsMatch())
            {
                ReenteredPasswordPrompt.Text = "Reentered password does not match!";
                ReenteredPasswordPrompt.Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightPink);
            }
            else
            {
                ReenteredPasswordPrompt.Text = "Accepted";
                ReenteredPasswordPrompt.Foreground = new SolidColorBrush(Microsoft.UI.Colors.LightGreen);
            }
        }
    }
}
