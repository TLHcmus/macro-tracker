﻿using System;
using System.Collections.Generic;

namespace MacroTrackerCore.Entities;

public partial class Goal
{
    public int GoalId { get; set; }

    public int? Calories { get; set; }

    public int? Protein { get; set; }

    public int? Carbs { get; set; }

    public int? Fat { get; set; }

    public int? UserId { get; set; }

    public virtual User? User { get; set; }
}
