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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.rtbInfo = new System.Windows.Forms.RichTextBox();
            this.chartPrecision = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.chartPrecisionClass = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.chartPrecision)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPrecisionClass)).BeginInit();
            this.SuspendLayout();
            // 
            // rtbInfo
            // 
            this.rtbInfo.Location = new System.Drawing.Point(12, 12);
            this.rtbInfo.Name = "rtbInfo";
            this.rtbInfo.ReadOnly = true;
            this.rtbInfo.Size = new System.Drawing.Size(505, 620);
            this.rtbInfo.TabIndex = 0;
            this.rtbInfo.Text = "";
            this.rtbInfo.TextChanged += new System.EventHandler(this.rtbInfo_TextChanged);
            // 
            // chartPrecision
            // 
            chartArea3.Name = "ChartArea1";
            this.chartPrecision.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.chartPrecision.Legends.Add(legend3);
            this.chartPrecision.Location = new System.Drawing.Point(554, 47);
            this.chartPrecision.Name = "chartPrecision";
            series5.BorderColor = System.Drawing.Color.Blue;
            series5.BorderWidth = 2;
            series5.ChartArea = "ChartArea1";
            series5.Color = System.Drawing.Color.Olive;
            series5.Legend = "Legend1";
            series5.Name = "Preciznost";
            series6.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series6.BorderWidth = 2;
            series6.ChartArea = "ChartArea1";
            series6.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series6.Legend = "Legend1";
            series6.Name = "F-Mjera";
            this.chartPrecision.Series.Add(series5);
            this.chartPrecision.Series.Add(series6);
            this.chartPrecision.Size = new System.Drawing.Size(846, 267);
            this.chartPrecision.TabIndex = 1;
            this.chartPrecision.Text = "Preciznost po prolascima";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(725, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(547, 32);
            this.label1.TabIndex = 2;
            this.label1.Text = "Graf preciznosti i f-mjere po prolascima";
            // 
            // chartPrecisionClass
            // 
            chartArea4.Name = "ChartArea1";
            this.chartPrecisionClass.ChartAreas.Add(chartArea4);
            legend4.Name = "Legend1";
            this.chartPrecisionClass.Legends.Add(legend4);
            this.chartPrecisionClass.Location = new System.Drawing.Point(554, 375);
            this.chartPrecisionClass.Name = "chartPrecisionClass";
            series7.BorderColor = System.Drawing.Color.Blue;
            series7.BorderWidth = 2;
            series7.ChartArea = "ChartArea1";
            series7.Color = System.Drawing.Color.Olive;
            series7.Legend = "Legend1";
            series7.Name = "Preciznost";
            series8.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            series8.BorderWidth = 2;
            series8.ChartArea = "ChartArea1";
            series8.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            series8.Legend = "Legend1";
            series8.Name = "F-Mjera";
            this.chartPrecisionClass.Series.Add(series7);
            this.chartPrecisionClass.Series.Add(series8);
            this.chartPrecisionClass.Size = new System.Drawing.Size(846, 267);
            this.chartPrecisionClass.TabIndex = 3;
            this.chartPrecisionClass.Text = "Preciznost po klasama";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(725, 340);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(546, 32);
            this.label2.TabIndex = 4;
            this.label2.Text = "Graf preciznosti i f-mjere po emocijama";
            // 
            // InfoForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1422, 654);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chartPrecisionClass);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chartPrecision);
            this.Controls.Add(this.rtbInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "InfoForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "InfoForm";
            ((System.ComponentModel.ISupportInitialize)(this.chartPrecision)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPrecisionClass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbInfo;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPrecision;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPrecisionClass;
        private System.Windows.Forms.Label label2;
    }
}