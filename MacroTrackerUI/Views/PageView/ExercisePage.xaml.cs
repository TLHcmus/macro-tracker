using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class ExercisePage : Page
{
    /// <summary>
    /// Gets or sets the ViewModel for managing exercises.
    /// </summary>
    private ExerciseViewModel ViewModel { get; set; } = new ExerciseViewModel();

    /// <summary>
    /// Initializes a new instance of the <see cref="ExercisePage"/> class.
    /// </summary>
    public ExercisePage()
    {
        this.InitializeComponent();
        ChatBot.ChatBotConversation = App.ChatBotConversation;
    }
}
