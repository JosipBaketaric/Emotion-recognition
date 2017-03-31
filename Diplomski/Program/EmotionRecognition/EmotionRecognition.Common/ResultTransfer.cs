﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.Common
{
    public class ResultTransfer
    {
        public double[][] ConfusionMatrix { get; set; }
        public double Accurancy { get; set; }
        public double Result { get; set; }
        public double weightedFMeasure { get; set; }
        public double TimeToTrain { get; set; }
        public List<double> fMeasure { get; set; }
        public List<double> areaUnderPRC { get; set; }
        public List<double> areaUnderROC { get; set; }
        public List<double> precision { get; set; }
        public List<double> foldResultsWeightedPrecision { get; set; }
        public List<double> foldResultsPrecision { get; set; }
        public List<double> foldResultsWeightedFMeasure { get; set; }
    }
}
