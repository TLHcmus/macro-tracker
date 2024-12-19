using System;
using System.Collections.Generic;

namespace MacroTrackerCore.Entities;

public partial class Food : Item
{
    public double CaloriesPer100g { get; set; }

    public double ProteinPer100g { get; set; }

    public double CarbsPer100g { get; set; }

    public double FatPer100g { get; set; }
}
