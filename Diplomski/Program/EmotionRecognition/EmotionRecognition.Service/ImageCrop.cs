using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EmotionRecognition.Service
{
    public class ImageCrop
    {
        private static ImageCrop Instance = null;

        private ImageCrop() { }

        public static ImageCrop GetInstance()
        {
            if (Instance != null)
                return Instance;
            Instance = new ImageCrop();
            return Instance;
        }

        public Bitmap Crop(Bitmap image, float leftMargine, float rightMargine, float topMargine, float bottomMargine)
        {
            if (image == null)
                return null;
            if (leftMargine + rightMargine > 0.99)
                return null;
            if (topMargine + bottomMargine > 0.99)
                return null;

            //Calculate width and height
            double dWidth = (1 - leftMargine - rightMargine) * image.Width;
            double dHeight = (1- bottomMargine - topMargine ) * image.Height;
            int width = (int)Math.Round(dWidth);
            int height = (int)Math.Round(dHeight);

            Rectangle destination = new Rectangle(0, 0, width, height);
            Bitmap bmp = new Bitmap(width, height);

            //Create rectangle from source image
            Graphics g = Graphics.FromImage(bmp);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //Calculate X, Y coordinate, width and height
            int xCoordinate = (int)Math.Round( leftMargine * image.Width );
            int yCoordinate = (int)Math.Round(topMargine * image.Height);
            int rWidth = (int)Math.Round( (1 - rightMargine - leftMargine) * image.Width );
            int rHeight = (int)Math.Round( (1 - bottomMargine - topMargine) * image.Height );

            Rectangle section = new Rectangle(xCoordinate, yCoordinate, rWidth, rHeight);

            //Draw
            g.DrawImage(image, destination, section, GraphicsUnit.Pixel);

            g.Dispose();

            return bmp;
        }


    }


}
