using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace EmotionRecognition.Service
{
    public class Classifier
    {
        private CascadeClassifier CascadeClassifier { get; set; }
        private string TrainingSets { get; set; }
        private int MinSize { get; set; }
        private int MaxSize { get; set; }
        private double ScaleFactor { get; set; }
        private int MinNeighbours { get; set; }

        public Classifier(string trainingSets, int minSize, int maxSize, int minNeighbours, double scaleFactor)
        {
            this.TrainingSets = trainingSets;
            this.MinSize = minSize;
            this.MaxSize = maxSize;
            this.MinNeighbours = minNeighbours;
            this.ScaleFactor = scaleFactor;
            this.CascadeClassifier = new CascadeClassifier(trainingSets);
        }

        public Bitmap Find(Bitmap image)
        {
            try
            {
                Image<Bgr, byte> imageFrame = new Image<Bgr, byte>(image);
                //To gray scale
                Image<Gray, byte> grayScaleImage = imageFrame.Convert<Gray, byte>();

                Rectangle[] rectangles = CascadeClassifier.DetectMultiScale(grayScaleImage, ScaleFactor, MinNeighbours, new Size(MinSize, MinSize), new Size(MaxSize, MaxSize));

                //Check result
                if (rectangles.Length == 0)
                    return null;

                int targetRectangle = 0;
                int targetSize = 0;

                if (rectangles.Length > 1)
                {

                    for (int i = 0; i < rectangles.Length; i++)
                    {
                        if (targetSize < (rectangles[i].Height * rectangles[i].Width))
                        {
                            targetRectangle = i;
                            targetSize = rectangles[i].Height * rectangles[i].Width;
                        }
                    }
                }

                //calculate 15%
                //int width10 = (int)Math.Round(((double)rectangles[targetRectangle].Width * 0.10));
                //int height15 = (int)Math.Round(((double)rectangles[targetRectangle].Height * 0.15));
                //Get only marked object from image
                //rectangles[targetRectangle].Inflate(width10, 0);
                Bitmap responseImage = GetObject(image, rectangles[targetRectangle]);

                return responseImage;
            }
           catch(Exception e)
            {
                throw e;
            }
        }

        private Bitmap GetObject(Bitmap sourceImage, Rectangle section)
        {
            try
            {
                Bitmap bmp = new Bitmap(section.Width, section.Height);
                Graphics g = Graphics.FromImage(bmp);

                Rectangle destination = new Rectangle(0, 0, bmp.Width, bmp.Height);

                g.DrawImage(sourceImage, destination, section, GraphicsUnit.Pixel);

                g.Dispose();

                return bmp;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

    }

}
