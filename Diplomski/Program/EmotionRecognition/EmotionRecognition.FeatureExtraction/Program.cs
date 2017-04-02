using EmotionRecognition.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Diagnostics;

namespace EmotionRecognition.FeatureExtraction
{
    class Program
    {
        public static int featureNumber { get; set; }
        public static String emotionCodes = "{AF,AN,DI,HA,NE,SA,SU}";

        static void Main(string[] args)
        {
            //79.04 :D :P     
            string trainingSet = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\Data\HaarCascade\01 haarcascade_frontalface_alt_tree.xml");
            trainingSet = Path.GetFullPath(trainingSet);

            string featurePath = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\Data\");
            featurePath = Path.GetFullPath(featurePath);

            //Must be on desktop
            string database = @"C:\Users\"+ Environment.UserName + @"\Desktop\KDEF - DATABASE\KDEF";

            bool headerAppended = false;

            Classifier faceClassifier = new Classifier(trainingSet, 80, 700, 2, 1.05);
            Bitmap image;
            Bitmap face;
            string emotionCode;


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
                        Service.Write.WriteCSV.AppendHeader(featurePath + @"\Features.txt", featureNumber);
                        Service.Write.WriteArff.AppendHeader(featurePath + @"\FeaturesArff.arff", featureNumber, emotionCodes);
                    }
                    //Write features
                    if(featureNumber == features.Count)
                    {
                        Service.Write.WriteCSV.Write(features, emotionCode);
                        Service.Write.WriteArff.Write(features, emotionCode);
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



    }


}
