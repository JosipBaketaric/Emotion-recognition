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
        public weka.classifiers.Classifier Classifier { get; set; }
        public double TimeToTrain { get; set; }
    }
}
