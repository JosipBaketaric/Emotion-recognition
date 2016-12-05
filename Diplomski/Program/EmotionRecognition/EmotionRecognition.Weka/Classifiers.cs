using System;
using System.Collections.Generic;
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

        public static double RandomForestKFoldEval(string trainingData)
        {
            weka.core.Instances data = new weka.core.Instances(new java.io.FileReader(trainingData));
            data.setClassIndex(data.numAttributes() - 1);

            weka.classifiers.Classifier classifier = new weka.classifiers.trees.RandomForest();

            int folds = 10;
            int runs = 10;
            int seed = 1;

            java.util.Random rand = new java.util.Random(seed);
            var randData = new Instances(data);
            randData.randomize(rand);
            Evaluation evalAll = new Evaluation(randData);
            for (int n = 0; n < folds; n++)
            {
                Evaluation eval = new Evaluation(randData);
                Instances train = randData.trainCV(folds, n);
                Instances test = randData.testCV(folds, n);

                Classifier clsCopy = classifier;
                clsCopy.buildClassifier(train);
                eval.evaluateModel(clsCopy, test);
                evalAll.evaluateModel(clsCopy, test);
            }
            var cm = evalAll.confusionMatrix();

            double result = (double)evalAll.correct() / (double)(evalAll.incorrect() + evalAll.correct());
            return result;
        }




    }



}
