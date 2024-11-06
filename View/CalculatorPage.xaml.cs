using MacroTracker.ViewModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace MacroTracker.View;

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
        int tdee = (int)ViewModel.healthInfo.CalculateTDEE();
        ResultTextBlock.Text = $"Your Maintenance Calories is {tdee} calories per day";
    }
}
