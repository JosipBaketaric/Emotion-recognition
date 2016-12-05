using EmotionRecognition.Service.Utils;
using System.Collections.Generic;
using System.Drawing;

namespace EmotionRecognition.Service
{
    public static class ProcessImage
    {       
        public static List<double> Process(Bitmap image)
        {
            //50x85 1% lower than 150x200
            List<double> upperAndLowerFeatures = new List<double>();
            var faceImage = ImageUtils.Resize(image, 68, 80);
            faceImage = ImageUtils.ToGrayScale(faceImage);

            var imgCropUpper = ImageUtils.Crop(faceImage, (float)0.05, (float)0.05, (float)0.02, (float)0.45);  //0.05, 0.05, 0.02, 0.45
            var imgCropLower = ImageUtils.Crop(faceImage, (float)0.1, (float)0.1, (float)0.5, (float)0.0);  //0.1, 0.1, 0.5, 0.0

            //Regions for feature extraction
            var imgCropUpperLeft = ImageUtils.Crop(imgCropUpper, (float)0.0, (float)0.5, (float)0.00, (float)0.0); //0,0.5,0,0
            var imgCropUpperRight = ImageUtils.Crop(imgCropUpper, (float)0.5, (float)0.0, (float)0.00, (float)0.0); //0.5,0,0,0


            var imgCropLowerLeft = ImageUtils.Crop(imgCropLower, (float)0.0, (float)0.6, (float)0.0, (float)0.0);   //0.6, 0, 0, 0
            var imgCropLowerMiddle = ImageUtils.Crop(imgCropLower, (float)0.3, (float)0.3, (float)0.0, (float)0.0); //0.3, 0.3, 0, 0
            var imgCropLowerRight = ImageUtils.Crop(imgCropLower, (float)0.6, (float)0.0, (float)0.0, (float)0.0);  //0.6, 0, 0, 0      


            //Feature extraction (Gabor filters + PCA)
            var featuresUpperLeft = Filters.GaborFilter(imgCropUpperLeft);
            var featuresUpperRight = Filters.GaborFilter(imgCropUpperRight);

            var featuresLowerLeft = Filters.GaborFilter(imgCropLowerLeft);
            var featuresLowerMiddle = Filters.GaborFilter(imgCropLowerMiddle);
            var featuresLowerRight = Filters.GaborFilter(imgCropLowerRight);

            //Add features to one list

            foreach (var item in featuresUpperLeft)
                upperAndLowerFeatures.Add(item);
            foreach (var item in featuresUpperRight)
                upperAndLowerFeatures.Add(item);

            foreach (var item in featuresLowerLeft)
                upperAndLowerFeatures.Add(item);
            foreach (var item in featuresLowerMiddle)
                upperAndLowerFeatures.Add(item);
            foreach (var item in featuresLowerRight)
                upperAndLowerFeatures.Add(item);


            return upperAndLowerFeatures;
        }

    }


}
