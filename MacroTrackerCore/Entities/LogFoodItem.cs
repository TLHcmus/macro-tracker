using System;
using System.Collections.Generic;

namespace MacroTrackerCore.Entities;

public partial class LogFoodItem
{
    public int LogFoodId { get; set; }

    public int? LogId { get; set; }

    public int? FoodId { get; set; }

    public double? NumberOfServings { get; set; }

    public double? TotalCalories { get; set; }

    public virtual Food? Food { get; set; }

    public virtual Log? Log { get; set; }
}
