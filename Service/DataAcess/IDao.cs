using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroTracker.Model;

namespace MacroTracker.Service.DataAcess;
public interface IDao
{
    List<Food> GetFoods();

    ObservableCollection<ExerciseInfo> GetExercises();

    List<User> GetUsers();

    /// <summary>
    /// Check if username and password match
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>
    /// Return false if username doesn't exist or username doesn't match password
    /// Return true if  username and password match
    /// </returns>
    bool DoesUserMatchPassword(string username, string password);

    /// <summary>
    /// Check if username exists
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    bool DoesUsernameExist(string username);

    void AddUser(User user);

}
