using System.ComponentModel;
namespace MacroTrackerUI.Models;

public class LogFood : Log
{
    public Food Food { get; set; }
    public float Calories { get; set; }
    public float Quantity { get; set; }
}
