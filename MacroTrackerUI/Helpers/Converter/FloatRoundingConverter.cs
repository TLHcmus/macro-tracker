using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUI.Helpers.Converter;

public class FloatRoundingConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value == null)
            return ((double) 0).ToString();

        if (value is double doubleValue)
        {
            return Math.Round(doubleValue, 1).ToString();
        }
        throw new Exception($"Value is not a double object and the type of value is {value.GetType().ToString}.");
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
