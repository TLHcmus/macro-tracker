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
    /// Mock method to check if username and password match
    /// Username: admin
    /// Password: 123
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public bool DoesUserMatchPassword(string username, string password)
    {
        if (username == "admin" && password == "123")
            return true;
        return false;
    }
}
