using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.Service.Utils
{
    public static class ArrayConverters
    {

        public static T[][] ToJaggedArray<T>(this T[,] twoDimensionalArray)
        {
            int rowsFirstIndex = twoDimensionalArray.GetLowerBound(0);
            int rowsLastIndex = twoDimensionalArray.GetUpperBound(0);
            int numberOfRows = rowsLastIndex + 1;

            int columnsFirstIndex = twoDimensionalArray.GetLowerBound(1);
            int columnsLastIndex = twoDimensionalArray.GetUpperBound(1);
            int numberOfColumns = columnsLastIndex + 1;

            T[][] jaggedArray = new T[numberOfRows][];
            for (int i = rowsFirstIndex; i <= rowsLastIndex; i++)
            {
                jaggedArray[i] = new T[numberOfColumns];

                for (int j = columnsFirstIndex; j <= columnsLastIndex; j++)
                {
                    jaggedArray[i][j] = twoDimensionalArray[i, j];
                }
            }
            return jaggedArray;
        }


        public static List<double> ArrayListToDoubleList(List<double[]> matrixList)
        {
            List<double> doubleList = new List<double>();
            foreach(var array in matrixList)
            {
                foreach(var item in array)
                {
                    doubleList.Add(item);
                }
            }
            return doubleList;
        }

        public static double[,] List1DTo2DimMatrix(List<double> list1d, int width, int height)
        {
            double[,] responseMatrix = new double[width, height];

            for(int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    responseMatrix[i, j] = list1d.ElementAt(i + j);
                }
            }

            return responseMatrix;
        }

        public static List<double> ArrayToList(double[] array)
        {
            List<double> responseList = new List<double>();
            foreach(var item in array)
            {
                responseList.Add(item);
            }
            return responseList;
        }

    }
}
