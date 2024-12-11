using System;

namespace MacroTrackerCore.Entities;

public class Log
{
    private static int IDCount { get; set; } = 0;
    public int ID { get; private set; }
    public DateTime Time { get; set; }

    public Log()
    {
        IDCount++;
        ID = IDCount;
    }
}
