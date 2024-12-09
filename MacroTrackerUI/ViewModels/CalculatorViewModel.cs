using MacroTrackerUI.Models;

namespace MacroTrackerUI.ViewModels;

public partial class CalculatorViewModel
{
    public HealthInfo HealthInfo { get; set; }

    public CalculatorViewModel()
    {
        HealthInfo = new HealthInfo();
    }
}
