using System;
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
        public double TimeToTrain { get; set; }
    }
}
