using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace MacroTrackerUI.Helpers
{
    public static class ImageHelper
    {
        public static BitmapImage ConvertByteArrayToImage(byte[] imageBytes)
        {
            if (imageBytes == null || imageBytes.Length == 0)
                return null;

            using (var stream = new InMemoryRandomAccessStream())
            {
                stream.WriteAsync(imageBytes.AsBuffer()).AsTask().Wait();
                stream.Seek(0);
                var bitmapImage = new BitmapImage();
                bitmapImage.SetSource(stream);
                return bitmapImage;
            }
        }
    }
}