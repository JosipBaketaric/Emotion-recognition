using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weka.classifiers;
using weka.classifiers.meta;
using weka.classifiers.trees;

namespace EmotionRecognition.Weka
{
    public static class Classifiers
    {

        public static Classifier RandomForest(string trainingData)
        {
            try
            {
                int percentSplit = 66;
                weka.core.Instances insts = new weka.core.Instances(new java.io.FileReader(trainingData));

                insts.setClassIndex(insts.numAttributes() - 1);

                weka.classifiers.Classifier cl = new weka.classifiers.trees.RandomForest();

                //randomize the order of the instances in the dataset. This is for testing
                weka.filters.Filter myRandom = new weka.filters.unsupervised.instance.Randomize();
                myRandom.setInputFormat(insts);
                insts = weka.filters.Filter.useFilter(insts, myRandom);

                int trainSize = insts.numInstances() * percentSplit / 100;
                int testSize = insts.numInstances() - trainSize;
                weka.core.Instances train = new weka.core.Instances(insts, 0, trainSize);

                cl.buildClassifier(train);
                int numCorrect = 0;
                for (int i = trainSize; i < insts.numInstances(); i++)
                {
                    weka.core.Instance currentInst = insts.instance(i);
                    double predictedClass = cl.classifyInstance(currentInst);
                    if (predictedClass == insts.instance(i).classValue())
                        numCorrect++;
                }

                double percentage = (double)numCorrect / (double)testSize;

                return cl;
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public static Classifier AdaBoostJ48(string trainingData)
        {
            try
            {
                int percentSplit = 75;
                weka.core.Instances insts = new weka.core.Instances(new java.io.FileReader(trainingData));
                insts.setClassIndex(insts.numAttributes() - 1);

                //Classifier
                weka.classifiers.meta.AdaBoostM1 cl = new weka.classifiers.meta.AdaBoostM1();
                var optionString = "-P 100 -S 1 -I 10 -W weka.classifiers.trees.RandomForest";
                var optionsSplit = optionString.Split(' ');
                cl.setOptions(optionsSplit);
                cl.setWeightThreshold(90);  //To speed up               
                cl.buildClassifier(insts);

                //randomize the order of the instances in the dataset.  this is for testing
                weka.filters.Filter myRandom = new weka.filters.unsupervised.instance.Randomize();
                myRandom.setInputFormat(insts);
                insts = weka.filters.Filter.useFilter(insts, myRandom);

                int trainSize = insts.numInstances() * percentSplit / 100;
                int testSize = insts.numInstances() - trainSize;
                weka.core.Instances train = new weka.core.Instances(insts, 0, trainSize);

                int numCorrect = 0;
                for (int i = trainSize; i < insts.numInstances(); i++)
                {
                    weka.core.Instance currentInst = insts.instance(i);
                    double predictedClass = cl.classifyInstance(currentInst);
                    if (predictedClass == insts.instance(i).classValue())
                        numCorrect++;
                }

                double percentage = (double)numCorrect / (double)testSize;
                return cl;
            }
           catch(Exception e)
            {
                throw e;
            }

        }


    }



}
