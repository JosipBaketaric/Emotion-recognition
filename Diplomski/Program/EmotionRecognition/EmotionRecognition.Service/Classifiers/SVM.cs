using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Math.Optimization.Losses;
using Accord.Statistics.Kernels;
using EmotionRecognition.Service.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.Service.Classifiers
{
    public static class SVM
    {
        public static void test()
        {
            string[] lines = System.IO.File.ReadAllLines(@"E:\Features\Features.txt");
            List<double[]> listOfInputs = new List<double[]>();
            List<string> outputsList = new List<string>();

            bool firstRun = true;
            foreach(var line in lines)
            {
                if (!firstRun)
                {
                    var splitString = line.Split('\t');
                    double[] tempArray = new double[splitString.Length - 1];
                    int tempCounter = 0;

                    foreach (var value in splitString)
                    {
                        if (tempCounter == (splitString.Length - 1))
                        {
                            outputsList.Add(value);
                        }
                        else
                        {
                            double temp;
                            double.TryParse(value, out temp);
                            tempArray[tempCounter] = temp;
                            tempCounter++;
                        }
                    }
                    listOfInputs.Add(tempArray);
                }
                firstRun = false;
            }

            string[] outputs = new string[outputsList.Count];
            int tempCounter2 = 0;
            foreach (var item in outputsList)
            {
                outputs[tempCounter2] = item;
                tempCounter2++;
            }

            double[,] inputMatrix = new double[listOfInputs.Count, listOfInputs[0].Length];

            for(int i = 0; i < listOfInputs.Count; i++)
            {
                for(int j = 0; j < listOfInputs[0].Length; j++)
                {
                    inputMatrix[i, j] = listOfInputs[i][j];
                }
            }

            var inputs = ArrayConverters.ToJaggedArray(inputMatrix);

            var teacher = new MulticlassSupportVectorLearning<Linear>()
            {
                // using LIBLINEAR's L2-loss SVC dual for each SVM
                Learner = (p) => new LinearDualCoordinateDescent()
                {
                    Loss = Loss.L2
                }
            };

            // Configure parallel execution options
            teacher.ParallelOptions.MaxDegreeOfParallelism = 1;

            int[] outputsInt = new int[outputs.Length];
            for (int i = 0; i < outputs.Length; i++)
            {
                if (outputs[i].Equals("AF"))
                    outputsInt[i] = 1;
                if (outputs[i].Equals("AN"))
                    outputsInt[i] = 2;
                if (outputs[i].Equals("DI"))
                    outputsInt[i] = 3;
                if (outputs[i].Equals("HA"))
                    outputsInt[i] = 4;
                if (outputs[i].Equals("NE"))
                    outputsInt[i] = 5;
                if (outputs[i].Equals("SA"))
                    outputsInt[i] = 6;
                if (outputs[i].Equals("SU"))
                    outputsInt[i] = 7;
            }

            // Learn a machine
            var machine = teacher.Learn(inputs, outputsInt);

            // Obtain class predictions for each sample
            int[] predicted = machine.Decide(inputs);

            // Compute classification error
            double error = new ZeroOneLoss(outputsInt).Loss(predicted);

        }



    }
}
