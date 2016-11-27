using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord;

namespace EmotionRecognition.Service
{
    public class GaborFeatures
    {
        private Bitmap Image { get; set; }

        public void ApplyFilter()
        {
            var gaborFilter = new Accord.Imaging.Filters.GaborFilter();
            gaborFilter.Apply(Image);

        }
    }
}
