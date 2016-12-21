using Emgu.CV;
using Emgu.CV.Structure;
using EmotionRecognition.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.Service.Utils
{
    public static class Filters
    {

        //Hough stuff Try!!
        //img.cmp with default imgs and use mask as features!!!
        public static List<double> CannyEdgeDetection(Bitmap image)
        {
            List<double> featuresList = new List<double>();
            Image<Gray, byte> gray = new Image<Gray, byte>(image);
            Image<Gray, byte> cannyImg;

            cannyImg = gray.Canny(100, 200);

            var features = ImageUtils.To1DimDoubleArray(cannyImg.ToBitmap());

            foreach (var feature in features)
                featuresList.Add(feature);

            return featuresList;
        }

        public static List<double> SobelEdgeDetectio(Bitmap image)
        {
            List<double> featuresList = new List<double>();
            Image<Gray, byte> gray = new Image<Gray, byte>(image);
            Bitmap sobelBitmap;
            Image<Gray, float> sobel;

            sobel = gray.Sobel(0, 1, 3).Add(gray.Sobel(1, 1, 3)).AbsDiff(new Gray(0.0));
            sobelBitmap = sobel.ToBitmap();
            sobelBitmap.Save(@"C:\Users\Josip\Desktop\sobelTest\0-1.bmp");

            featuresList = ImageUtils.PCA(sobelBitmap);
            return featuresList;
        }
       
        public static List<double> GaborFilter(Bitmap image)
        {
            try
            {
                List<GaborFilteredImage> filteredImagesList = new List<GaborFilteredImage>();
                List<double> featuresList = new List<double>();

                image = ImageUtils.ConvertTo24bpp(image);
                double theta = (Math.PI / 180) * 22.5;  //(Math.PI / 180) * 22.5;

                //i=0; i < 4
                for (int i = 0; i < 4; i++)
                {
                    //j = 0; j < 4
                    for (int j = 0; j < 4; j++)
                    {
                        var filter = new Accord.Imaging.Filters.GaborFilter();
                        //Setup
                        filter.Lambda = i + 6;  //i + 6
                        filter.Theta = theta * j;
                        filter.Gamma = 0.5 + (j / 2); //filter.Gamma = 0.5 + (j/2);
                        filter.Sigma = Math.PI; //PI
                        filter.Psi = 0.5;   //0.5
                        filter.Size = 2;    //2
                        var filteredImage = filter.Apply(image);

                        filteredImagesList.Add(new GaborFilteredImage() { Image = filteredImage, Orientation = theta, Wavelength = i });
                        //filteredImage.Save(@"C:\Users\Josip\Desktop\filteri\" + i.ToString() + "-" + j.ToString() + ".bmp");
                    }
                }

                featuresList = ImageUtils.GaborFeatures(filteredImagesList);
                return featuresList;
            }
           catch(Exception e)
            {
                throw e;
            }
        }


    }
}
