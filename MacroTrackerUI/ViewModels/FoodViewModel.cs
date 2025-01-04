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

    private DaoSender Sender { get; } =
        ProviderUI.GetServiceProvider().GetService<DaoSender>();

    public FoodViewModel()
    {
        Foods = new ObservableCollection<Food>(Sender.GetFoods());
    }

    public void AddFood(Food food)
    {
        Sender.AddFood(food);

        Foods.Add(food);
    }

    public void RemoveFood(int foodId)
    {
        Sender.RemoveFood(foodId);

        var foodToRemove = Foods.FirstOrDefault(food => food.FoodId == foodId);

        if (foodToRemove != null)
        {
            // Xóa món ăn nếu tìm thấy
            Foods.Remove(foodToRemove);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
