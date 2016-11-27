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
            ProcessImage ProcessImage = ProcessImage.GetInstance();
            string database = @"C:\Users\Josip\Desktop\KDEF - DATABASE\KDEF";
            List<string> dirs = new List<string>(System.IO.Directory.EnumerateDirectories(database));
            double progressTotal = dirs.Count;
            double progress = 0;

            Console.WriteLine("Starting...");
            Console.Write("Progress: " + progress.ToString() + " / " + progressTotal);

            foreach (var directory in dirs)
            {                
                List<string> files = new List<string>(System.IO.Directory.EnumerateFiles(directory, "*S.jpg"));
                foreach (var file in files)
                {
                    ProcessImage.Process(file);
                }

                progress++;
                ClearCurrentConsoleLine();
                Console.Write("Progress: " + progress.ToString() + " / " + progressTotal);
            }


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
