using MacroTrackerUI.Views.PageView;
using Microsoft.UI.Xaml;

namespace MacroTrackerUI.Views.WindowView;

/// <summary>
/// Represents the main window of the application.
/// </summary>
public sealed partial class ShellWindow : Window
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ShellWindow"/> class.
    /// </summary>
    public ShellWindow()
    {
        this.InitializeComponent();
        MainFrame.Navigate(typeof(LoginPageShell));
    }
}
