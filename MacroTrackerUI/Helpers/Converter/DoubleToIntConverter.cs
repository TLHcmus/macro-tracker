using Microsoft.UI.Xaml.Data;
using System;

namespace MacroTrackerUI.Helpers.Converter;
/// <summary>
/// Converts a double value to an integer value.
/// </summary>
public class DoubleToIntConverter : IValueConverter
{
    /// <summary>
    /// Converts a double value to an integer value.
    /// </summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="language">The language of the conversion.</param>
    /// <returns>An integer value converted from the double value, or 0 if the value is not a double.</returns>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is double doubleValue)
        {
            // Chuyển đổi từ double sang int
            return (int)doubleValue;
        }
        return 0; // Trả về giá trị mặc định nếu không phải là double
    }

    /// <summary>
    /// This method is not implemented and will throw a <see cref="NotImplementedException"/> if called.
    /// </summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="language">The language of the conversion.</param>
    /// <returns>Throws a <see cref="NotImplementedException"/>.</returns>
    /// <exception cref="NotImplementedException">Always thrown as this method is not implemented.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException(); // Không cần sử dụng trong trường hợp này
    }
}
