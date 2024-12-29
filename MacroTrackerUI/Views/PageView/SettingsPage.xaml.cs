using Microsoft.UI.Xaml.Controls;

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class SettingsPage : Page
{
    /// <summary>
    /// Delegate for handling log out click events.
    /// </summary>
    public delegate void LogOutClickHandler();

    /// <summary>
    /// Event triggered when the log out hyperlink is clicked.
    /// </summary>
    public event LogOutClickHandler LogOutClickEvent;

    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsPage"/> class.
    /// </summary>
    public SettingsPage()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Handles the log out hyperlink click event.
    /// </summary>
    /// <param name="sender">The hyperlink that was clicked.</param>
    /// <param name="args">Event data for the click event.</param>
    private void LogOut_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
    {
        LogOutClickEvent?.Invoke();
    }

    /// <summary>
    /// Handles the log in hyperlink click event.
    /// </summary>
    /// <param name="sender">The hyperlink that was clicked.</param>
    /// <param name="args">Event data for the click event.</param>
    private void LogIn_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
    {
        Frame.Navigate(typeof(LoginPageShell));
    }
}
