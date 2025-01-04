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
        NavigateToPage(typeof(LogPage));
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
            if (args.IsSettingsSelected)
            {
                NavigateToPage(typeof(SettingsPage));
                if (ContentFrame.Content is SettingsPage settingsPage)
                {
                    settingsPage.LogOutClickEvent += Setting_LogOutClickEvent;
                }
                return;
            }

            var selectedItem = args.SelectedItemContainer.Tag.ToString();
            NavigateToPageByName(selectedItem);
        }
    }

    /// <summary>
    /// Navigates to a page by its type.
    /// </summary>
    /// <param name="pageType">The type of the page to navigate to.</param>
    private void NavigateToPage(Type pageType)
    {
        ContentFrame.Navigate(pageType);
    }

    /// <summary>
    /// Navigates to a page by its name.
    /// </summary>
    /// <param name="pageName">The name of the page to navigate to.</param>
    private void NavigateToPageByName(string pageName)
    {
        try
        {
            Type pageType = Type.GetType($"{this.GetType().Namespace}.{pageName}");
            NavigateToPage(pageType);
        }
        catch (Exception)
        {
            NavigateToPage(typeof(FoodPage));
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
