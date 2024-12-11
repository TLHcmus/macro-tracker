using Microsoft.UI.Xaml.Data;
using System;

namespace MacroTrackerUI.Helpers.Converter;

class DateTimeToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is DateTime dateTime)
        {
            if (dateTime.Date == DateTime.Today)
            {
                return "Today";
            }
            else if (dateTime.Date == DateTime.Today.AddDays(-1))
            {
                return "Yesterday";
            }
            return dateTime.ToString("MMMM dd, yyyy");
        }
        throw new Exception("Value is not a DateTime object.");
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
