using Emgu.CV;
using Emgu.CV.Structure;
using EmotionRecognition.Common;
using EmotionRecognition.Service.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.Service
{
    public static class ImageUtils
    {

        public static Bitmap Crop(Bitmap img, float leftMargine, float rightMargine, float topMargine, float bottomMargine)
        {
            if (img == null)
                return null;
            if (leftMargine + rightMargine > 0.99)
                return null;
            if (topMargine + bottomMargine > 0.99)
                return null;

            //Calculate width and height
            double dWidth = (1 - leftMargine - rightMargine) * img.Width;
            double dHeight = (1 - bottomMargine - topMargine) * img.Height;
            int width = (int)Math.Round(dWidth);
            int height = (int)Math.Round(dHeight);

            Rectangle destination = new Rectangle(0, 0, width, height);
            Bitmap bmp = new Bitmap(width, height);

            //Create rectangle from source image
            Graphics g = Graphics.FromImage(bmp);
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

            //Calculate X, Y coordinate, width and height
            int xCoordinate = (int)Math.Round(leftMargine * img.Width);
            int yCoordinate = (int)Math.Round(topMargine * img.Height);
            int rWidth = (int)Math.Round((1 - rightMargine - leftMargine) * img.Width);
            int rHeight = (int)Math.Round((1 - bottomMargine - topMargine) * img.Height);

            Rectangle section = new Rectangle(xCoordinate, yCoordinate, rWidth, rHeight);

            //Draw
            g.DrawImage(img, destination, section, GraphicsUnit.Pixel);

            g.Dispose();

            return bmp;
        }

        public static Bitmap Resize(Bitmap img, int width, int height)
        {
            if (img == null)
                return null;

            Bitmap resizedImage = new Bitmap(width, height, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(img, 0, 0, width, height);
            }
            return resizedImage;
        }

        public static Bitmap ToGrayScale(Bitmap img)
        {
            Image<Bgr, byte> imageFrame = new Image<Bgr, byte>(img);
            //To gray scale
            Image<Gray, byte> grayScaleImage = imageFrame.Convert<Gray, byte>();

            return grayScaleImage.Bitmap;
        }

        public static Color GetMostUsedColor(Bitmap img)
        {
            List<Color> TenMostUsedColors = new List<Color>();
            List<int> TenMostUsedColorIncidences = new List<int>();
            Color MostUsedColor = Color.Empty;
            int MostUsedColorIncidence = 0;
            int pixelColor;
            Dictionary<int, int> dctColorIncidence = new Dictionary<int, int>();

            for (int row = 0; row < img.Size.Width; row++)
            {
                for (int col = 0; col < img.Size.Height; col++)
                {
                    pixelColor = img.GetPixel(row, col).ToArgb();

                    if (dctColorIncidence.Keys.Contains(pixelColor))
                    {
                        dctColorIncidence[pixelColor]++;
                    }
                    else
                    {
                        dctColorIncidence.Add(pixelColor, 1);
                    }
                }
            }

            // note that there are those who argue that a
            // .NET Generic Dictionary is never guaranteed
            // to be sorted by methods like this
            var dctSortedByValueHighToLow = dctColorIncidence.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            // this should be replaced with some elegant Linq ?
            foreach (KeyValuePair<int, int> kvp in dctSortedByValueHighToLow.Take(10))
            {
                TenMostUsedColors.Add(Color.FromArgb(kvp.Key));
                TenMostUsedColorIncidences.Add(kvp.Value);
            }

            MostUsedColor = Color.FromArgb(dctSortedByValueHighToLow.First().Key);
            MostUsedColorIncidence = dctSortedByValueHighToLow.First().Value;

            return MostUsedColor;
        }

        public static double[,] To2DimDoubleArray(Bitmap img)
        {
            var converter = new Accord.Imaging.Converters.ImageToMatrix();
            double[,] matrix;
            converter.Convert(img, out matrix);
            return matrix;
        }

        public static double[] To1DimDoubleArray(Bitmap img)
        {
            var converter = new Accord.Imaging.Converters.ImageToArray();
            double[] array;
            converter.Convert(img, out array);
            return array;
        }

        public static Bitmap ToImageFromMatrix(double[,] matrix)
        {
            var converter = new Accord.Imaging.Converters.MatrixToImage();
            Bitmap responseImage;
            converter.Convert(matrix, out responseImage);
            return responseImage;
        }

        public static Bitmap ConvertTo24bpp(Image img)
        {
            var bmp = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (var gr = Graphics.FromImage(bmp))
                gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
            return bmp;
        }

        //LBP
        public static int[] calculateLBP(Bitmap bmp)
        {
            List<int> LBPfeatures = new List<int>();
            int currentFeature;


            //Calculate LBP
            for (int i = 1; i < bmp.Height - 1; i++)
            {

                for (int j = 1; j < bmp.Width - 1; j++)
                {

                    currentFeature = calculateCurrentLBP(j, i, bmp);
                    //Write to array
                    LBPfeatures.Add(currentFeature);
                }//End of inner for

            }//End of outer for

            List<int> uniformLBPfeatures = new List<int>();

            //For each 8-bit binary, if it's uniform get its count from LBPfeatures
            for (int i = 0; i < 256; i++)
            {
                if (isUniform(i))
                {
                    int uc = LBPfeatures.FindAll(x => x == i).Count; //x so that x == i
                    //Add this number to the uniformLBPFeatures
                    uniformLBPfeatures.Add(uc);
                }
            }

            //Count all non-uniform
            int nuc = LBPfeatures.FindAll(x => !isUniform(x)).Count; //x so that x is not uniform
            uniformLBPfeatures.Add(nuc);

            //Convert List<int> to int[] and return it
            return uniformLBPfeatures.ToArray();
        }

        private static int calculateCurrentLBP(int i, int j, Bitmap bmp)
        {
            double[] arrayNeighbours = new double[8];
            int counter = 0;
            double temp;
            double centerValue = (bmp.GetPixel(i, j).R + bmp.GetPixel(i, j).G + bmp.GetPixel(i, j).B) / 3;
            for (int a = -1; a < 2; a++)
            {

                for (int b = -1; b < 2; b++)
                {
                    if (a == 0 && b == 0)
                        continue;

                    temp = bmp.GetPixel(i + a, j + b).R + bmp.GetPixel(i + a, j + b).G + bmp.GetPixel(i + a, j + b).B;
                    temp /= 3;

                    if (temp > centerValue)
                        arrayNeighbours[counter] = 1;
                    else
                        arrayNeighbours[counter] = 0;
                    counter++;
                }//End of for
            }//End of for

            String binaryCombination;
            //Order them clockwise
            binaryCombination = arrayNeighbours[0].ToString() + arrayNeighbours[1].ToString() + arrayNeighbours[2].ToString() +
                arrayNeighbours[4].ToString() + arrayNeighbours[7].ToString() + arrayNeighbours[6].ToString() +
                arrayNeighbours[5].ToString() + arrayNeighbours[3].ToString();

            try
            {
                int returnNumb = Convert.ToInt32(binaryCombination, 2);

                return returnNumb;
            }
            catch (Exception)
            {               
                return -1;
            }

        }

        //Check if 8-bit value is uniform
        private static bool isUniform(int value)
        {
            int counter = 0;
            //From weight 0 to weight 6
            for (int i = 1; i <= 64; i = i << 1)
            {
                //If current and next bit are different increase the counter
                int currentBit = value & i;
                int nextBit = value & (i << 1);
                if (currentBit != (nextBit >> 1))
                    counter++;
            }
            //If counter is greater than 2 return false
            return counter > 2 ? false : true;
        }//End of isUniform



        public static Bitmap NormalizeImage (Bitmap img)
        {
            var img2dArray = ImageUtils.To2DimDoubleArray(img);
            List<double> response2dArray = new List<double>();

            double oldMin = img2dArray[0,0];
            double oldMax = img2dArray[0, 0];
            double oldRange;

            double newMin = 0;
            double newMax = 255;
            double newRange = newMax - newMin;

            foreach (var value in img2dArray)
            {
                if (value > oldMax)
                    oldMax = value;
                if (value < oldMin)
                    oldMin = value;
            }

            oldRange = oldMax - oldMin;

            foreach (var value in img2dArray)
            {
                var scale = (value - oldMin) / oldRange;

                var newValue = (newRange * scale) + newMin;
                response2dArray.Add(newValue);
            }

            var matrixResponse = ArrayConverters.List1DTo2DimMatrix(response2dArray, img.Width, img.Height);

            return ImageUtils.ToImageFromMatrix(matrixResponse);
        }

        public static Bitmap HistogramEqualization(Bitmap img)
        {
            var histogramEqualization = new Accord.Imaging.Filters.HistogramEqualization();
            var responseImg = histogramEqualization.Apply(img);
            return responseImg;
        }

        public static Bitmap MeanNormalization(Bitmap img)
        {
            var meanNormalization = new Accord.Imaging.Filters.Mean();
            var responseImg= meanNormalization.Apply(img);
            return responseImg;
        }

        public static List<double> PCA (Bitmap img)
        {
            var pca = new Accord.Statistics.Analysis.PrincipalComponentAnalysis();
            
            var imgMatrix = ImageUtils.To2DimDoubleArray(img);
            var jaggedMatrix = ArrayConverters.ToJaggedArray(imgMatrix);

            pca.Learn(jaggedMatrix);

            var pcaTransformResult = pca.Transform(jaggedMatrix);
            
            var pcaResult = ArrayConverters.ArrayToList(pca.Means);  //pca.Eigenvalues

            ////Remove first 3 values
            //List<double> Response = new List<double>();
            //int counter = 0;
            //foreach(var item in pcaResult)
            //{
            //    if (counter > 2)
            //        Response.Add(item);
            //    counter++;
            //}

            return pcaResult;
        }

        public static List<double> GaborFeatures(List<GaborFilteredImage> gaborFilteredImagesList)
        {
            List<double> result = new List<double>();

            foreach (var gaborFilteredImage in gaborFilteredImagesList)
            {
                var pcaValuesList = ImageUtils.PCA(gaborFilteredImage.Image);               
                foreach (var value in pcaValuesList)
                    result.Add(value);                
            }

            return result;
        }


    }
}
