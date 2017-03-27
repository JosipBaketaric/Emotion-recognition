namespace EmotionRecognitionForm
{
    partial class Form1
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
            this.btnTest = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnLoadImg = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbClassificatorList = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbIntervalTimes = new System.Windows.Forms.ComboBox();
            this.cbClassifiers = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btnProcess = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPokreni = new System.Windows.Forms.Button();
            this.lblEmocija = new System.Windows.Forms.Label();
            this.btnInfo = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pbResult = new System.Windows.Forms.PictureBox();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnCustomSet = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(1050, 711);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(121, 34);
            this.btnTest.TabIndex = 10;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1212, 711);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(121, 34);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Izlaz";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(737, 702);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 34);
            this.button1.TabIndex = 5;
            this.button1.Text = "Automatski";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btnLoadImg
            // 
            this.btnLoadImg.Location = new System.Drawing.Point(882, 702);
            this.btnLoadImg.Name = "btnLoadImg";
            this.btnLoadImg.Size = new System.Drawing.Size(121, 34);
            this.btnLoadImg.TabIndex = 6;
            this.btnLoadImg.Text = "Učitaj sliku";
            this.btnLoadImg.UseVisualStyleBackColor = true;
            this.btnLoadImg.Click += new System.EventHandler(this.btnLoadImg_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbClassificatorList);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cbIntervalTimes);
            this.groupBox2.Controls.Add(this.cbClassifiers);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.btnLoadImg);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.btnProcess);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.btnPokreni);
            this.groupBox2.Location = new System.Drawing.Point(12, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1032, 748);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(546, 618);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Model klasifikatora:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // cbClassificatorList
            // 
            this.cbClassificatorList.FormattingEnabled = true;
            this.cbClassificatorList.Location = new System.Drawing.Point(549, 649);
            this.cbClassificatorList.Name = "cbClassificatorList";
            this.cbClassificatorList.Size = new System.Drawing.Size(122, 24);
            this.cbClassificatorList.TabIndex = 12;
            this.cbClassificatorList.SelectedIndexChanged += new System.EventHandler(this.cbClassificatorList_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(761, 618);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(148, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Vrijeme okidanja slika:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(344, 618);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Klasifikator za lice:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 618);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Kamera:";
            // 
            // cbIntervalTimes
            // 
            this.cbIntervalTimes.FormattingEnabled = true;
            this.cbIntervalTimes.Location = new System.Drawing.Point(764, 649);
            this.cbIntervalTimes.Name = "cbIntervalTimes";
            this.cbIntervalTimes.Size = new System.Drawing.Size(166, 24);
            this.cbIntervalTimes.TabIndex = 8;
            this.cbIntervalTimes.SelectedIndexChanged += new System.EventHandler(this.cbIntervalTimes_SelectedIndexChanged);
            // 
            // cbClassifiers
            // 
            this.cbClassifiers.FormattingEnabled = true;
            this.cbClassifiers.Location = new System.Drawing.Point(347, 649);
            this.cbClassifiers.Name = "cbClassifiers";
            this.cbClassifiers.Size = new System.Drawing.Size(181, 24);
            this.cbClassifiers.TabIndex = 7;
            this.cbClassifiers.SelectedIndexChanged += new System.EventHandler(this.cbClassifiers_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(85, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(929, 559);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(6, 649);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(280, 24);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnProcess
            // 
            this.btnProcess.Location = new System.Drawing.Point(347, 702);
            this.btnProcess.Name = "btnProcess";
            this.btnProcess.Size = new System.Drawing.Size(328, 34);
            this.btnProcess.TabIndex = 3;
            this.btnProcess.Text = "Prepoznaj";
            this.btnProcess.UseVisualStyleBackColor = true;
            this.btnProcess.Click += new System.EventHandler(this.btnProcess_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(172, 702);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(121, 34);
            this.btnStop.TabIndex = 4;
            this.btnStop.Text = "Zaustavi";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPokreni
            // 
            this.btnPokreni.Location = new System.Drawing.Point(13, 702);
            this.btnPokreni.Name = "btnPokreni";
            this.btnPokreni.Size = new System.Drawing.Size(121, 34);
            this.btnPokreni.TabIndex = 0;
            this.btnPokreni.Text = "Pokreni";
            this.btnPokreni.UseVisualStyleBackColor = true;
            this.btnPokreni.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblEmocija
            // 
            this.lblEmocija.AutoSize = true;
            this.lblEmocija.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblEmocija.Location = new System.Drawing.Point(104, 307);
            this.lblEmocija.Name = "lblEmocija";
            this.lblEmocija.Size = new System.Drawing.Size(81, 25);
            this.lblEmocija.TabIndex = 7;
            this.lblEmocija.Text = "Emocija";
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(88, 564);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(121, 34);
            this.btnInfo.TabIndex = 8;
            this.btnInfo.Text = "Ocijeni";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnInfo);
            this.groupBox1.Controls.Add(this.lblEmocija);
            this.groupBox1.Controls.Add(this.pbResult);
            this.groupBox1.Controls.Add(this.lblResult);
            this.groupBox1.Location = new System.Drawing.Point(1050, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(283, 604);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // pbResult
            // 
            this.pbResult.Location = new System.Drawing.Point(60, 86);
            this.pbResult.Name = "pbResult";
            this.pbResult.Size = new System.Drawing.Size(165, 198);
            this.pbResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbResult.TabIndex = 6;
            this.pbResult.TabStop = false;
            // 
            // lblResult
            // 
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblResult.Location = new System.Drawing.Point(57, 18);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(168, 44);
            this.lblResult.TabIndex = 5;
            this.lblResult.Text = "Rezultat";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblResult.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnCustomSet
            // 
            this.btnCustomSet.Location = new System.Drawing.Point(1050, 627);
            this.btnCustomSet.Name = "btnCustomSet";
            this.btnCustomSet.Size = new System.Drawing.Size(283, 34);
            this.btnCustomSet.TabIndex = 11;
            this.btnCustomSet.Text = "Kreiranje vlastitog seta podataka\r\n";
            this.btnCustomSet.UseVisualStyleBackColor = true;
            this.btnCustomSet.Click += new System.EventHandler(this.btnCustomSet_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1362, 763);
            this.Controls.Add(this.btnCustomSet);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Program za prepoznavanje emocija";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnLoadImg;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btnProcess;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPokreni;
        private System.Windows.Forms.Label lblEmocija;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pbResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ComboBox cbClassifiers;
        private System.Windows.Forms.ComboBox cbIntervalTimes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbClassificatorList;
        private System.Windows.Forms.Button btnCustomSet;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

