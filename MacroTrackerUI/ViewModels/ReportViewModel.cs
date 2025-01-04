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

    /// <summary>
    /// Sets up the diagrams for displaying nutritional data.
    /// </summary>
    public void SetUpDiagrams()
    {
        var foods = Sender.GetFoods();
        var foodDict = foods.ToDictionary(food => food.Name, food => food);

        LogList = new ObservableCollection<Log>(Sender.GetLogs());

        var nutrientsByDate = LogList
            .GroupBy(log => log.LogDate)
            .Select(group => new
            {
                Date = group.Key,
                TotalCalories = group.Sum(log => log.TotalCalories),
                TotalProtein = group.Sum(log => log.LogFoodItems.Sum(item =>
                    foodDict.TryGetValue(item.FoodName, out var food) ? food.ProteinPer100g * item.NumberOfServings : 0)),
                TotalCarbs = group.Sum(log => log.LogFoodItems.Sum(item =>
                    foodDict.TryGetValue(item.FoodName, out var food) ? food.CarbsPer100g * item.NumberOfServings : 0)),
                TotalFat = group.Sum(log => log.LogFoodItems.Sum(item =>
                    foodDict.TryGetValue(item.FoodName, out var food) ? food.FatPer100g * item.NumberOfServings : 0))
            })
            .OrderBy(x => x.Date)
            .ToList();

        CaloriesSeries = new ObservableCollection<ISeries>
        {
            new LineSeries<double>
            {
                Values = nutrientsByDate.Select(x => x.TotalCalories).ToList(),
                Name = "Total Calories",
                LineSmoothness = 0,
                Stroke = new SolidColorPaint(new SKColor(0, 0, 255)) { StrokeThickness = 2 },
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        };

        ProteinSeries = new ObservableCollection<ISeries>
        {
            new LineSeries<double>
            {
                Values = nutrientsByDate.Select(x => x.TotalProtein).ToList(),
                Name = "Total Protein",
                LineSmoothness = 0,
                Stroke = new SolidColorPaint(new SKColor(255, 0, 0)) { StrokeThickness = 2 },
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        };

        CarbsSeries = new ObservableCollection<ISeries>
        {
            new LineSeries<double>
            {
                Values = nutrientsByDate.Select(x => x.TotalCarbs).ToList(),
                Name = "Total Carbs",
                LineSmoothness = 0,
                Stroke = new SolidColorPaint(new SKColor(0, 255, 0)) { StrokeThickness = 2 },
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        };

        FatSeries = new ObservableCollection<ISeries>
        {
            new LineSeries<double>
            {
                Values = nutrientsByDate.Select(x => x.TotalFat).ToList(),
                Name = "Total Fat",
                LineSmoothness = 0,
                Stroke = new SolidColorPaint(new SKColor(255, 170, 29)) { StrokeThickness = 2 },
                Fill = null,
                GeometryFill = null,
                GeometryStroke = null
            }
        };

        XAxes = new ObservableCollection<Axis>
        {
            new Axis
            {
                Labels = nutrientsByDate.Select(x => x.Date?.ToString("MM/dd/yyyy")).ToList(),
                Name = "Date"
            }
        };

        CaloriesYAxes = new ObservableCollection<Axis>
        {
            new Axis { Name = "Total Calories" }
        };

        ProteinYAxes = new ObservableCollection<Axis>
        {
            new Axis { Name = "Total Protein" }
        };

        CarbsYAxes = new ObservableCollection<Axis>
        {
            new Axis { Name = "Total Carbs" }
        };

        FatYAxes = new ObservableCollection<Axis>
        {
            new Axis { Name = "Total Fat" }
        };
    }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
}
