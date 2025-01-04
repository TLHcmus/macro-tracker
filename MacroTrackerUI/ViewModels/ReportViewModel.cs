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
using System.Diagnostics;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for generating reports based on logs.
/// </summary>
public class ReportViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the collection of logs.
    /// </summary>
    public ObservableCollection<Log> LogList { get; set; }

    /// <summary>
    /// Gets the data access sender.
    /// </summary>
    private IDaoSender Sender { get; }

    private IServiceProvider Provider { get; }

    public ReportViewModel()
    {
        Provider = ProviderUI.GetServiceProvider();
        Sender = Provider.GetService<IDaoSender>();
        SetUpDiagrams();
    }

    public ReportViewModel(IServiceProvider provider)
    {
        Provider = provider;
        Sender = Provider.GetService<IDaoSender>();
        SetUpDiagrams();
    }

    public ObservableCollection<ISeries> CaloriesSeries { get; set; }
    public ObservableCollection<ISeries> ProteinSeries { get; set; }
    public ObservableCollection<ISeries> CarbsSeries { get; set; }
    public ObservableCollection<ISeries> FatSeries { get; set; }
    public ObservableCollection<Axis> XAxes { get; set; }

    public ObservableCollection<Axis> CaloriesYAxes { get; set; }
    public ObservableCollection<Axis> ProteinYAxes { get; set; }
    public ObservableCollection<Axis> CarbsYAxes { get; set; }
    public ObservableCollection<Axis> FatYAxes { get; set; }

    public void SetUpDiagrams()
    {
        // Lấy danh sách tất cả các món ăn từ database
        var foods = Sender.GetFoods(); // Lấy danh sách foods (có thông tin dinh dưỡng)
        var foodDict = foods.ToDictionary(food => food.Name, food => food); // Tạo dictionary để tra cứu 

        // Lấy danh sách các log
        LogList = new ObservableCollection<Log>(Sender.GetLogs());

        // Tính tổng dinh dưỡng theo ngày
        var nutrientsByDate = LogList
            .GroupBy(log => log.LogDate)
            .Select(group => new
            {
                // Note: Serving size = 100 gram.
                Date = group.Key,
                TotalCalories = group.Sum(log => log.TotalCalories),
                TotalProtein = group.Sum(log => log.LogFoodItems.Sum(item =>
                    foodDict.TryGetValue(item.FoodName, out var food) ? food.ProteinPer100g * (item.NumberOfServings) : 0)),
                TotalCarbs = group.Sum(log => log.LogFoodItems.Sum(item =>
                    foodDict.TryGetValue(item.FoodName, out var food) ? food.CarbsPer100g * (item.NumberOfServings) : 0)),
                TotalFat = group.Sum(log => log.LogFoodItems.Sum(item =>
                    foodDict.TryGetValue(item.FoodName, out var food) ? food.FatPer100g * (item.NumberOfServings) : 0))
            })
            .OrderBy(x => x.Date)
            .ToList();

        // Tao cac bieu do
        CaloriesSeries =
        [
            new LineSeries<double>
            {
                Values = nutrientsByDate.Select(x => x.TotalCalories).ToList(),
                Name = "Total Calories",
                LineSmoothness = 0,
                Stroke = new SolidColorPaint(new SKColor(0, 0, 255)) {StrokeThickness = 2},
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        ];

        ProteinSeries =
        [
            new LineSeries<double>
            {
                Values = nutrientsByDate.Select(x => x.TotalProtein).ToList(),
                Name = "Total Protein",
                LineSmoothness = 0,
                Stroke = new SolidColorPaint(new SKColor(255, 0, 0)) {StrokeThickness = 2},
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        ];

        CarbsSeries =
        [
            new LineSeries<double>
            {
                Values = nutrientsByDate.Select(x => x.TotalCarbs).ToList(),
                Name = "Total Carbs",
                LineSmoothness = 0,
                Stroke = new SolidColorPaint(new SKColor(0, 255, 0)) {StrokeThickness = 2},
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        ];

        FatSeries =
        [
            new LineSeries<double>
            {
                Values = nutrientsByDate.Select(x => x.TotalFat).ToList(),
                Name = "Total Fat",
                LineSmoothness = 0,
                Stroke = new SolidColorPaint(new SKColor(255, 170, 29)) {StrokeThickness = 2},
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        ];

        // XAxes dung chung hien thi ngay
        XAxes =
        [
            new() {
                Labels = nutrientsByDate.Select(x => x.Date?.ToString("MM/dd/yyyy")).ToList(),
                Name = "Date"
            }
        ];
        // Tao cac YAxes khac nhau
        CaloriesYAxes =
        [
            new() { Name = "Total Calories" }
        ];

        ProteinYAxes =
        [
            new() { Name = "Total Protein" }
        ];

        CarbsYAxes =
        [
            new() { Name = "Total Carbs" }
        ];

        FatYAxes =
        [
            new() { Name = "Total Fat" }
        ];
    }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
}
