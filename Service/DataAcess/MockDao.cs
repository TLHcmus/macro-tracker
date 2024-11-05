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