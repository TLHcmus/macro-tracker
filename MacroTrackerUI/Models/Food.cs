using System.ComponentModel;

namespace MacroTrackerUI.Models;

/// <summary>
/// Represents a food item with nutritional information.
/// </summary>
public class Food : INotifyPropertyChanged
{
    /// <summary>
    /// Gets or sets the name of the food.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Gets or sets the calories per 100 grams of the food.
    /// </summary>
    public double CaloriesPer100g { get; set; }

    /// <summary>
    /// Gets or sets the protein per 100 grams of the food.
    /// </summary>
    public double ProteinPer100g { get; set; }

    /// <summary>
    /// Gets or sets the carbohydrates per 100 grams of the food.
    /// </summary>
    public double CarbsPer100g { get; set; }

    /// <summary>
    /// Gets or sets the fat per 100 grams of the food.
    /// </summary>
    public double FatPer100g { get; set; }

    /// <summary>
    /// Gets or sets the file name of the icon representing the food.
    /// </summary>
    public string IconFileName { get; set; }

    /// <summary>
    /// Calculates the nutritional values based on the given grams of the food.
    /// </summary>
    /// <param name="grams">The weight of the food in grams.</param>
    /// <returns>A tuple containing the calories, protein, carbohydrates, and fat for the given weight.</returns>
    public (double Calories, double Protein, double Carbs, double Fat) GetNutrition(double grams)
    {
        double factor = grams / 100;
        return (CaloriesPer100g * factor, ProteinPer100g * factor, CarbsPer100g * factor, FatPer100g * factor);
    }

    /// <summary>
    /// Occurs when a property value changes.
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;
}

