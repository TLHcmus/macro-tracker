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
}
