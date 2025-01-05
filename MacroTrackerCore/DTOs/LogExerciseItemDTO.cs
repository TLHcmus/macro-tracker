using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerCore.DTOs;

public class LogExerciseItemDTO
{
    public int LogExerciseId { get; set; }

    public int ExerciseId { get; set; }

    public double Duration { get; set; }

    public double TotalCalories { get; set; }
}
