using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MacroTrackerUI.ViewModels;

public class ReportViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Log> LogList { get; set; }

    private DaoSender Sender { get; } = ProviderUI.GetServiceProvider().GetRequiredService<DaoSender>();

    public ObservableCollection<ISeries> Series { get; set; }
    public ObservableCollection<Axis> XAxes { get; set; }

    public ObservableCollection<Axis> YAxes { get; set; }

    public ReportViewModel()
    {
        LogList = new ObservableCollection<Log>(Sender.GetLogs());

        var caloriesByDate = LogList
           .GroupBy(log => log.LogDate)
           .Select(group => new { Date = group.Key, TotalCalories = group.Sum(log => log.TotalCalories) })
           .OrderBy(x => x.Date)
           .ToList();

        Series = new ObservableCollection<ISeries>
        {
            new LineSeries<double>
            {
                Values = caloriesByDate.Select(x => x.TotalCalories).ToList()
            }
        };

        XAxes = new ObservableCollection<Axis>
        {
            new Axis
            {
                Labels = caloriesByDate.Select(x => x.Date?.ToString("MM/dd/yyyy")).ToList(),
                Name = "Date"
            }
        };
        YAxes = new ObservableCollection<Axis>
        {
            new Axis
            {
                Name = "Total Calories"
            }
        };


        //// Lấy danh sách tất cả các món ăn từ database
        //var foods = Sender.GetFoods(); // Lấy danh sách foods (có thông tin dinh dưỡng)
        //var foodDict = foods.ToDictionary(food => food.Name, food => food); // Tạo dictionary để tra cứu nhanh

        //// Lấy danh sách các log
        //LogList = new ObservableCollection<Log>(Sender.GetLogs());

        //// Tính tổng dinh dưỡng theo ngày
        //var nutrientsByDate = LogList
        //    .GroupBy(log => log.LogDate)
        //    .Select(group => new
        //    {
        //        Date = group.Key,
        //        TotalCalories = group.Sum(log => log.TotalCalories),
        //        TotalProtein = group.Sum(log => log.LogFoodItems.Sum(item =>
        //            foodDict.TryGetValue(item.FoodName, out var food) ? food.ProteinPer100g * item.NumberOfServings : 0)),
        //        TotalCarbs = group.Sum(log => log.LogFoodItems.Sum(item =>
        //            foodDict.TryGetValue(item.FoodName, out var food) ? food.CarbsPer100g * item.NumberOfServings : 0)),
        //        TotalFat = group.Sum(log => log.LogFoodItems.Sum(item =>
        //            foodDict.TryGetValue(item.FoodName, out var food) ? food.FatPer100g * item.NumberOfServings : 0))
        //    })
        //    .OrderBy(x => x.Date)
        //    .ToList();

        //Series = new ObservableCollection<ISeries>
        //{
        //    new LineSeries<double>
        //    {
        //        Values = nutrientsByDate.Select(x => x.TotalCalories).ToList(),
        //        Name = "Total Calories"
        //    },
        //    new LineSeries<double>
        //    {
        //        Values = nutrientsByDate.Select(x => x.TotalProtein).ToList(),
        //        Name = "Total Protein"
        //    },
        //    new LineSeries<double>
        //    {
        //        Values = nutrientsByDate.Select(x => x.TotalCarbs).ToList(),
        //        Name = "Total Carbs"
        //    },
        //    new LineSeries<double>
        //    {
        //        Values = nutrientsByDate.Select(x => x.TotalFat).ToList(),
        //        Name = "Total Fat"
        //    }
        //};

        //XAxes = new ObservableCollection<Axis>
        //{
        //    new Axis
        //    {
        //        Labels = nutrientsByDate.Select(x => x.Date?.ToString("MM/dd/yyyy")).ToList(),
        //        Name = "Date"
        //    }
        //};

        //YAxes = new ObservableCollection<Axis>
        //{
        //    new Axis
        //    {
        //        Name = "Nutrients"
        //    }
        //};

    }

    public event PropertyChangedEventHandler PropertyChanged;
}
