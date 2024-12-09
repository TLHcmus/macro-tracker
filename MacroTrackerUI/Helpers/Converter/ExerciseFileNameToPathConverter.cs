using MacroTrackerUI.Services.PathService;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace MacroTrackerUI.Helpers.Converter;

class ExerciseFileNameToPathConverter : IValueConverter
{
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

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
