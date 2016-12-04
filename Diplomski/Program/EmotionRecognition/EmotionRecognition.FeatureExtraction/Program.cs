using EmotionRecognition.Service;
using EmotionRecognition.Service.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmotionRecognition.Weka;
using System.Diagnostics;
using EmotionRecognition.Service.Classifiers;

namespace EmotionRecognition.FeatureExtraction
{
    class Program
    {
        public static int featureNumber { get; set; }

        static void Main(string[] args)
        {
            //Features selected by weka
            //string featuresSelected = System.IO.File.ReadAllText(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\featureSelect50x50x4x6.txt");
            //var splitSelectedFeatures = featuresSelected.Split(',');
            //List<int> featuresSelectedList = new List<int>();
            //for(int i = 0; i < 100; i++)
            //{
            //    int tempOut;
            //    int.TryParse(splitSelectedFeatures[i], out tempOut);
            //    featuresSelectedList.Add(tempOut);
            //}


            //Classifiers.J48(@"E:\Features\FeaturesArff.arff");


            string trainingSet = @"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\EmotionRecognition.Common\HaarCascade\haarcascade_frontalcatface_extended.xml";
            string featurePath = @"E:\Features";

            bool headerAppended = false;

            Classifier faceClassifier = new Classifier(trainingSet, 80, 700, 2, 1.05);
            Bitmap image;
            Bitmap face;
            string emotionCode;

            string database = @"C:\Users\Josip\Desktop\KDEF - DATABASE\KDEF";
            List<string> dirs = new List<string>(System.IO.Directory.EnumerateDirectories(database));
            double progressTotal = dirs.Count;
            double progress = 0;
            Stopwatch stopWatch = new Stopwatch();
            
            Console.WriteLine("Starting...");
            Console.Write("Progress: " + progress.ToString() + " / " + progressTotal);

            stopWatch.Start();
            foreach (var directory in dirs)
            {                
                List<string> files = new List<string>(System.IO.Directory.EnumerateFiles(directory, "*S.jpg"));
                foreach (var file in files)
                {
                    //Load image
                    image = new Bitmap(file);
                    //Get face
                    face = faceClassifier.Find(image);
                    if (face == null)
                        continue;

                    var name = Path.GetFileNameWithoutExtension(file);
                    emotionCode = name.Substring(name.Length - 3, 2);
                    var features = ProcessImage.Process(face);

                    //Append header
                    if (!headerAppended)
                    {
                        featureNumber = features.Count;
                        headerAppended = true;
                        WriteFeatures.AppendHeader(featurePath + @"\Features.txt", featureNumber);
                        WriteFeatures_Arff.AppendHeader(featurePath + @"\FeaturesArff.arff", featureNumber);
                    }
                    //Write features
                    if(featureNumber == features.Count)
                    {
                        WriteFeatures.Write(features, emotionCode);
                        WriteFeatures_Arff.Write(features, emotionCode);
                    }
                    
                }

                progress++;
                ClearCurrentConsoleLine();
                Console.Write("Progress: " + progress.ToString() + " / " + progressTotal);
            }

            stopWatch.Stop();
            Console.WriteLine("\n" + "Time taken to build training data: " + (double)((double)stopWatch.ElapsedMilliseconds / (double)1000 / (double)60) + " min" + "\n");
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }


        //private static void Process(Bitmap image, string emotionCode, bool headerAppended, string path)
        //{
        //    image = ImageUtils.Resize(image, 50, 85);
        //    image = ImageUtils.ToGrayScale(image);

        //    //64.4556 % with face img 100x170 + face divided in 5 regions
        //    var imgCropUpper = ImageUtils.Crop(image, (float)0.05, (float)0.05, (float)0.02, (float)0.45);
        //    var imgCropLower = ImageUtils.Crop(image, (float)0.1, (float)0.1, (float)0.5, (float)0.0);

        //    //Regions for feature extraction
        //    var imgCropUpperLeft = ImageUtils.Crop(imgCropUpper, (float)0.0, (float)0.55, (float)0.00, (float)0.0);
        //    var imgCropUpperRight = ImageUtils.Crop(imgCropUpper, (float)0.55, (float)0.0, (float)0.00, (float)0.0);

        //    var imgCropLowerLeft = ImageUtils.Crop(imgCropLower, (float)0.0, (float)0.6, (float)0.0, (float)0.0);
        //    var imgCropLowerMiddle = ImageUtils.Crop(imgCropLower, (float)0.3, (float)0.3, (float)0.0, (float)0.0);
        //    var imgCropLowerRight = ImageUtils.Crop(imgCropLower, (float)0.6, (float)0.0, (float)0.0, (float)0.0);

        //    //Feature extraction (Gabor filters + PCA)
        //    var featuresUpperLeft = Filters.GaborFilter(imgCropUpperLeft);
        //    var featuresUpperRight = Filters.GaborFilter(imgCropUpperRight);

        //    var featuresLowerLeft = Filters.GaborFilter(imgCropLowerLeft);
        //    var featuresLowerMiddle = Filters.GaborFilter(imgCropLowerMiddle);
        //    var featuresLowerRight = Filters.GaborFilter(imgCropLowerRight);

        //    //Add features to one list
        //    List<double> upperAndLowerFeatures = new List<double>();

        //    foreach(var item in featuresUpperLeft)
        //        upperAndLowerFeatures.Add(item);
        //    foreach (var item in featuresUpperRight)
        //        upperAndLowerFeatures.Add(item);

        //    foreach (var item in featuresLowerLeft)
        //        upperAndLowerFeatures.Add(item);
        //    foreach (var item in featuresLowerMiddle)
        //        upperAndLowerFeatures.Add(item);
        //    foreach (var item in featuresLowerRight)
        //        upperAndLowerFeatures.Add(item);

        //    int featureNumberLocal = upperAndLowerFeatures.Count;

        //    //Save data
        //    if (!headerAppended)
        //    {
        //        featureNumber = featureNumberLocal;
        //        var fullPath = path + @"\Features.txt";
        //        WriteFeatures.AppendHeader(fullPath, featureNumber);
        //        var fullPathArff = path + @"\FeaturesArff.arff";
        //        WriteFeatures_Arff.AppendHeader(fullPathArff, featureNumber);
        //    }

        //    if(featureNumberLocal == featureNumber)
        //    {
        //        WriteFeatures.Write(upperAndLowerFeatures, emotionCode);
        //        WriteFeatures_Arff.Write(upperAndLowerFeatures, emotionCode);
        //    }
        //    else
        //    {
        //        //else
        //    }
        //}




    }


}
