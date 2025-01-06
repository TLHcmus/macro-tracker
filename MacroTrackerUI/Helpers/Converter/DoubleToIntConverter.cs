using Microsoft.UI.Xaml.Data;
using System;

namespace MacroTrackerUI.Helpers.Converter;
public class DoubleToIntConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is double doubleValue)
        {
            // Chuyển đổi từ double sang int
            return (int)doubleValue;
        }
        return 0; // Trả về giá trị mặc định nếu không phải là double
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException(); // Không cần sử dụng trong trường hợp này
    }
}