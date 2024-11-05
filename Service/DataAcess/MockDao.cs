using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroTracker.Model;

namespace MacroTracker.Service.DataAcess;
public class MockDao : IDao
{
    public List<Exercise> GetExercises() => throw new NotImplementedException();
    
    public List<Food> GetFoods() => throw new NotImplementedException();

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