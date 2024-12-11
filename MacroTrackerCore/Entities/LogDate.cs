namespace MacroTrackerCore.Entities;

public class LogDate
{
    public int ID { get; set; }
    public DateTime Date { get; set; }
    public List<LogExercise> LogExercise { get; set; }
    public List<LogFood> LogFood { get; set; }
}
