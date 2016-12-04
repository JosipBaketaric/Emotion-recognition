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

        public static Bitmap SkinColorFilter(Bitmap image)
        {
            Color skinColor = image.GetPixel(13, 51);
            int skinColorInt = skinColor.ToArgb();
            int currentColorInt;
            double percentage = 0.0;
            bool isInRange;
            Color white = Color.White;
            Color black = Color.Black;

            for(int i = 0; i < image.Width; i++)
            {
                for(int j = 0; j < image.Height; j++)
                {
                    currentColorInt = image.GetPixel(i, j).ToArgb();
                    isInRange = IsValueInRange(skinColorInt, currentColorInt, percentage);

                    if (isInRange)
                        image.SetPixel(i, j, white);
                    else
                        image.SetPixel(i, j, black);
                }
            }
            return image;
        }

        public static bool IsValueInRange(int source, int chechValue, double percentage)
        {
            var percentage1 = 1 + percentage;

            if (source == chechValue)
                return true;

            if(source > chechValue)
            {
                if (chechValue * percentage1 <= source)
                    return true;
            }

            if (source < chechValue)
            {
                if (source * percentage1 >= chechValue)
                    return true;
            }
            return false;
        }



        public static List<double> GaborFilter(Bitmap image)
        {
            //Add try catch block
            List<GaborFilteredImage> filteredImagesList = new List<GaborFilteredImage>();
            List<double> featuresList = new List<double>();

            image = ImageUtils.ConvertTo24bpp(image);
            double theta = (Math.PI / 180) * 22.5;

            //for (int i = 0; i < 18; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        var filter = new Accord.Imaging.Filters.GaborFilter();
            //        //Setup
            //        filter.Lambda = i + 2;
            //        filter.Theta = theta * j;
            //        filter.Gamma = 0.5 + (j / 2); //67.2% filter.Gamma = 0.5 + (j/2);
            //        filter.Sigma = Math.PI; //PI
            //        filter.Psi = 0.5;   //0.5
            //        filter.Size = 2;    //2

            //        var filteredImage = filter.Apply(image);

            //        filteredImagesList.Add(new GaborFilteredImage() { Image = filteredImage, Orientation = theta, Wavelength = i });
            //        //filteredImage.Save(@"C:\Users\Josip\Desktop\filteri\" + i.ToString() + "-" + j.ToString() + ".bmp");
            //    }
            //}


            theta = (Math.PI / 180) * 22.5;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    var filter = new Accord.Imaging.Filters.GaborFilter();
                    //Setup
                    filter.Lambda = i + 6;  //i + 6      68.30
                    filter.Theta = theta * j;
                    filter.Gamma = 0.5 + (j/2); //filter.Gamma = 0.5 + (j/2);
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


    }
}
