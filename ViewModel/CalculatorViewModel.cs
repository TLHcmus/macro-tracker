using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MacroTracker.Model;
using Windows.System.Preview;

namespace MacroTracker.ViewModel;
public partial class CalculatorViewModel
{
    public HealthInfo healthInfo
    {
        get; set;
    }

    public CalculatorViewModel()
    {
        healthInfo = new HealthInfo();
    }
}
