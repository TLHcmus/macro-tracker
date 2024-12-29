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
    private ReportViewModel ViewModel { get; set; }
    public ReportPage()
    {
        this.InitializeComponent();
        ViewModel = new ReportViewModel();
        this.DataContext = ViewModel;
    }

}
