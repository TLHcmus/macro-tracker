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

    /// <summary>
    /// Event handler for when the navigation view selection changes
    /// </summary>
    /// <param name="sender"></param> 
    /// <param name="args"></param>
    private void Nv_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItemContainer != null)
        {
            // Check if the settings item is selected
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(SettingsPage));
                return;
            }

            // Get the other selected items
            var selectedItem = args.SelectedItemContainer.Tag.ToString(); // Get the tag of the selected item
            try
            {
                Type pageType = Type.GetType(this.GetType().Namespace + "." + selectedItem); // MacroTracker.View + selectedItem
                contentFrame.Navigate(pageType);
            }
            catch (Exception)
            {
                contentFrame.Navigate(typeof(FoodPage)); // Default page if there is an error
            }
        }
    }
}
