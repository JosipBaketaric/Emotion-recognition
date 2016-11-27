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
    public class ProcessImage
    {
        private static string SaveData = @"C:\Users\Josip\Desktop\testBase";
        private static string SaveFeaturesPath = @"C:\Users\Josip\Desktop\testBase\Features.txt";
        private static int FeatureNumber { get; set; }
        private bool fHeaderAppended { get; set; }

        private static ProcessImage Instance = null;
        private Classifier FaceClassifier { get; set; }
        private Classifier EyeClassifier { get; set; }
        private Classifier MouthClassifier { get; set; }
        private Classifier NoseClassifier { get; set; }
        private ImageCrop Crop { get; set; }
        private ResizeImage Resize { get; set; }

        private ProcessImage()
        {
            FaceClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\haarcascade_frontalcatface_extended.xml", 50, 1500, 3, 1.03);
            EyeClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\haarcascade_eye.xml", 20, 500, 2, 1.02);
            MouthClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\Mouth.xml", 30, 500, 1, 1.02);
            NoseClassifier = new Classifier(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\Nose 25x15.xml", 30, 500, 1, 1.03);
            Crop = ImageCrop.GetInstance();
            Resize = ResizeImage.GetInstance();
            FeatureNumber = 0;
            fHeaderAppended = false;
        }

        public static ProcessImage GetInstance()
        {
            if (Instance != null)
                return Instance;
            Instance = new ProcessImage();
            return Instance;
        }

        public bool Process(string fileLocation)
        {
            if (fileLocation == null)
                return false;

            string fileName = Path.GetFileNameWithoutExtension(fileLocation);
            string folderLocation = Path.GetDirectoryName(fileLocation);
            var folderLocationSplit = folderLocation.Split('\\');
            string folderName = folderLocationSplit.Last<String>();
            string emotionCode = fileName.Substring(fileName.Length - 3, 2);

            Bitmap image = new Bitmap(fileLocation);
            if (image == null)
                return false;

            var faceImage = FaceClassifier.Find(image);
            if (faceImage == null)
                return false;

            if (!System.IO.Directory.Exists(SaveData + "\\" + folderName + "\\" + fileName))
                Directory.CreateDirectory(SaveData + "\\" + folderName + "\\" + fileName);

            faceImage.Save(SaveData + "\\" + folderName + "\\" + fileName + "\\" + "face.jpg");

            //Split face in region
            Bitmap noseRegion = Crop.Crop(faceImage, (float)0.3, (float)0.3, (float)0.2, (float)0.2);
            Bitmap leftEyeRegion = Crop.Crop(faceImage, (float)0.0, (float)0.35, (float)0.0, (float)0.3);
            Bitmap rightEyeRegion = Crop.Crop(faceImage, (float)0.35, (float)0.0, (float)0.0, (float)0.3);
            Bitmap mouthRegion = Crop.Crop(faceImage, (float)0.1, (float)0.1, (float)0.7, (float)0.0);

            var nose = SaveImage.Save((SaveData + "\\" + folderName + "\\" + fileName + "\\" + "nose.jpg"), noseRegion, (SaveData + "\\" + folderName + "\\" + fileName + "\\Regions\\" + "NoseRegion.jpg"), NoseClassifier, 50, 50);
            var leftEye = SaveImage.Save((SaveData + "\\" + folderName + "\\" + fileName + "\\" + "leftEye.jpg"), leftEyeRegion, (SaveData + "\\" + folderName + "\\" + fileName + "\\Regions\\" + "leftEyeRegion.jpg"), EyeClassifier, 30, 30);
            var rightEye = SaveImage.Save((SaveData + "\\" + folderName + "\\" + fileName + "\\" + "rightEye.jpg"), rightEyeRegion, (SaveData + "\\" + folderName + "\\" + fileName + "\\Regions\\" + "rightEyeRegion.jpg"), EyeClassifier, 30, 30);
            var mouth = SaveImage.Save((SaveData + "\\" + folderName + "\\" + fileName + "\\" + "mouth.jpg"), mouthRegion, (SaveData + "\\" + folderName + "\\" + fileName + "\\Regions\\" + "\\" + "mouthRegion.jpg"), MouthClassifier, 50, 50);

            if(leftEye == null && rightEye != null)
            {
                leftEye = rightEye;
                leftEye = Resize.Resize(leftEye, 30, 30);
                leftEye.Save(SaveData + "\\" + folderName + "\\" + fileName + "\\" + "leftEye.jpg");
            }
            if (rightEye == null && leftEye != null)
            {
                rightEye = leftEye;
                rightEye = Resize.Resize(rightEye, 30, 30);
                rightEye.Save(SaveData + "\\" + folderName + "\\" + fileName + "\\" + "rightEye.jpg");
            }

            if(rightEye == null || leftEye == null)
            {
                rightEye = rightEyeRegion;
                leftEye = leftEyeRegion;

                rightEye = Resize.Resize(rightEye, 30, 30);
                rightEye.Save(SaveData + "\\" + folderName + "\\" + fileName + "\\" + "rightEye.jpg");
                leftEye = Resize.Resize(leftEye, 30, 30);
                leftEye.Save(SaveData + "\\" + folderName + "\\" + fileName + "\\" + "leftEye.jpg");
            }

            if(mouth == null)
            {
                mouth = MouthClassifier.Find(faceImage);
                if(mouth == null)
                {
                    mouth = MouthClassifier.Find(image);
                }
                if(mouth == null)
                {
                    var newMouthRegion = Crop.Crop(image, (float)0.1, (float)0.1, (float)0.6, (float)0.0);
                    mouth = newMouthRegion;
                }
                mouth = Resize.Resize(mouth, 50, 50);
            }

            if(nose == null)
            {
                nose = NoseClassifier.Find(faceImage);
                if (nose == null)
                {
                    nose = NoseClassifier.Find(image);
                }
                if (nose == null)
                {
                    var newNoseRegion = Crop.Crop(image, (float)0.3, (float)0.3, (float)0.1, (float)0.2);
                    nose = newNoseRegion;
                }
                nose = Resize.Resize(nose, 50, 50);
            }


            if (nose == null || leftEye == null || rightEye == null || mouth == null)
                return false;

            //Features
            List<double[]> featureList = new List<double[]>();

            //Using detected objects
            //var noseFeatures = GaborFeatures.GetFeatures(nose);
            //var leftEyeFeatures = GaborFeatures.GetFeatures(leftEye);
            //var rightEyeFeatures = GaborFeatures.GetFeatures(rightEye);
            //var mouthFeatures = GaborFeatures.GetFeatures(mouth);

            //Using regions
            var noseFeatures = GaborFeatures.GetFeatures( Resize.Resize(noseRegion, 50, 50));
            var leftEyeFeatures = GaborFeatures.GetFeatures(Resize.Resize(leftEyeRegion, 30, 30));
            var rightEyeFeatures = GaborFeatures.GetFeatures(Resize.Resize(rightEyeRegion, 30, 30));
            var mouthFeatures = GaborFeatures.GetFeatures(Resize.Resize(mouthRegion, 50, 50));

            featureList.Add(noseFeatures);
            featureList.Add(leftEyeFeatures);
            featureList.Add(rightEyeFeatures);
            featureList.Add(mouthFeatures);

            if (!fHeaderAppended)
            {
                FeatureNumber = noseFeatures.Length + leftEyeFeatures.Length + rightEyeFeatures.Length + mouthFeatures.Length;
                WriteFeatures.AppendHeader(SaveFeaturesPath, FeatureNumber + 1);
                fHeaderAppended = true;
            }
            else
            {
                int featureNumber = noseFeatures.Length + leftEyeFeatures.Length + rightEyeFeatures.Length + mouthFeatures.Length;
                if (FeatureNumber != featureNumber)
                    return false;
            }

            WriteFeatures.Write(featureList, emotionCode);

            return true;
        }

    }   
}
