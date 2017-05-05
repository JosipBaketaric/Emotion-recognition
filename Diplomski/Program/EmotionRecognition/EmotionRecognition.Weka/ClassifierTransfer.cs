using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.Weka
{
    public class ClassifierTransfer
    {
        public double[][] ConfusionMatrix { get; set; }
        public double Accurancy { get; set; }
        public double result {get;set;}
        public weka.classifiers.Classifier Classifier { get; set; }
        public double TimeToTrain { get; set; }
        public double weightedFMeasure { get; set; }
        public List<double> fMeasure { get; set; }
        public List<double> areaUnderPRC { get; set; }
        public List<double> areaUnderROC { get; set; }
        public List<double> precision { get; set; }
        public List<double> foldResultsWeightedPrecision { get; set; }
        public List<double> foldResultsPrecision { get; set; }
        public List<double> foldResultsWeightedFMeasure { get; set; }

        //New
        public double errorRate { get; set; }
        public double meanAbsoluteError { get; set; }
        public double rootMeanSquaredError { get; set; }
        public double kappa { get; set; }
        public double weightedAreaUnderROC { get; set; }
        public double weightedRecall { get; set; }


        public List<double> foldKappa { get; set; }
        public List<double> foldAreaUnderROC { get; set; }
        public List<double> foldWeightedRecall { get; set; }
        public List<double> foldMeanAbsoluteError { get; set; }
        public List<double> foldRootMeanSquaredError { get; set; }
    }
}
