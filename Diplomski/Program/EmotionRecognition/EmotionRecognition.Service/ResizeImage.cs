using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.Service
{
    public class ResizeImage
    {
        private static ResizeImage Instance = null;
        private ResizeImage() { }
        public static ResizeImage GetInstance()
        {
            if (Instance != null)
                return Instance;
            Instance = new ResizeImage();
            return Instance;
        }
        public Bitmap Resize(Bitmap image, int width, int height)    
        {
            if (image == null)
                return null;

            Bitmap resizedImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }
       
        
         
    }
}
