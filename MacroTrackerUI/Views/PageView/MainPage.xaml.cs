using Microsoft.UI.Xaml.Controls;
using System;

namespace MacroTrackerUI.Views.PageView;

/// <summary>
/// An empty page that can be used on its own or navigated to within a Frame.
/// </summary>
public sealed partial class MainPage : Page
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MainPage"/> class.
    /// </summary>
    public MainPage()
    {
        this.InitializeComponent();
        ContentFrame.Navigate(typeof(LogPage));
        //ContentFrame.BackStack.Clear();
    }

    /// <summary>
    /// Event handler for when the navigation view selection changes.
    /// </summary>
    /// <param name="sender">The sender object.</param>
    /// <param name="args">The event arguments.</param>
    private void Nv_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItemContainer != null)
        {
            // Check if the settings item is selected   
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
                (ContentFrame.Content as SettingsPage).LogOutClickEvent += Setting_LogOutClickEvent;
                return;
            }

            // Get the other selected items
            var selectedItem = args.SelectedItemContainer.Tag.ToString(); // Get the tag of the selected item
            try
            {
                Type pageType = Type.GetType(this.GetType().Namespace + "." + selectedItem); // MacroTrackerUI.View + selectedItem
                ContentFrame.Navigate(pageType);
            }
            catch (Exception)
            {
                ContentFrame.Navigate(typeof(FoodPage)); // Default page if there is an error
            }
        }
    }

    /// <summary>
    /// Event handler for the logout click event in the settings page.
    /// </summary>
    private void Setting_LogOutClickEvent()
    {
        while (Frame.CanGoBack)
        {
            Frame.GoBack();
        }
    }
}
