using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.Service.Write
{
    public static class WriteArff
    {

        private static string Path { get; set; }
        private static bool HasHeader = false;

        public static void Write(List<double> featuresArray, string emotion)
        {
            if (HasHeader && Path != null && !Path.Equals(""))
            {
                var writer = File.AppendText(Path);
                
                foreach (var feature in featuresArray)
                {
                    writer.Write(feature.ToGBString());
                    writer.Write(",");
                }//End of foreach
                writer.Write(emotion);
                writer.Write(writer.NewLine);
                writer.Flush();
                writer.Dispose();

            }
        }

        public static void AppendHeader(string path, int numberOfFeatures, String emotionCodes)
        {
            Path = path;
            HasHeader = true;

            var writer = File.AppendText(Path);
            writer.Write("%Title: Emotions Database");
            writer.Write(writer.NewLine);
            writer.Write("%Sources:");
            writer.Write(writer.NewLine);
            writer.Write("%\tD. Lundqvist, A. Flykt, & A. Öhman, The Karolinska Directed Emotional Faces - KDEF, CD ROM from Department of Clinical Neuroscience, Psychology section, Karolinska Institutet, ISBN 91-630-7164-9, 1998");
            writer.Write(writer.NewLine);
            writer.Write("%Creator:");
            writer.Write(writer.NewLine);
            writer.Write("%\tJosip Baketaric");
            writer.Write(writer.NewLine);

            writer.Write("@RELATION emotions");
            writer.Write(writer.NewLine);
            writer.Write(writer.NewLine);

            for (int i = 0; i < numberOfFeatures; i++)
            {
                writer.Write("@ATTRIBUTE " + (i + 1).ToString() + " REAL");
                writer.Write(writer.NewLine);
            }
            writer.Write("@ATTRIBUTE Class " + emotionCodes);
            writer.Write(writer.NewLine);
            writer.Write(writer.NewLine);
            writer.Write("@DATA");
            writer.Write(writer.NewLine);

            writer.Flush();
            writer.Dispose();
        }

    }


    public static class DoubleExtensions
    {
        public static string ToGBString(this double value)
        {
            return value.ToString(CultureInfo.GetCultureInfo("en-GB"));
        }
    }

}


