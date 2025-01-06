using Microsoft.UI.Xaml.Media.Imaging;
using Windows.Storage.Streams;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;

namespace MacroTrackerUI.Helpers
{
    public static class ImageHelper
    {
        // Chuyen mang byte[] thanh bitmap image
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

        // Doc file thanh mang byte[]
        public static async Task<byte[]> ReadFileToByteArrayAsync(string filePath)
        {
            var file = await Windows.Storage.StorageFile.GetFileFromPathAsync(filePath);
            using (var stream = await file.OpenReadAsync())
            {
                var reader = new Windows.Storage.Streams.DataReader(stream);
                byte[] imageBytes = new byte[stream.Size];
                await reader.LoadAsync((uint)stream.Size);
                reader.ReadBytes(imageBytes);
                return imageBytes;
            }
        }
    }
}