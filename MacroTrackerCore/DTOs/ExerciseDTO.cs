using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerCore.DTOs;

public class ExerciseDTO
{
    public int ExerciseId { get; set; }
    public string Name { get; set; }

    public double CaloriesPerMinute { get; set; }

    public byte[] Image { get; set; }
}
