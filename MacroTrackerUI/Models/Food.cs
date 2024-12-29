using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class Food : INotifyPropertyChanged
{
    public string Name { get; set; } = null!;

    public double CaloriesPer100g { get; set; }

    public double ProteinPer100g { get; set; }

    public double CarbsPer100g { get; set; }

    public double FatPer100g { get; set; }

    public string IconFileName { get; set; }

    public (double Calories, double Protein, double Carbs, double Fat) GetNutrition(double grams)
    {
        double factor = grams / 100;
        return (CaloriesPer100g * factor, ProteinPer100g * factor, CarbsPer100g * factor, FatPer100g * factor);
    }

    public event PropertyChangedEventHandler PropertyChanged;
}

