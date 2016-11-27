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

        public Classifier(string trainingSets, int minSize, int maxSize)
        {
            this.TrainingSets = trainingSets;
            this.MinSize = minSize;
            this.MaxSize = maxSize;
            this.CascadeClassifier = new CascadeClassifier(trainingSets);
        }

        public Bitmap Find(Bitmap image)
        {
            Image<Bgr, byte> imageFrame = new Image<Bgr, byte>(image);
            //To gray scale
            Image<Gray, byte> grayScaleImage = imageFrame.Convert<Gray, byte>();

            Rectangle[] rectangles = CascadeClassifier.DetectMultiScale(grayScaleImage, 1.3, 1, new Size(MinSize, MinSize), new Size(MaxSize, MaxSize));

            //Check result
            if (rectangles.Length == 0)
                return null;

            //TODO MOVE THIS IN FUNCTION, ADD REST PARAMETERS TO CONSTRUCTOR, AND FIND FACE ON BOTTOM OF FACE IMAGE
            int targetRectangle = 0;
            int targetSize = 0;

            if (rectangles.Length > 1)
            {
            
                for(int i = 0; i < rectangles.Length; i++)
                {
                    if(targetSize < rectangles[i].Height)
                    {
                        targetRectangle = i;
                        targetSize = rectangles[i].Height;
                    }                       
                }
            }
            //Get only marked object from image
            Bitmap responseImage = GetObject(image, rectangles[targetRectangle]);

            return responseImage;
        }

        private Bitmap GetObject(Bitmap sourceImage, Rectangle section)
        {
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            Graphics g = Graphics.FromImage(bmp);

            Rectangle destination = new Rectangle(0, 0, bmp.Width, bmp.Height);

            g.DrawImage(sourceImage, destination, section, GraphicsUnit.Pixel);

            g.Dispose();

            return bmp;
        }

    }

}
