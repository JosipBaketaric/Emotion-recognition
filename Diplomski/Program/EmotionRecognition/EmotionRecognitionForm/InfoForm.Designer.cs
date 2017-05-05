namespace EmotionRecognitionForm
{
    partial class InfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.chartPrecision = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.chartPrecisionClass = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.chartROCRecallKappa = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartPrecision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPrecisionClass)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartROCRecallKappa)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbInfo
            // 
            this.rtbInfo.Location = new System.Drawing.Point(12, 12);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.ReadOnly = true;
            this.rtbInfo.Size = new System.Drawing.Size(505, 534);
            this.rtbInfo.TabIndex = 0;
            this.rtbInfo.Text = "";
            this.rtbInfo.TextChanged += new System.EventHandler(this.rtbInfo_TextChanged);
            // 
            // chartPrecision
            // 
            chartArea1.Name = "ChartArea1";
            this.chartPrecision.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartPrecision.Legends.Add(legend1);
            this.chartPrecision.Location = new System.Drawing.Point(17, 47);
            this.chartPrecision.Name = "chartPrecision";
            series1.BorderColor = System.Drawing.Color.Blue;
            series1.BorderWidth = 2;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.Olive;
            series1.Legend = "Legend1";
            series1.Name = "Preciznost";
            series2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series2.BorderWidth = 2;
            series2.ChartArea = "ChartArea1";
            series2.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series2.Legend = "Legend1";
            series2.Name = "F-Mjera";
            this.chartPrecision.Series.Add(series1);
            this.chartPrecision.Series.Add(series2);
            this.chartPrecision.Size = new System.Drawing.Size(889, 362);
            this.chartPrecision.TabIndex = 1;
            this.chartPrecision.Text = "Preciznost po prolascima";
            this.chartPrecision.Click += new System.EventHandler(this.chartPrecision_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(216, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(547, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Graf preciznosti i f-mjere po prolascima";
            // 
            // chartPrecisionClass
            // 
            chartArea2.Name = "ChartArea1";
            this.chartPrecisionClass.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartPrecisionClass.Legends.Add(legend2);
            this.chartPrecisionClass.Location = new System.Drawing.Point(18, 883);
            this.chartPrecisionClass.Name = "chartPrecisionClass";
            series3.BorderColor = System.Drawing.Color.Blue;
            series3.BorderWidth = 2;
            series3.ChartArea = "ChartArea1";
            series3.Color = System.Drawing.Color.Olive;
            series3.Legend = "Legend1";
            series3.Name = "Preciznost";
            series4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series4.BorderWidth = 2;
            series4.ChartArea = "ChartArea1";
            series4.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series4.Legend = "Legend1";
            series4.Name = "F-Mjera";
            this.chartPrecisionClass.Series.Add(series3);
            this.chartPrecisionClass.Series.Add(series4);
            this.chartPrecisionClass.Size = new System.Drawing.Size(889, 318);
            this.chartPrecisionClass.TabIndex = 3;
            this.chartPrecisionClass.Text = "Preciznost po klasama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(217, 848);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(546, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Graf preciznosti i f-mjere po emocijama";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.chartROCRecallKappa);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.chartPrecisionClass);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.chartPrecision);
            this.panel1.Location = new System.Drawing.Point(523, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(948, 534);
            this.panel1.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3.Location = new System.Drawing.Point(167, 448);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(644, 32);
            this.label3.TabIndex = 6;
            this.label3.Text = "Graf ROC vrijednosti, odziva i Kappa statistike";
            // 
            // chartROCRecallKappa
            // 
            chartArea3.Name = "ChartArea1";
            this.chartROCRecallKappa.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartROCRecallKappa.Legends.Add(legend3);
            this.chartROCRecallKappa.Location = new System.Drawing.Point(19, 483);
            this.chartROCRecallKappa.Name = "chartROCRecallKappa";
            series5.BorderColor = System.Drawing.Color.DarkViolet;
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "ROC";
            series6.BorderColor = System.Drawing.Color.Purple;
            series6.BorderWidth = 2;
            series6.ChartArea = "ChartArea1";
            series6.Legend = "Legend1";
            series6.Name = "Odziv";
            series7.BorderColor = System.Drawing.Color.Teal;
            series7.BorderWidth = 2;
            series7.ChartArea = "ChartArea1";
            series7.Legend = "Legend1";
            series7.Name = "Kappa";
            this.chartROCRecallKappa.Series.Add(series5);
            this.chartROCRecallKappa.Series.Add(series6);
            this.chartROCRecallKappa.Series.Add(series7);
            this.chartROCRecallKappa.Size = new System.Drawing.Size(889, 350);
            this.chartROCRecallKappa.TabIndex = 5;
            this.chartROCRecallKappa.Text = "Preciznost po prolascima";
            // 
            // InfoForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1483, 567);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rtbInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "InfoForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "P42";
            ((System.ComponentModel.ISupportInitialize)(this.chartPrecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPrecisionClass)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartROCRecallKappa)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPrecision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPrecisionClass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartROCRecallKappa;
    }
}