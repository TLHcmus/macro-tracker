using MacroTrackerUI.Models;
using MacroTrackerUI.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MacroTrackerUI.Views.UserControlView;

public sealed partial class OptionSelection : UserControl, INotifyPropertyChanged
{
    private OptionSelectionViewModel ViewModel { get; set; } = 
        new OptionSelectionViewModel();

    public delegate void ItemClickEvent(Item item, Type itemType);
    public event ItemClickEvent ItemClickEventHandler;

    public delegate void BackEvent();
    public event BackEvent BackEventHandler;

    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title",
            typeof(string),
            typeof(OptionSelection),
            new PropertyMetadata(null)
        );

    public static readonly DependencyProperty ItemListProperty = DependencyProperty.Register(
        "ItemList",
        typeof(ObservableCollection<Item>),
        typeof(OptionSelection),
        new PropertyMetadata(null)
    );

    public event PropertyChangedEventHandler PropertyChanged;

    public string Title
    {
        get { return (string)GetValue(TitleProperty); }
        set { SetValue(TitleProperty, value); }
    }

    public ObservableCollection<Item> ItemList
    {
        get { return (ObservableCollection<Item>)GetValue(ItemListProperty); }
        set { SetValue(ItemListProperty, value); }
    }

    public OptionSelection()
    {
        this.InitializeComponent();
    }

    private void ListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        ItemClickEventHandler?.Invoke(e.ClickedItem as Item, e.ClickedItem.GetType());
    }

    private void BackButton_Click(object sender, RoutedEventArgs e)
    {
        BackEventHandler?.Invoke();
    }
}
