using MacroTrackerUI.Models;
using MacroTrackerUI.Services.ProviderService;
using MacroTrackerUI.Services.SenderService.DataAccessSender;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MacroTrackerUI.ViewModels;

/// <summary>
/// ViewModel for managing exercises.
/// </summary>
public class ExerciseViewModel : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the collection of exercises.
    /// </summary>
    public ObservableCollection<Exercise> Exercises { get; set; }

    /// <summary>
    /// Gets the data access object for sending data.
    /// </summary>
    private DaoSender Dao { get; } = ProviderUI.GetServiceProvider().GetService<DaoSender>();

    /// <summary>
    /// Initializes a new instance of the <see cref="ExerciseViewModel"/> class.
    /// </summary>
    public ExerciseViewModel()
    {
        Exercises = new ObservableCollection<Exercise>(Dao.GetExercises());
    }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
}
