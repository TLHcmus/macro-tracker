using MacroTrackerUI.Models;
using MacroTrackerUI.Views.WindowView;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MacroTrackerUI;

/// <summary>
/// Provides application-specific behavior to supplement the default Application class.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Gets the chat bot conversation.
    /// </summary>
    public static ObservableCollection<Message> ChatBotConversation { get; set; } = [];

    /// <summary>
    /// Initializes the singleton application object. This is the first line of authored code
    /// executed, and as such is the logical equivalent of main() or WinMain().
    /// </summary>
    public App()
    {
        this.InitializeComponent();
    }

    /// <summary>
    /// Invoked when the application is launched.
    /// </summary>
    /// <param name="args">Details about the launch request and process.</param>
    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        m_window = new ShellWindow();
        MaximizeWindow(m_window);
        m_window.Activate();
    }

    /// <summary>
    /// Maximizes the specified window.
    /// </summary>
    /// <param name="m_window">The window to maximize.</param>
    private void MaximizeWindow(Window m_window)
    {
        var windowHandle = WinRT.Interop.WindowNative.GetWindowHandle(m_window);
        var windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
        AppWindow appWindow = AppWindow.GetFromWindowId(windowId);
        OverlappedPresenter presenter = (OverlappedPresenter)appWindow.Presenter;
        presenter.Maximize();
    }

    /// <summary>
    /// The main window of the application.
    /// </summary>
    public static Window m_window;
}
