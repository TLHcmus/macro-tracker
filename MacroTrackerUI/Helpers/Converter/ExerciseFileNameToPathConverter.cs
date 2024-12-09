using MacroTrackerUI.Services.PathService;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace MacroTrackerUI.Helpers.Converter;

/// <summary>
/// Converts an exercise file name to a BitmapImage path.
/// </summary>
class ExerciseFileNameToPathConverter : IValueConverter
{
    /// <summary>
    /// Converts a file name to a BitmapImage path.
    /// </summary>
    /// <param name="value">The file name of the exercise icon.</param>
    /// <param name="targetType">The type of the target property.</param>
    /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
    /// <param name="language">The language of the conversion.</param>
    /// <returns>A BitmapImage object representing the exercise icon.</returns>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        string path = AssetsPathRegistry.RegisteredAssetsPath["ExerciseIcon"];
        try
        {
            string iconFileName = value as string;
            return new BitmapImage(new Uri($"{path}/{iconFileName}"));
        }
        catch (Exception)
        {
            return new BitmapImage(new Uri($"{path}/default.png"));
        }
    }

    /// <summary>
    /// Converts back is not implemented.
    /// </summary>
    /// <param name="value">The value produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
    /// <param name="language">The language of the conversion.</param>
    /// <returns>Throws NotImplementedException.</returns>
    /// <exception cref="NotImplementedException">Always thrown as this method is not implemented.</exception>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
