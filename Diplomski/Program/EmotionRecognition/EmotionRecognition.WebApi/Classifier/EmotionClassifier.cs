using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmotionRecognition.WebApi.Classifier
{
    public class EmotionClassifier
    {
        private static EmotionClassifier Instance = null;

        private EmotionClassifier() { }

        public static EmotionClassifier GetClassifier()
        {
            if (Instance != null)
                return Instance;
            Instance = new EmotionClassifier();
            return Instance;
        }

    }
}