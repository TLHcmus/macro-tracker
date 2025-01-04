using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

public class FoodViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Food> Foods { get; set; }

    private IServiceProvider Provider { get; set; }

    private IDaoSender Sender { get; }

    public FoodViewModel()
    {
        Provider = ProviderUI.GetServiceProvider();
        Sender = Provider.GetService<IDaoSender>();
        Foods = new ObservableCollection<Food>(Sender.GetFoods());
    }

    public FoodViewModel(IServiceProvider provider)
    {
        Provider = provider;
        Sender = Provider.GetService<IDaoSender>();
        Foods = new ObservableCollection<Food>(Sender.GetFoods());
    }

    public void AddFood(Food food)
    {
        Sender.AddFood(food);

        Foods.Add(food);
    }

    public void RemoveFood(string foodName)
    {
        Sender.RemoveFood(foodName);

        var foodToRemove = Foods.FirstOrDefault(food => food.Name.Equals(foodName, StringComparison.OrdinalIgnoreCase));

        if (foodToRemove != null)
        {
            // Xóa món ăn nếu tìm thấy
            Foods.Remove(foodToRemove);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
