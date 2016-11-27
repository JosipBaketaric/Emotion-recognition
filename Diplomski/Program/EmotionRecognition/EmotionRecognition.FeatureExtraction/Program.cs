using EmotionRecognition.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.FeatureExtraction
{
    class Program
    {
        static void Main(string[] args)
        {
            var FaceClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\haarcascade_frontalcatface_extended.xml", 200, 1000);
            var EyeClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\haarcascade_eye.xml", 50, 800);
            var MouthClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\Mouth.xml", 50, 800);
            var NoseClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\Nose 25x15.xml", 50, 800);
            var RightEyeClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\REye.xml", 50, 800);
            var LeftEyeClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\LEye.xml", 50, 800);

            var Crop = ImageCrop.GetInstance();

            var imagePath = @"C:\Users\Josip\Desktop\testBase\Miro.jpg";
            var Image = new Bitmap(imagePath);

            var savePathFace = @"C:\Users\Josip\Desktop\testBase\Face1.bmp";
            var savePathEye = @"C:\Users\Josip\Desktop\testBase\Eye1.bmp";
            var savePathMouth = @"C:\Users\Josip\Desktop\testBase\Mouth1.bmp";
            var savePathNose = @"C:\Users\Josip\Desktop\testBase\Nose1.bmp";
            var savePathRightEye = @"C:\Users\Josip\Desktop\testBase\RightEye1.bmp";
            var savePathLeftEye = @"C:\Users\Josip\Desktop\testBase\LeftEye1.bmp";


            //Find face
            var Face = FaceClassifier.Find(Image);

            //Split face in region
            Bitmap Nose = Crop.Crop(Face, (float)0.3, (float)0.3, (float)0.2, (float)0.2);

            if(Nose != null)
            {
                Nose.Save(@"C:\Users\Josip\Desktop\testBase\testImage1.bmp");
                Bitmap nose = NoseClassifier.Find(Nose);
                if (nose != null)
                    nose.Save(savePathNose);
            }
                

        }
    }
}
