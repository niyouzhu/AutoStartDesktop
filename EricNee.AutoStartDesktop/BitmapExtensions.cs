using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace System.Drawing
{
    public static class BitmapExtensions
    {
        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            var rt = new BitmapImage();
            rt.BeginInit();
            rt.StreamSource = new MemoryStream();
            bitmap.Save(rt.StreamSource, ImageFormat.Png);
            rt.EndInit();
            return rt;
        }

        public static byte[] ToBytes(this Bitmap bitmap)
        {
            using (var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                var bytes = new byte[stream.Length];
                stream.Write(bytes, 0, (int)stream.Length);
                return bytes;
            }

        }
    }
}
