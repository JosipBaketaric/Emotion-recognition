using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmotionRecognition.Common
{
    public class GaborFilteredImage
    {
        public double Orientation { get; set; }
        public double Wavelength { get; set; }
        public Bitmap Image { get; set; }
    }
}
