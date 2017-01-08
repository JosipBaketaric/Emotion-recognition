using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmotionRecognitionForm
{
    public partial class TestForm : Form
    {
        public TestForm(double[,] resultMatrix)
        {
            InitializeComponent();
            PrintConfusionMatrix(resultMatrix);

        }

        private void PrintConfusionMatrix(double[,] resultMatrix)
        {
            rtbTest.AppendText("Matrica pogrešaka\n\n");
            for (int i = 0; i < resultMatrix.Length / 7; i++)
            {
                for (int j = 0; j < resultMatrix.Length / 7; j++)
                {
                    rtbTest.AppendText(resultMatrix[i, j].ToString() + "\t");
                }
                rtbTest.AppendText("\n\n");
            }
        }

    }
}
