using System.Runtime.CompilerServices;

namespace MacroTrackerCore.Entities;

public class LogDate
{
    private static int IDCount { get; set; } = 0;
    public int ID { get; private set; }
    public DateTime Date { get; set; }
    public List<LogExercise> LogExercise { get; set; }
    public List<LogFood> LogFood { get; set; }

    public LogDate()
    {
        IDCount++;
        ID = IDCount;
    }
}
