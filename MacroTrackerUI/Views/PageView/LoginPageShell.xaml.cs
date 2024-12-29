using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class LoginPageShell : Page
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginPageShell"/> class.
    /// </summary>
    public LoginPageShell()
    {
        this.InitializeComponent();
        InitializeLoginPage();
    }

    /// <summary>
    /// Initializes the login page by navigating to the Login page and setting up event handlers.
    /// </summary>
    private void InitializeLoginPage()
    {
        LoginMode.Navigate(typeof(Login));
        if (LoginMode.Content is Login currentContent)
        {
            currentContent.SignUpLinkClickEvent += SignUpLink_Click;
            currentContent.LogInClickEvent += Login_Click;
        }
    }

    /// <summary>
    /// Handles the SignUpLink click event to navigate to the SignUp page.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void SignUpLink_Click(object sender, RoutedEventArgs e)
    {
        LoginMode.Navigate(typeof(SignUp), null, new SlideNavigationTransitionInfo
        {
            Effect = SlideNavigationTransitionEffect.FromRight
        });

        if (LoginMode.Content is SignUp currentContent)
        {
            currentContent.LoginLinkClickEvent += LoginLink_Click;
            currentContent.SignUpClickEvent += SignUp_Click;
        }
    }

    /// <summary>
    /// Handles the LoginLink click event to navigate back to the Login page.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void LoginLink_Click(object sender, RoutedEventArgs e)
    {
        LoginMode.GoBack();
        if (LoginMode.Content is Login currentContent)
        {
            currentContent.SignUpLinkClickEvent += SignUpLink_Click;
            currentContent.LogInClickEvent += Login_Click;
        }
    }

    /// <summary>
    /// Handles the SignUp click event to navigate back to the Login page.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void SignUp_Click(object sender, RoutedEventArgs e)
    {
        LoginMode.GoBack();
        if (LoginMode.Content is Login currentContent)
        {
            currentContent.SignUpLinkClickEvent += SignUpLink_Click;
            currentContent.LogInClickEvent += Login_Click;
        }
    }

    /// <summary>
    /// Handles the Login click event to navigate to the MainPage.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void Login_Click(object sender, RoutedEventArgs e)
    {
        Frame.Navigate(typeof(MainPage));
    }
}
