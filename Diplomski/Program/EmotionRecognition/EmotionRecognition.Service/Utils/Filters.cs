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
