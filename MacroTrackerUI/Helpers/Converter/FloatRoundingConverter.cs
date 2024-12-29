using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Helpers.Converter;

/// <summary>
/// Converts a double value to a string with one decimal place rounded.
/// </summary>
public class FloatRoundingConverter : IValueConverter
{
    /// <summary>
    /// Converts a double value to a string with one decimal place rounded.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="language">The language of the conversion.</param>
    /// <returns>A string representation of the rounded double value.</returns>
    /// <exception cref="Exception">Thrown when the value is not a double object.</exception>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
            return ((double)0).ToString();

        if (value is double doubleValue)
        {
            return Math.Round(doubleValue, 1).ToString();
        }
        throw new Exception($"Value is not a double object and the type of value is {value.GetType().ToString()}.");
    }

    /// <summary>
    /// Not implemented. Converts a value back to its source type.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="language">The language of the conversion.</param>
    /// <returns>Throws NotImplementedException.</returns>
    /// <exception cref="NotImplementedException">Always thrown as this method is not implemented.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
