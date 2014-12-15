using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace OrdersDb.Domain.Utils
{
    public static class ImageUtils
    {
        public static Image ResizeImage(Image imgToResize, Size size)
        {
            var sourceWidth = imgToResize.Width;
            var sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = (size.Width / (float)sourceWidth);
            nPercentH = (size.Height / (float)sourceHeight);

            nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);

            var bitmap = new Bitmap(destWidth, destHeight);
            var g = Graphics.FromImage(bitmap);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return bitmap;
        }

        public static byte[] ResizeAndConvertToJpg(byte[] imageData, int width = 100, int height = 100)
        {
            var fullImage = ConvertToJpg(imageData);
            var bitmap = fullImage.ToBitmap();
            var resized = ResizeImage(bitmap, new Size(width, height));
            return resized.ToByteArray(ImageFormat.Jpeg);
        }


        private static Image cropImage(Image img, Rectangle cropArea)
        {
            var bmpImage = new Bitmap(img);
            var bmpCrop = bmpImage.Clone(cropArea,
                bmpImage.PixelFormat);
            return bmpCrop;
        }

        public static byte[] ToByteArray(this Image image, ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        }

        public static Bitmap ToBitmap(this byte[] array)
        {
            using (var image = Image.FromStream(new MemoryStream(array)))
            {
                return new Bitmap(image);
            }
        }

        public static byte[] ConvertToJpg(byte[] bytes)
        {
            using (var bitmap = bytes.ToBitmap())
            {
                return bitmap.ToByteArray(ImageFormat.Jpeg);
            }
        }

    }
}