using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker.Model;

public class Food : INotifyPropertyChanged
{
    public string Name { get; set; }
    public int CaloriesPer100g { get; set; } 
    public int ProteinPer100g { get; set; }
    public int CarbsPer100g { get; set; }
    public int FatPer100g { get; set; }

    public (double Calories, double Protein, double Carbs, double Fat) GetNutrition(double grams)
    {
        double factor = grams / 100;
        return (CaloriesPer100g * grams, ProteinPer100g * grams, CarbsPer100g * grams, FatPer100g * grams);
    }

    public event PropertyChangedEventHandler PropertyChanged;  
}

