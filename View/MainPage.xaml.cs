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
public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        contentFrame.Navigate(typeof(FoodPage));
    }

    private void Nv_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItemContainer != null)
        {
            var selectedItem = args.SelectedItemContainer.Tag.ToString();
            switch (selectedItem)
            {
                case "FoodPage":
                    contentFrame.Navigate(typeof(FoodPage));
                    break;
                case "ExercisePage":
                    contentFrame.Navigate(typeof(ExercisePage));
                    break;
                case "ReportPage":
                    contentFrame.Navigate(typeof(ReportPage));
                    break;
                case "GoalsPage":
                    contentFrame.Navigate(typeof(GoalsPage));
                    break;
                case "CalculatorPage":
                    contentFrame.Navigate(typeof(CalculatorPage));
                    break;
            }
        }
    }
}
