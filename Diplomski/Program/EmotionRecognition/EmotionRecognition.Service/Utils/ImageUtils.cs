using Accord;
using Emgu.CV;
using Emgu.CV.Structure;
using EmotionRecognition.Common;
using EmotionRecognition.Service.Utils;
using OpenCV.Net;
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
            try
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
            catch(Exception e)
            {
                throw e;
            }
           
        }

        public static Bitmap Resize(Bitmap img, int width, int height)
        {
            try
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
            catch(Exception e)
            {
                throw e;
            }
        }

        public static Bitmap ToGrayScale(Bitmap img)
        {
            try
            {
                Image<Bgr, byte> imageFrame = new Image<Bgr, byte>(img);
                //To gray scale
                Image<Gray, byte> grayScaleImage = imageFrame.Convert<Gray, byte>();

                return grayScaleImage.Bitmap;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static double[,] To2DimDoubleArray(Bitmap img)
        {
            try
            {
                var converter = new Accord.Imaging.Converters.ImageToMatrix();
                double[,] matrix;
                converter.Convert(img, out matrix);
                return matrix;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static double[] To1DimDoubleArray(Bitmap img)
        {
            try
            {
                var converter = new Accord.Imaging.Converters.ImageToArray();
                double[] array;
                converter.Convert(img, out array);
                return array;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static Bitmap ToImageFromMatrix(double[,] matrix)
        {
            try
            {
                var converter = new Accord.Imaging.Converters.MatrixToImage();
                Bitmap responseImage;
                converter.Convert(matrix, out responseImage);
                return responseImage;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public static Bitmap ConvertTo24bpp(Image img)
        {
            try
            {
                var bmp = new Bitmap(img.Width, img.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                using (var gr = Graphics.FromImage(bmp))
                    gr.DrawImage(img, new Rectangle(0, 0, img.Width, img.Height));
                return bmp;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Bitmap RemoveBackground(Bitmap Image)
        {
            double threshold = 55;

            Image<Bgr, Byte> img = new Image<Bgr, byte>(Image);
            Image<Gray, Byte> gray = img.Convert<Gray, Byte>();

            gray = gray.ThresholdBinary(new Gray(threshold), new Gray(255));

            Image<Bgr, Byte> newimg = img.Copy(gray);

            return newimg.ToBitmap();
        }

        public static Bitmap ApplyTreshold(Bitmap img)
        {
            Color color = Color.Black;

            for(int i = 0; i < img.Width; i++)
            {
                for(int j = 0; j < img.Height; j++)
                {
                    if(img.GetPixel(i,j).B < 20)
                        img.SetPixel(i, j, color);
                }
            }
            return img;
        }
       

        public static List<double> PCA (Bitmap img)
        {
            try
            {
                var pca = new Accord.Statistics.Analysis.PrincipalComponentAnalysis();

                var imgMatrix = ImageUtils.To2DimDoubleArray(img);
                var jaggedMatrix = ArrayConverters.ToJaggedArray(imgMatrix);

                pca.Learn(jaggedMatrix);

                var pcaTransformResult = pca.Transform(jaggedMatrix);

                var pcaResult = ArrayConverters.ArrayToList(pca.Means);  //pca.Eigenvalues

                //Remove first two values = slightly better results
                pcaResult.RemoveAt(0);
                pcaResult.RemoveAt(0);

                //Remove last two
                pcaResult.Remove(pcaResult.Count - 1);
                pcaResult.Remove(pcaResult.Count - 1);

                return pcaResult;
            }
           catch(Exception e)
            {
                throw e;
            }
        }

        public static List<double> GaborFeatures(List<GaborFilteredImage> gaborFilteredImagesList)
        {
            try
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
            catch (Exception e)
            {
                throw e;
            }
        }


        public static Bitmap treshold(Bitmap image)
        {
            var bgr = new Emgu.CV.Structure.Bgr();
            var bgr2 = new Emgu.CV.Structure.Bgr();

            bgr.Blue = 255;
            bgr.Green = 255;
            bgr.Red = 255;

            bgr2.Blue = 2;
            bgr2.Green = 2;
            bgr2.Red = 2;

            Image<Bgr, byte> img = new Image<Bgr, byte>(image);
            var adaptive = img.ThresholdAdaptive(bgr, Emgu.CV.CvEnum.AdaptiveThresholdType.GaussianC,
                Emgu.CV.CvEnum.ThresholdType.Binary, 11, bgr2);

            Bitmap adaptiveBitmap = adaptive.ToBitmap();

            return adaptiveBitmap;
        }



    }
}
