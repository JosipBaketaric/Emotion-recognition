using EmotionRecognition.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weka.core;

namespace EmotionRecognition.Weka
{
    public class Classify
    {
        private weka.classifiers.Classifier Classifier = null;
        private static Classify Instance = null;
        private ClassifierTransfer cf;

        private Classify()
        {
            string featurePathDebug = Path.Combine(System.Environment.CurrentDirectory, @"..\..\..\..\Data\TrainingFeatures\FeaturesArff.arff");
            string featurePathFinal = Path.Combine(System.Environment.CurrentDirectory, @"\Data\TrainingFeatures\FeaturesArff.arff");

            featurePathDebug = Path.GetFullPath(featurePathDebug);
            featurePathFinal = Path.GetFullPath(featurePathFinal);

            if(File.Exists(featurePathDebug))
            {
                cf = Classifiers.SVMKFoldEval(featurePathDebug);
            }
            else if (File.Exists(System.Environment.CurrentDirectory + "\\Data\\TrainingFeatures\\FeaturesArff.arff"))
            {
                cf = Classifiers.SVMKFoldEval(System.Environment.CurrentDirectory + "\\Data\\TrainingFeatures\\FeaturesArff.arff");
            }
            else
            {
                throw new FileNotFoundException();
            }



            Classifier = cf.Classifier;
        }

        public static Classify GetInstance()
        {
            if (Instance != null)
                return Instance;
            Instance = new Classify();
            return Instance;
        }

        public ResultTransfer GetClass(List<double> data)
        {
            //Create attributes
            List<weka.core.Attribute> attributes = new List<weka.core.Attribute>();
            for(int i = 0; i < data.Count; i++)
            {
                weka.core.Attribute temp = new weka.core.Attribute(i.ToString());
                attributes.Add(temp);
            }
            //Add classes {AF,AN,DI,HA,NE,SA,SU}
            FastVector fvClassVal = new FastVector(7);
            fvClassVal.addElement("AF");
            fvClassVal.addElement("AN");
            fvClassVal.addElement("DI");
            fvClassVal.addElement("HA");
            fvClassVal.addElement("NE");
            fvClassVal.addElement("SA");
            fvClassVal.addElement("SU");
            weka.core.Attribute Class = new weka.core.Attribute("ScheduledFirst", fvClassVal);

            // Declare the feature vector
            FastVector fvWekaAttributes = new FastVector(data.Count + 1);
            // Add attributes 
            foreach(var attribute in attributes)
                fvWekaAttributes.addElement(attribute);
            fvWekaAttributes.addElement(Class);

            Instances dataset = new Instances("whatever", fvWekaAttributes, 0);

            double[] attValues = new double[data.Count];

            for(int i = 0; i < data.Count; i++)
            {
                attValues[i] = data[i];
            }

            //Create the new instance i1
            Instance i1 = new weka.core.DenseInstance(1.0, attValues);
            //Add the instance to the dataset (Instances) (first element 0)        
            dataset.add(i1);
            //Define class attribute position
            dataset.setClassIndex(dataset.numAttributes() - 1);

            var result = Classifier.classifyInstance(dataset.instance(0));

            ResultTransfer rt = new ResultTransfer() { Result = result, Accurancy = cf.Accurancy, ConfusionMatrix = cf.ConfusionMatrix, TimeToTrain = cf.TimeToTrain };

            return rt;
        }

    }
}
