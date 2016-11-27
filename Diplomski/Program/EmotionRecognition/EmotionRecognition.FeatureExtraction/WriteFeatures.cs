﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.FeatureExtraction
{
    public static class WriteFeatures
    {
        private static string Path { get; set; }
        private static bool HasHeader = false;

        public static void Write(List<double[]> featuresList, string emotion)
        {
            if (HasHeader && Path != null && !Path.Equals(""))
            {
                var writer = System.IO.File.AppendText(Path);

                foreach (var featureArray in featuresList)
                {
                    foreach(var feature in featureArray)
                    {
                        writer.Write(feature.ToString());
                        writer.Write("\t");
                    }
                }//End of foreach
                writer.Write(emotion);
                writer.Write(writer.NewLine);
                writer.Flush();
                writer.Dispose();

            }                          
        }

        public static void AppendHeader(string path, int numberOfFeatures)
        {
            Path = path;
            HasHeader = true;

            var writer = System.IO.File.AppendText(Path);

            for(int i = 0; i < numberOfFeatures; i++)
            {
                writer.Write((i + 1).ToString());
                writer.Write("\t");
            }
            writer.Write("Class");
            writer.Write(writer.NewLine);
            writer.Flush();
            writer.Dispose();
        }

    }
}
