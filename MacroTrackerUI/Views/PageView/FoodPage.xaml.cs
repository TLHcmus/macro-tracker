using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml.Controls;
using System;

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class FoodPage : Page
{
    private FoodViewModel ViewModel { get; set; } = new FoodViewModel();
    public FoodPage()
    {
        this.InitializeComponent();
    }

    public void Item_Click(Item item, Type type)
    {
        DetailItem.Visibility = Microsoft.UI.Xaml.Visibility.Visible;
    }

    private void BackButton_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
    {

    }
}
