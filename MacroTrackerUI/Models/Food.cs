using System.ComponentModel;

namespace MacroTrackerUI.Models;

public class Food : Item, INotifyPropertyChanged
{
    public double CaloriesPer100g { get; set; }

    public double ProteinPer100g { get; set; }

    public double CarbsPer100g { get; set; }

    public double FatPer100g { get; set; }

    public (double Calories, double Protein, double Carbs, double Fat) GetNutrition(double grams)
    {
        double factor = grams / 100;
        return (CaloriesPer100g * grams, ProteinPer100g * grams, CarbsPer100g * grams, FatPer100g * grams);
    }

    public event PropertyChangedEventHandler PropertyChanged;
}

