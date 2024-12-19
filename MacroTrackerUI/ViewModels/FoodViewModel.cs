using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MacroTrackerUI.ViewModels;

public class FoodViewModel : INotifyPropertyChanged
{
    private ObservableCollection<Food> _foods;
    public ObservableCollection<Food> Foods
    {
        get => _foods;
        set
        {
            _foods = value;
            Items = new ObservableCollection<Item>(Dao.GetFoods());
        }
    }
    public ObservableCollection<Item> Items { get; set; }

    private DaoSender Dao { get; } =
        ProviderUI.GetServiceProvider().GetService<DaoSender>();

    public FoodViewModel()
    {
        Foods = new ObservableCollection<Food>(Dao.GetFoods());
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
