using Microsoft.UI.Xaml.Data;
using System;

namespace MacroTrackerUI.Helpers.Converter;

/// <summary>
/// Converts a duration value to a string representation in minutes.
/// </summary>
public class DurationToMinuteCoverter : IValueConverter
{
    /// <summary>
    /// Converts a duration value to a string representation in minutes.
    /// </summary>
    /// <param name="value">The duration value to convert.</param>
    /// <param name="targetType">The type of the target property. This parameter is not used.</param>
    /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
    /// <param name="language">The language of the conversion. This parameter is not used.</param>
    /// <returns>A string representation of the duration in minutes, or an empty string if the value is not a double.</returns>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is double duration)
        {
            return $"{duration} minutes";
        }
        return string.Empty;
    }

    /// <summary>
    /// Converts a string representation of a duration back to a double value.
    /// This method is not implemented and will throw a <see cref="NotImplementedException"/>.
    /// </summary>
    /// <param name="value">The string representation of the duration.</param>
    /// <param name="targetType">The type of the target property. This parameter is not used.</param>
    /// <param name="parameter">An optional parameter to be used in the converter logic. This parameter is not used.</param>
    /// <param name="language">The language of the conversion. This parameter is not used.</param>
    /// <returns>Throws a <see cref="NotImplementedException"/>.</returns>
    /// <exception cref="NotImplementedException">Always thrown as this method is not implemented.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
