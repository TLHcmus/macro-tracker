using System;
using System.Collections.Generic;

namespace MacroTrackerCore.Entities;

public partial class Food
{
    public string Name { get; set; } = null!;

    public double? CaloriesPer100g { get; set; }

    public double? ProteinPer100g { get; set; }

    public double? CarbsPer100g { get; set; }

    public double? FatPer100g { get; set; }

    public string? IconFileName { get; set; }

    public virtual ICollection<LogFoodItem> LogFoodItems { get; set; } = new List<LogFoodItem>();
}
