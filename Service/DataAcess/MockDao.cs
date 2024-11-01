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
}
