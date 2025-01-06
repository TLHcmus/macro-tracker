using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace MacroTrackerUI.Helpers.Converter
{
    /// <summary>
    /// Converts a byte array to a BitmapImage for use in XAML bindings.
    /// </summary>
    public class ByteArrayToImageConverter : IValueConverter
    {
        /// <summary>
        /// Converts a byte array to a BitmapImage.
        /// </summary>
        /// <param name="value">The byte array to convert.</param>
        /// <param name="targetType">The target type (not used).</param>
        /// <param name="parameter">An optional parameter (not used).</param>
        /// <param name="language">The language (not used).</param>
        /// <returns>A BitmapImage created from the byte array, or null if the input is not a byte array.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is byte[] byteArray)
            {
                var bitmapImage = new BitmapImage();
                using (var stream = new InMemoryRandomAccessStream())
                {
                    stream.WriteAsync(byteArray.AsBuffer()).AsTask().Wait();
                    stream.Seek(0);
                    bitmapImage.SetSource(stream);
                }
                return bitmapImage;
            }
            return null;
        }

        /// <summary>
        /// Not implemented. Converts a BitmapImage back to a byte array.
        /// </summary>
        /// <param name="value">The value to convert back (not used).</param>
        /// <param name="targetType">The target type (not used).</param>
        /// <param name="parameter">An optional parameter (not used).</param>
        /// <param name="language">The language (not used).</param>
        /// <returns>Throws a NotImplementedException.</returns>
        /// <exception cref="NotImplementedException">Always thrown.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
