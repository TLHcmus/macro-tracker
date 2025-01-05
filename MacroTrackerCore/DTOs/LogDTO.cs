using MacroTrackerCore.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerCore.DTOs;

public class LogDTO
{
    public int LogId { get; set; }

    public DateOnly? LogDate { get; set; }

    public double TotalCalories { get; set; }

    public List<LogExerciseItemDTO> LogExerciseItems { get; set; }

    public List<LogFoodItemDTO> LogFoodItems { get; set; }
}
