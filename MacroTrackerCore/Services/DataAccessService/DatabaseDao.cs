using System.Collections.ObjectModel;
using Microsoft.Extensions.DependencyInjection;
using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.ProviderService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Data;
using System.Reflection.Metadata;

namespace MacroTrackerCore.Services.DataAccessService;

public class DatabaseDao : IDao
{
    // Use Entity Framework to get exercises from the database
    private readonly MacroTrackerContext _context;

    public DatabaseDao()
    {
        _context = new MacroTrackerContext();
    }

    public List<Exercise> GetExercises()
    {
        return new List<Exercise>(_context.Exercises);
    }

    public ObservableCollection<Food> GetFoods()
    {
        return new ObservableCollection<Food>(_context.Foods);
    }

    public bool DoesUserMatchPassword(string username, string password) => throw new NotImplementedException();

    public bool DoesUsernameExist(string username) => throw new NotImplementedException();

    public void AddUser(User user) => throw new NotImplementedException();

    public List<User> GetUsers() => throw new NotImplementedException();

    public Goal GetGoal() => throw new NotImplementedException();

    public LogDate AddDefaultLogDate() => throw new NotImplementedException();

    public List<LogDate> GetAllLogs() => throw new NotImplementedException();

    public void AddLogDate(LogDate logDate) => throw new NotImplementedException();

    public void DeleteLogDate(int id) => throw new NotImplementedException();

}
