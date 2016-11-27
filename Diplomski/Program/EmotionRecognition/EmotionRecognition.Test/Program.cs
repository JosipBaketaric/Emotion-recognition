using EmotionRecognition.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var FaceClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\haarcascade_frontalcatface_extended.xml", 200, 1000);
            var EyeClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\haarcascade_eye.xml", 50, 800);
            var MouthClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\Mouth.xml", 50, 800);
                       
            var imagePath = @"C:\Users\Josip\Desktop\testBase\Miro.jpg";
            var Image = new Bitmap(imagePath);

            var savePathFace = @"C:\Users\Josip\Desktop\testBase\Face1.bmp";
            var savePathEye = @"C:\Users\Josip\Desktop\testBase\Eye1.bmp";
            var savePathMouth = @"C:\Users\Josip\Desktop\testBase\Mouth1.bmp";

            var Face = FaceClassifier.Find(Image);
            var Eye = EyeClassifier.Find(Image);
            Bitmap Mouth;

            if(Face != null)
                Mouth = MouthClassifier.Find(Face);              
            else
                Mouth = MouthClassifier.Find(Image);

            if (Face != null)
                Face.Save(savePathFace);
            if(Eye != null)
                Eye.Save(savePathEye);
            if (Mouth != null)
                Mouth.Save(savePathMouth);

        }

    }
}
