using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker.Helper.Converter
{
    class ExerciseFileNameToPathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string path = Service.ServiceAssetsPathRegistry.RegisteredAssetsPath["ExerciseIcon"];
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
}
