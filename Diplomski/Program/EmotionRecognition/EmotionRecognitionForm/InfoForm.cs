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

            LoadChart(result, Emotions);

            rtbInfo.AppendText("Vrijeme potrebno za treniranje: " + result.TimeToTrain.ToString() + "\n\n");

            rtbInfo.AppendText("Preciznost: " + result.Accurancy.ToString() + "\n\n");

            PrintConfusionMatrix(result, Emotions);
            PrintAccurencyDetails(result, Emotions);

            printInfo(result, Emotions);
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

        private void printInfo(ResultTransfer rf, string[] Emotions)
        {
            rtbInfo.AppendText("\n\n\n");
            rtbInfo.AppendText("--------------------DETALJI--------------------");
            rtbInfo.AppendText("\n\n");

            //AVERAGE
            rtbInfo.AppendText("----------PROSJEČNO----------");
            rtbInfo.AppendText("\n");

            rtbInfo.AppendText("F-mjera: " + rf.weightedFMeasure);
            rtbInfo.AppendText("\n");

            rtbInfo.AppendText("Preciznost: " + rf.Accurancy);
            rtbInfo.AppendText("\n");

            rtbInfo.AppendText("Stopa pogrešaka: " + rf.errorRate);
            rtbInfo.AppendText("\n");

            rtbInfo.AppendText("Kappa: " + rf.kappa);
            rtbInfo.AppendText("\n");

            rtbInfo.AppendText("Srednja apsolutna pogreška: " + rf.meanAbsoluteError);
            rtbInfo.AppendText("\n");

            rtbInfo.AppendText("Korijen iz srednje apsolutne pogreške: " + rf.rootMeanSquaredError);
            rtbInfo.AppendText("\n");

            rtbInfo.AppendText("Površina ispod ROC: " + rf.weightedAreaUnderROC);
            rtbInfo.AppendText("\n");

            rtbInfo.AppendText("Odziv: " + rf.weightedRecall);
            rtbInfo.AppendText("\n");

            rtbInfo.AppendText("\n\n");

            //FOLDS
            rtbInfo.AppendText("----------PO PROLASCIMA----------");
            rtbInfo.AppendText("\n\n");

            if ( (rf.foldResultsPrecision.Count == rf.foldResultsWeightedFMeasure.Count) &&
                (rf.foldResultsWeightedPrecision.Count == rf.foldResultsPrecision.Count) )
            {
                for (int i = 0; i < rf.foldResultsPrecision.Count; i++)
                {
                    rtbInfo.AppendText("------PROLAZAK " + (i + 1) + "------");
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("F-mjera: " + rf.foldResultsWeightedFMeasure.ElementAt(i));
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("Preciznost: " + rf.foldResultsPrecision.ElementAt(i));
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("Površina ispod ROC: " + rf.foldAreaUnderROC.ElementAt(i));
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("Kappa: " + rf.foldKappa.ElementAt(i));
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("Srednja apsolutna pogreška: " + rf.foldMeanAbsoluteError.ElementAt(i));
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("Korijen iz srednje apsolutne pogreške: " + rf.foldRootMeanSquaredError.ElementAt(i));
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("Odziv: " + rf.foldWeightedRecall.ElementAt(i));
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("\n");
                }
                rtbInfo.AppendText("\n\n");
            }

            else
            {
                rtbInfo.AppendText("COUNT ERROR!");
            }

            //FOLDS
            rtbInfo.AppendText("----------PO KLASAMA----------");
            rtbInfo.AppendText("\n\n");

            if ((rf.fMeasure.Count == rf.precision.Count) &&
                (rf.areaUnderPRC.Count == rf.areaUnderROC.Count) &&
                (rf.fMeasure.Count == rf.areaUnderPRC.Count) )
            {
                for(int i = 0; i < rf.fMeasure.Count; i++)
                {
                    rtbInfo.AppendText("------KLASA " + (i + 1) + ": " + Emotions[i] + "------");
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("F-mjera: " + rf.fMeasure.ElementAt(i));
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("Preciznost: " + rf.precision.ElementAt(i));
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("Područje ispod PRC: " + rf.areaUnderPRC.ElementAt(i));
                    rtbInfo.AppendText("\n");

                    rtbInfo.AppendText("Područje ispod ROC: " + rf.areaUnderROC.ElementAt(i));
                    rtbInfo.AppendText("\n\n");
                }
                rtbInfo.AppendText("\n\n");
            }
            else
            {
                rtbInfo.AppendText("COUNT ERROR!");
            }
        }


        private void LoadChart(ResultTransfer rf, string[] Emotions)
        {
            chartPrecision.ChartAreas[0].AxisX.Title = "Prolazak";
            chartPrecision.ChartAreas[0].AxisY.Title = "Vrijednost";
            chartPrecision.ChartAreas[0].AxisY.Maximum = 1;

            for (int i = 0; i < rf.foldResultsPrecision.Count; i++)
            {
                chartPrecision.Series["Preciznost"].Points.AddXY(i+1, rf.foldResultsWeightedPrecision.ElementAt(i) );
                chartPrecision.Series["F-Mjera"].Points.AddXY(i + 1, rf.foldResultsWeightedFMeasure.ElementAt(i) );                              
            }


            chartROCRecallKappa.ChartAreas[0].AxisX.Title = "Prolazak";
            chartROCRecallKappa.ChartAreas[0].AxisY.Title = "Vrijednost";
            chartROCRecallKappa.ChartAreas[0].AxisY.Maximum = 1;

            for (int i = 0; i < rf.foldAreaUnderROC.Count; i++)
            {
                chartROCRecallKappa.Series["ROC"].Points.AddXY(i + 1, rf.foldAreaUnderROC.ElementAt(i));
                chartROCRecallKappa.Series["Odziv"].Points.AddXY(i + 1, rf.foldWeightedRecall.ElementAt(i));
                chartROCRecallKappa.Series["Kappa"].Points.AddXY(i + 1, rf.foldKappa.ElementAt(i));
            }

            chartPrecisionClass.ChartAreas[0].AxisX.Title = "Emocija";
            chartPrecisionClass.ChartAreas[0].AxisY.Title = "Vrijednost";
            chartPrecisionClass.ChartAreas[0].AxisY.Maximum = 1;

            //
            for (int i = 0; i < 7; i++)
            {
                chartPrecisionClass.Series["Preciznost"].Points.AddXY(Emotions[i], rf.precision.ElementAt(i));
                chartPrecisionClass.Series["F-Mjera"].Points.AddXY(Emotions[i], rf.fMeasure.ElementAt(i));
            }
           

        }




        private void rtbInfo_TextChanged(object sender, EventArgs e)
        {

        }

        private void chartPrecision_Click(object sender, EventArgs e)
        {

        }
    }
}
