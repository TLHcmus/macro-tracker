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
        var foodId = Sender.AddFood(food);

        // Cap nhat food id cua mon vua them
        food.FoodId = foodId;
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

    // Get log by date
    public Log GetLogByDate(DateOnly date)
    {
        return Sender.GetLogByDate(date);
    }

    // Update log
    public void UpdateLog(Log log)
    {
        Sender.UpdateLog(log);
    }

    public event PropertyChangedEventHandler PropertyChanged;
}
