using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroTracker.Model;

namespace MacroTracker.Service.DataAcess;
public interface IDao
{
    List<Food> GetFoods();

    List<Exercise> GetExercises();

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
}
