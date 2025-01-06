using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for managing food items.
/// </summary>
public class FoodViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the collection of food items.
    /// </summary>
    public ObservableCollection<Food> Foods { get; set; }

    private IServiceProvider Provider { get; }
    private IDaoSender Sender { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="FoodViewModel"/> class.
    /// </summary>
    public FoodViewModel()
    {
        Provider = ProviderUI.GetServiceProvider();
        Sender = Provider.GetService<IDaoSender>();
        Foods = new ObservableCollection<Food>(Sender.GetFoods());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FoodViewModel"/> class with a specified service provider.
    /// </summary>
    /// <param name="provider">The service provider.</param>
    public FoodViewModel(IServiceProvider provider)
    {
        Provider = provider;
        Sender = Provider.GetService<IDaoSender>();
        Foods = new ObservableCollection<Food>(Sender.GetFoods());
    }

    /// <summary>
    /// Adds a new food item to the collection.
    /// </summary>
    /// <param name="food">The food item to add.</param>
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
