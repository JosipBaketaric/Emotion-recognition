using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weka.core;

namespace EmotionRecognition.Weka
{
    public class Classify
    {
        private weka.classifiers.Classifier RandomForest = null;
        private static Classify Instance = null;

        private Classify()
        {
            RandomForest = Classifiers.RandomForest(@"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\Data\TrainingFeatures\FeaturesArff.arff");
        }

        public static Classify GetInstance()
        {
            if (Instance != null)
                return Instance;
            Instance = new Classify();
            return Instance;
        }

        public double GetClass(List<double> data)
        {
            //Instances dataUnlabeled = new Instances("TestInstances", atts, 0);
            //dataUnlabeled.add(newInst);
            //dataUnlabeled.setClassIndex(dataUnlabeled.numAttributes() - 1);
            //double classif = ibk.classifyInstance(dataUnlabeled.firstInstance());


            Instance instance = new DenseInstance(data.Count);          

            for (int i = 0; i < data.Count; i++)
            {
                instance.setValue(i, data[i]);
            }

            var result = RandomForest.classifyInstance(instance);

            return result;
        }

    }
}
