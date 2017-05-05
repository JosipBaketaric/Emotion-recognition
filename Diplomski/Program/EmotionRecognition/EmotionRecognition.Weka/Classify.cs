using EmotionRecognition.Common;
using System;
using System.Collections.Generic;
using System.IO;
using weka.core;

namespace EmotionRecognition.Weka
{
    public class Classify
    {
        private weka.classifiers.Classifier Classifier = null;
        private static Classify Instance = null;
        private ClassifierTransfer cf;
        private bool fEvaluated;
        private bool fSet;
        public ResultTransfer rf { get; set; }

        private Classify()
        {
            fEvaluated = false;
            fSet = false;
        }

        public void setClassifier(String location)
        {
            if (File.Exists(location))
            {
                cf = Classifiers.getClassifier(location);
                Classifier = cf.Classifier;
                fSet = true;
            }        
            else
                throw new FileNotFoundException();
        }

        public void evaluateClassifier(String dataLocation)
        {
            if (File.Exists(dataLocation) && fSet == true)
            {
                //Load data
                Instances data = new Instances(new java.io.FileReader(dataLocation));
                data.setClassIndex(data.numAttributes() - 1);

                //Use classifier coppy
                weka.classifiers.Classifier classifierCopy = Classifier;
                //Evaluate
                cf = Classifiers.ClassifierEvaluation(classifierCopy, data);

                //Map data
                rf = new ResultTransfer();
                rf.Accurancy = cf.Accurancy;
                rf.ConfusionMatrix = cf.ConfusionMatrix;
                rf.TimeToTrain = cf.TimeToTrain;
                rf.areaUnderPRC = cf.areaUnderPRC;
                rf.areaUnderROC = cf.areaUnderROC;
                rf.fMeasure = cf.fMeasure;
                rf.foldResultsPrecision = cf.foldResultsPrecision;
                rf.foldResultsWeightedFMeasure = cf.foldResultsWeightedFMeasure;
                rf.foldResultsWeightedPrecision = cf.foldResultsWeightedPrecision;
                rf.precision = cf.precision;
                rf.weightedFMeasure = cf.weightedFMeasure;

                //New
                rf.errorRate = cf.errorRate;
                rf.meanAbsoluteError = cf.meanAbsoluteError;
                rf.rootMeanSquaredError = cf.rootMeanSquaredError;
                rf.kappa = cf.kappa;
                rf.weightedAreaUnderROC = cf.weightedAreaUnderROC;
                rf.weightedRecall = cf.weightedRecall;

                rf.foldKappa = cf.foldKappa;
                rf.foldAreaUnderROC = cf.foldAreaUnderROC;
                rf.foldWeightedRecall = cf.foldWeightedRecall;
                rf.foldMeanAbsoluteError = cf.foldMeanAbsoluteError;
                rf.foldRootMeanSquaredError = cf.foldRootMeanSquaredError;

                fEvaluated = true;
            }
            else
            {
                throw new FileNotFoundException();
            }
                          
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
            //Classifier not set
            if (!fSet)
            {
                return null;
            }

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

            ResultTransfer rt = new ResultTransfer();

            rt.Result = result;

            if (fEvaluated)
            {
                rt.Accurancy = cf.Accurancy;
                rt.ConfusionMatrix = cf.ConfusionMatrix;
                rt.TimeToTrain = cf.TimeToTrain;
                rt.areaUnderPRC = cf.areaUnderPRC;
                rt.areaUnderROC = cf.areaUnderROC;
                rt.fMeasure = cf.fMeasure;
                rt.foldResultsPrecision = cf.foldResultsPrecision;
                rt.foldResultsWeightedFMeasure = cf.foldResultsWeightedFMeasure;
                rt.foldResultsWeightedPrecision = cf.foldResultsWeightedPrecision;
                rt.precision = cf.precision;
                rt.weightedFMeasure = cf.weightedFMeasure;
            }

            return rt;
        }

    }
}
