using Microsoft.UI.Xaml.Data;
using System;

namespace MacroTrackerUI.Helpers.Converter;

/// <summary>
/// Converts a <see cref="DateOnly"/> object to a string representation.
/// </summary>
public class DateTimeToStringConverter : IValueConverter
{
    /// <summary>
    /// Converts a <see cref="DateOnly"/> object to a string.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="language">The language of the conversion.</param>
    /// <returns>A string representation of the date.</returns>
    /// <exception cref="Exception">Thrown when the value is not a <see cref="DateOnly"/> object.</exception>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is DateOnly date)
        {
            if (date == DateOnly.FromDateTime(DateTime.Now))
            {
                return "Today";
            }
            else if (date == DateOnly.FromDateTime(DateTime.Today.AddDays(-1)))
            {
                return "Yesterday";
            }
            return date.ToString("MMMM dd, yyyy");
        }
        throw new Exception("Value is not a DateOnly object.");
    }

    /// <summary>
    /// Converts a value back. This method is not implemented.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="language">The language of the conversion.</param>
    /// <returns>Throws a <see cref="NotImplementedException"/>.</returns>
    /// <exception cref="NotImplementedException">Always thrown as this method is not implemented.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
