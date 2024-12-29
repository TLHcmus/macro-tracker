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
    private DaoSender Sender { get; } = ProviderUI.GetServiceProvider().GetRequiredService<DaoSender>();

    /// <summary>
    /// Gets or sets the series for the chart.
    /// </summary>
    public ObservableCollection<ISeries> Series { get; set; }

    /// <summary>
    /// Gets or sets the X axes for the chart.
    /// </summary>
    public ObservableCollection<Axis> XAxes { get; set; }

    /// <summary>
    /// Gets or sets the Y axes for the chart.
    /// </summary>
    public ObservableCollection<Axis> YAxes { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportViewModel"/> class.
    /// </summary>
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
    }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
}
