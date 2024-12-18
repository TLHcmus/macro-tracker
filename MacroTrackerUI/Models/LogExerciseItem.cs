using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Models;

public class LogExerciseItem
{
    public int LogExerciseId { get; set; }

    public string ExerciseName { get; set; }

    public double Duration { get; set; }

    public double TotalCalories { get; set; }
}
