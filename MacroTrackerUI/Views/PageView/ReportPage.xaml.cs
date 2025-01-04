using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ReportPage : Page
{
    /// <summary>
    /// Gets the ViewModel for the ReportPage.
    /// </summary>
    private ReportViewModel ViewModel { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportPage"/> class.
    /// </summary>
    public ReportPage()
    {
        InitializeComponent();
        ViewModel = new ReportViewModel();
        DataContext = ViewModel;
        ChatBot.ChatBotConversation = App.ChatBotConversation;
    }
}
