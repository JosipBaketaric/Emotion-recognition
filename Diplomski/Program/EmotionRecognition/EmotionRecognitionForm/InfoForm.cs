using EmotionRecognition.Common;
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
    public partial class InfoForm : Form
    {
        public InfoForm(ResultTransfer result, string[] Emotions)
        {
            InitializeComponent();


            rtbInfo.AppendText("Preciznost: " + result.Accurancy.ToString() + "\n\n");

            PrintConfusionMatrix(result, Emotions);
            PrintAccurencyDetails(result, Emotions);
        }

        private void PrintConfusionMatrix(ResultTransfer result, string[] Emotions)
        {           
            rtbInfo.AppendText("Matrica pogrešaka\n\n");

            rtbInfo.AppendText("\t");
            for (int i = 0; i < Emotions.Length; i++)
            {
                rtbInfo.AppendText(Emotions[i].Substring(0, 2));
                if (i != 6)
                    rtbInfo.AppendText("\t");
            }

            rtbInfo.AppendText("\n\n");

            for (int i = 0; i < result.ConfusionMatrix.Length; i++)
            {
                rtbInfo.AppendText(Emotions[i].Substring(0, 2));
                rtbInfo.AppendText("\t");

                for (int j = 0; j < result.ConfusionMatrix.Length; j++)
                {
                    rtbInfo.AppendText(result.ConfusionMatrix[i][j].ToString());
                    if (j != 6)
                        rtbInfo.AppendText("\t");
                }
                rtbInfo.AppendText("\n\n");
            }
        }

        private void PrintAccurencyDetails(ResultTransfer result, string[] Emotions)
        {
            rtbInfo.AppendText("\n\n");
            rtbInfo.AppendText("Detalji preciznosti klasifikacije");
            rtbInfo.AppendText("\n\n");

            for (int i = 0; i < Emotions.Length; i++)
            {
                rtbInfo.AppendText(Emotions[i].Substring(0, 2) + "\t");                
                var precision = CalculatePrecision(result, i);
                rtbInfo.AppendText(precision.ToString());

                rtbInfo.AppendText("\n\n");
            }
        }

        private double CalculatePrecision(ResultTransfer result, int x)
        {
            double sumCol = 0;
            double returnResult;

            for (int i = 0; i < result.ConfusionMatrix.Length; i++)
            {
                sumCol += result.ConfusionMatrix[i][x];
            }

            returnResult = (result.ConfusionMatrix[x][x]) / (sumCol);
            return returnResult;
        }

        private void rtbInfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
