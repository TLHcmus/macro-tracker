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

public class ReportViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Log> LogList { get; set; }

    private DaoSender Sender { get; } = ProviderUI.GetServiceProvider().GetRequiredService<DaoSender>();


    public ObservableCollection<ISeries> CaloriesSeries { get; set; }
    public ObservableCollection<ISeries> ProteinSeries { get; set; }
    public ObservableCollection<ISeries> CarbsSeries { get; set; }
    public ObservableCollection<ISeries> FatSeries { get; set; }
    public ObservableCollection<Axis> XAxes { get; set; }

    public ObservableCollection<Axis> CaloriesYAxes { get; set; }
    public ObservableCollection<Axis> ProteinYAxes { get; set; }
    public ObservableCollection<Axis> CarbsYAxes { get; set; }
    public ObservableCollection<Axis> FatYAxes { get; set; }

    public class NutritionData
    {
        public DateOnly Date { get; set; }
        public double TotalCalories { get; set; }
        public double TotalProtein { get; set; }
        public double TotalCarbs { get; set; }
        public double TotalFat { get; set; }
    }


    public ReportViewModel()
    {
        // Lấy danh sách tất cả các món ăn từ database
        var foods = Sender.GetFoods(); // Lấy danh sách foods (có thông tin dinh dưỡng)
        var foodDict = foods.ToDictionary(food => food.FoodId, food => food); // Tạo dictionary để tra cứu 

        // Tính toán các ngày trong vòng 7 ngày gần nhất
        var today = DateOnly.FromDateTime(DateTime.Today);
        var recentDates = Enumerable.Range(0, 7)
                                    .Select(offset => today.AddDays(-offset))
                                    .ToList();

        // Khởi tạo danh sách Log cho 7 ngày gần nhất
        LogList = new ObservableCollection<Log>();

        foreach (var date in recentDates)
        {
            // Lấy log cho từng ngày trong 7 ngày gần nhất và thêm vào LogList
            var logForDay = Sender.GetLogByDate(date);
            LogList.Add(logForDay);
        }

        // Khởi tạo danh sách dinh dưỡng theo ngày
        var nutrientsByDate = recentDates
                            .Select(date => new NutritionData
                            {
                                Date = date,
                                TotalCalories = LogList.Where(log => log?.LogDate == date)
                                                       .Sum(log => log?.TotalCalories ?? 0),
                                TotalProtein = LogList.Where(log => log?.LogDate == date)
                                                      .Sum(log => log?.LogFoodItems?.Sum(item =>
                                                          foodDict.TryGetValue(item.FoodId, out var food) && food != null
                                                              ? food.ProteinPer100g * (item.NumberOfServings / 100)
                                                              : 0) ?? 0),
                                TotalCarbs = LogList.Where(log => log?.LogDate == date)
                                                    .Sum(log => log?.LogFoodItems?.Sum(item =>
                                                          foodDict.TryGetValue(item.FoodId, out var food) && food != null
                                                              ? food.CarbsPer100g * (item.NumberOfServings / 100)
                                                              : 0) ?? 0),
                                TotalFat = LogList.Where(log => log?.LogDate == date)
                                                  .Sum(log => log?.LogFoodItems?.Sum(item =>
                                                          foodDict.TryGetValue(item.FoodId, out var food) && food != null
                                                              ? food.FatPer100g * (item.NumberOfServings / 100)
                                                              : 0) ?? 0)
                            })
                            .OrderBy(x => x.Date)
                            .ToList();


        // Nếu một ngày không có log thì gán giá trị 0 cho tất cả các chỉ số dinh dưỡng
        foreach (var date in recentDates)
        {
            if (!nutrientsByDate.Any(x => x.Date == date))
            {
                nutrientsByDate.Add(new NutritionData
                {
                    Date = date,
                    TotalCalories = 0,
                    TotalProtein = 0,
                    TotalCarbs = 0,
                    TotalFat = 0
                });
            }
        }

        // Tạo các biểu đồ
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

        // XAxes dùng chung hiển thị ngày
        XAxes = new ObservableCollection<Axis>
    {
        new Axis
        {
            Labels = nutrientsByDate.Select(x => x.Date.ToString("MM/dd/yyyy")).ToList(),
            Name = "Date"
        }
    };

        // Tạo các YAxes khác nhau
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



    public event PropertyChangedEventHandler PropertyChanged;
}
