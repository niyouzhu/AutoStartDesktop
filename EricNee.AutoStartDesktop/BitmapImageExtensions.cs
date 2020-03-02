using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace System.Windows.Media.Imaging
{
    public static class BitmapImageExtensions
    {
        public static byte[] ToBytes(this BitmapImage bitmapImage)
        {
            var stream = bitmapImage.StreamSource;
            stream.Position = 0;
            var bytes = new byte[stream.Length];
            bitmapImage.StreamSource.Read(bytes, 0, bytes.Length);
            return bytes;
        }
    }
}


namespace System
{
    using System.IO;
    using System.Windows.Media.Imaging;

    public static class ByteExtensions
    {
        public static BitmapImage ToBitmapImage(this byte[] bytes)
        {
            var rt = new BitmapImage();
            rt.BeginInit();
            rt.StreamSource = new MemoryStream(bytes);
            rt.EndInit();
            return rt;
        }
    }

}
