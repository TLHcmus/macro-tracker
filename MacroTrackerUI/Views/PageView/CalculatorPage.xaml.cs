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
    /// <summary>
    /// Gets or sets the view model for the CalculatorPage.
    /// </summary>
    public CalculatorViewModel ViewModel
    {
        get; set;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculatorPage"/> class.
    /// </summary>
    public CalculatorPage()
    {
        this.InitializeComponent();
        ViewModel = new CalculatorViewModel();
    }

    /// <summary>
    /// Handles the Click event of the CalculateButton control.
    /// Calculates the Total Daily Energy Expenditure (TDEE) and displays the result.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void CalculateButton_Click(object sender, RoutedEventArgs e)
    {
        int tdee = (int)ViewModel.HealthInfo.CalculateTDEE();
        ResultTextBlock.Text = $"Your Maintenance Calories is {tdee} calories per day";
    }
}
