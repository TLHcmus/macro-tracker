using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerCore.DTOs;

/// <summary>
/// Data Transfer Object representing a food item with nutritional information.
/// </summary>
public class FoodDTO
{
    /// <summary>
    /// Gets or sets the unique identifier for the food item.
    /// </summary>
    public int FoodId { get; set; }

    /// <summary>
    /// Gets or sets the name of the food item.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the number of calories per 100 grams of the food item.
    /// </summary>
    public double CaloriesPer100g { get; set; }

    /// <summary>
    /// Gets or sets the amount of protein per 100 grams of the food item.
    /// </summary>
    public double ProteinPer100g { get; set; }

    /// <summary>
    /// Gets or sets the amount of carbohydrates per 100 grams of the food item.
    /// </summary>
    public double CarbsPer100g { get; set; }

    /// <summary>
    /// Gets or sets the amount of fat per 100 grams of the food item.
    /// </summary>
    public double FatPer100g { get; set; }

    /// <summary>
    /// Gets or sets the image of the food item as a byte array.
    /// </summary>
    public byte[] Image { get; set; }
}
