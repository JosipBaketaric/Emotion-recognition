using EmotionRecognition.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.FeatureExtraction
{
    public static class SaveImage
    {

        public static Bitmap Save(string imageSavePath, Bitmap imageRegion, string imageRegionSavePath, Classifier classifier, int resizeWidth, int resizeHeight)
        {
            if(imageRegion != null)
            {
                ResizeImage resize = ResizeImage.GetInstance();

                string imageRegionFolder = Path.GetDirectoryName(imageRegionSavePath);

                if (!System.IO.Directory.Exists(imageRegionFolder))
                    Directory.CreateDirectory(imageRegionFolder);

                imageRegion.Save(imageRegionSavePath);

                Bitmap classifiedObject = classifier.Find(imageRegion);
                if(classifiedObject != null)
                {
                    var resizedImage = resize.Resize(classifiedObject, resizeWidth, resizeHeight);                
                    resizedImage.Save(imageSavePath);
                    return resizedImage;
                }
            }
            return null;
        }


    }

}
