using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord;
using System.Drawing.Imaging;

namespace EmotionRecognition.Service
{
    public static class GaborFeatures
    {
        private static Bitmap ApplyFilter(Bitmap image)
        {
            var bitmap24 = Get24bppRgb(image);
            var gaborFilter = new Accord.Imaging.Filters.GaborFilter();

            var filteredImage = gaborFilter.Apply(bitmap24);

            return filteredImage;
        }

        public static double[] GetFeatures(Bitmap image)
        {
            var filteredImage = ApplyFilter(image);
            var imageMatrix = getSourceMatrix(filteredImage);
            var pca = new Accord.Statistics.Analysis.PrincipalComponentAnalysis();
            var x = pca.Learn(imageMatrix);
            var no = x.NumberOfOutputs;
            var outputs = pca.Eigenvalues;
            return outputs;
        }

        // Convert to Format24bppRgb
        private static Bitmap Get24bppRgb(Image image)
        {
            var bitmap = new Bitmap(image);
            var bitmap24 = new Bitmap(bitmap.Width, bitmap.Height, PixelFormat.Format24bppRgb);
            using (var gr = Graphics.FromImage(bitmap24))
            {
                gr.DrawImage(bitmap, new Rectangle(0, 0, bitmap24.Width, bitmap24.Height));
            }
            return bitmap24;
        }

        private static double[][] getSourceMatrix(Bitmap bmp)
        {
            double[][] matrix = new double[bmp.Height][];
            for (int i = 0; i < bmp.Height; i++)
            {
                matrix[i] = new double[bmp.Width];
            }

            for (int i = 0; i < bmp.Height; i++)
            {
                for (int j = 0; j < bmp.Width; j++)
                {
                    matrix[i][j] = (bmp.GetPixel(j, i).R + bmp.GetPixel(j, i).G + bmp.GetPixel(j, i).B) / 3;
                }
            }

            return matrix;
        }//End of getSourceMatrix

    }
}
