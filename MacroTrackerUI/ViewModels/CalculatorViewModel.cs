using MacroTrackerUI.Models;

namespace MacroTrackerUI.ViewModels;

public partial class CalculatorViewModel
{
    public Health HealthInfo { get; set; }

    public CalculatorViewModel()
    {
        HealthInfo = new Health();
    }
}
