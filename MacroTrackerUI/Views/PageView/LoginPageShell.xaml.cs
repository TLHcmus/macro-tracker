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

        LoginMode.Navigate(typeof(Login));
        Login currentContent = LoginMode.Content as Login;
        currentContent.SignUpLinkClickEvent += SignUpLink_Click;
        currentContent.LogInClickEvent += Login_Click;
    }

    /// <summary>
    /// Handles the SignUpLink click event to navigate to the SignUp page.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void SignUpLink_Click(object sender, RoutedEventArgs e)
    {
        LoginMode.Navigate(typeof(SignUp), null, new SlideNavigationTransitionInfo()
        {
            Effect = SlideNavigationTransitionEffect.FromRight
        }
        );

        SignUp currentContent = LoginMode.Content as SignUp;
        currentContent.LoginLinkClickEvent += LoginLink_Click;
        currentContent.SignUpClickEvent += SignUp_Click;
    }

    /// <summary>
    /// Handles the LoginLink click event to navigate back to the Login page.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void LoginLink_Click(object sender, RoutedEventArgs e)
    {
        LoginMode.GoBack();
        Login currentContent = LoginMode.Content as Login;
        currentContent.SignUpLinkClickEvent += SignUpLink_Click;
        currentContent.LogInClickEvent += Login_Click;
    }

    /// <summary>
    /// Handles the SignUp click event to navigate back to the Login page.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void SignUp_Click(object sender, RoutedEventArgs e)
    {
        LoginMode.GoBack();
        Login currentContent = LoginMode.Content as Login;
        currentContent.SignUpLinkClickEvent += SignUpLink_Click;
        currentContent.LogInClickEvent += Login_Click;
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
