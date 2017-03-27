using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weka.classifiers;
using weka.classifiers.meta;
using weka.classifiers.trees;
using weka.core;

namespace EmotionRecognition.Weka
{
    public static class Classifiers
    {

        public static Classifier RandomForest(string trainingData)
        {
            try
            {
                Instances data = new Instances(new java.io.FileReader(trainingData));
                data.setClassIndex(data.numAttributes() - 1);

                Classifier classifier = new RandomForest();
                classifier.buildClassifier(data);

                return classifier;
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public static ClassifierTransfer RandomForestKFoldEval(string trainingData)
        {
            weka.core.Instances data = new weka.core.Instances(new java.io.FileReader(trainingData));
            data.setClassIndex(data.numAttributes() - 1);

            weka.classifiers.Classifier classifier = new weka.classifiers.trees.RandomForest();

            //Evaluate
            ClassifierTransfer cf = ClassifierEvaluation(classifier, data);
            return cf;
        }


        public static ClassifierTransfer SVMKFoldEval(string trainingData)
        {
            weka.core.Instances data = new weka.core.Instances(new java.io.FileReader(trainingData));
            data.setClassIndex(data.numAttributes() - 1);

            weka.classifiers.functions.SMO classifier = new weka.classifiers.functions.SMO();
            classifier.setC(0.095);

            //Evaluate
            ClassifierTransfer cf = ClassifierEvaluation(classifier, data);
            return cf;
        }

        public static void SVMBuildAndSave(string trainingData, string location)
        {
            weka.core.Instances data = new weka.core.Instances(new java.io.FileReader(trainingData));
            data.setClassIndex(data.numAttributes() - 1);

            weka.classifiers.functions.SMO classifier = new weka.classifiers.functions.SMO();
            classifier.setC(0.095);

            ClassifierTransfer cf = ClassifierEvaluation(classifier, data);
            //save
            weka.core.SerializationHelper.write(location, cf.Classifier);
        }


        //Load trained model from file
        public static ClassifierTransfer getClassifier(String location)
        {
            Classifier cls = (Classifier)weka.core.SerializationHelper.read(location);

            ClassifierTransfer cf = new ClassifierTransfer();
            cf.Classifier = cls;

            return cf;
        }


        public static ClassifierTransfer ClassifierEvaluation (Classifier classifier, weka.core.Instances data)
        {
            Stopwatch stopWatch = new Stopwatch();

            data.setClassIndex(data.numAttributes() - 1);
            int folds = 10;
            int seed = 1;

            //check num of instances
            if (data.numInstances() <= 10 && data.numInstances() > 1)
                folds = data.numInstances();
            else if(data.numInstances() < 1)
            {
                return null;
            }

            // randomize data
            java.util.Random rand = new java.util.Random(seed);
            Instances randData = new Instances(data);
            randData.randomize(rand);
            if (randData.classAttribute().isNominal() && folds > 1)
                randData.stratify(folds);

            List<Evaluation> evaluationList = new List<Evaluation>();
            Evaluation eval = new Evaluation(randData);

            stopWatch.Start();
            // perform cross-validation
            for (int n = 0; n < folds; n++)
            {
                Instances train = randData.trainCV(folds, n);
                Instances test = randData.testCV(folds, n);

                Classifier clsCopy = classifier;
                clsCopy.buildClassifier(train);

                Evaluation evalCurrent = new Evaluation(randData);

                eval.evaluateModel(clsCopy, test);
                evaluationList.Add(evalCurrent);
            }
            stopWatch.Stop();

            // metrics
            var cm = eval.confusionMatrix();

            List<double> fMeasure = new List<double>();
            List<double> areaUnderPRC = new List<double>();
            List<double> areaUnderROC = new List<double>();
            List<double> precision = new List<double>();
            for (int i = 0; i < data.numClasses(); i++)
            {
                fMeasure.Add(eval.fMeasure(i));
                areaUnderPRC.Add(eval.areaUnderPRC(i));
                areaUnderROC.Add(eval.areaUnderROC(i));
                precision.Add(eval.precision(i));
            }

            double weightedPrecision = eval.weightedPrecision();
            double result = (double)eval.correct() / (double)(eval.incorrect() + eval.correct());
            double timeToTrain = (double)((double)stopWatch.ElapsedMilliseconds / (double)1000 / (double)60);

            ClassifierTransfer cf = new ClassifierTransfer();
            cf.Classifier = classifier;
            cf.Accurancy = result;
            cf.TimeToTrain = timeToTrain;
            cf.areaUnderPRC = areaUnderPRC;
            cf.areaUnderROC = areaUnderROC;
            cf.ConfusionMatrix = cm;
            cf.fMeasure = fMeasure;
            cf.precision = precision;

            return cf;
        }

    }



}
