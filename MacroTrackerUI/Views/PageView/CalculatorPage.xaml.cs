using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class CalculatorPage : Page
{
    // View model
    public CalculatorViewModel ViewModel
    {
        get; set;
    }
    public CalculatorPage()
    {
        this.InitializeComponent();
        ViewModel = new CalculatorViewModel();
    }

    private void calculateButton_Click(object sender, RoutedEventArgs e)
    {
        int tdee = (int)ViewModel.CalculateTDEE();
        ResultTextBlock.Text = $"Your Maintenance Calories is {tdee} calories per day";
    }
}
