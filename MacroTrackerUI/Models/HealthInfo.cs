namespace MacroTrackerUI.Models;
/// <summary>
/// Represents the health information of an individual.
/// </summary>
public class HealthInfo
{
    /// <summary>
    /// Gets or sets the age of the individual.
    /// </summary>
    public int Age { get; set; }

    /// <summary>
    /// Gets or sets the weight of the individual in kilograms.
    /// </summary>
    public int Weight { get; set; }

    /// <summary>
    /// Gets or sets the height of the individual in centimeters.
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Gets or sets the activity level of the individual.
    /// </summary>
    public string ActivityLevel { get; set; }

    /// <summary>
    /// Gets or sets the gender of the individual.
    /// </summary>
    public string Gender { get; set; }
}
