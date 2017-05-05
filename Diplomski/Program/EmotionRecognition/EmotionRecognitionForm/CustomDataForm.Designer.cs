namespace EmotionRecognitionForm
{
    partial class CustomDataForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.lblLocation = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.gbNameFormat = new System.Windows.Forms.GroupBox();
            this.rbFormat2 = new System.Windows.Forms.RadioButton();
            this.rbFormat1 = new System.Windows.Forms.RadioButton();
            this.numLength = new System.Windows.Forms.NumericUpDown();
            this.lblLength = new System.Windows.Forms.Label();
            this.tbStrah = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbSrdzba = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbGadenje = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbRadost = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbNeutralno = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbTuga = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbIznenadenje = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.gbNameFormat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 33);
            this.button1.TabIndex = 0;
            this.button1.Text = "Lokacija";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Location = new System.Drawing.Point(115, 37);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(13, 17);
            this.lblLocation.TabIndex = 1;
            this.lblLocation.Text = "-";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(20, 352);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(204, 43);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Pokreni";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // gbNameFormat
            // 
            this.gbNameFormat.Controls.Add(this.rbFormat2);
            this.gbNameFormat.Controls.Add(this.rbFormat1);
            this.gbNameFormat.Location = new System.Drawing.Point(13, 92);
            this.gbNameFormat.Name = "gbNameFormat";
            this.gbNameFormat.Size = new System.Drawing.Size(134, 109);
            this.gbNameFormat.TabIndex = 3;
            this.gbNameFormat.TabStop = false;
            this.gbNameFormat.Text = "Format imena";
            // 
            // rbFormat2
            // 
            this.rbFormat2.AutoSize = true;
            this.rbFormat2.Location = new System.Drawing.Point(7, 72);
            this.rbFormat2.Name = "rbFormat2";
            this.rbFormat2.Size = new System.Drawing.Size(102, 21);
            this.rbFormat2.TabIndex = 1;
            this.rbFormat2.TabStop = true;
            this.rbFormat2.Text = "*EMOCIJA.*";
            this.rbFormat2.UseVisualStyleBackColor = true;
            // 
            // rbFormat1
            // 
            this.rbFormat1.AutoSize = true;
            this.rbFormat1.Location = new System.Drawing.Point(7, 31);
            this.rbFormat1.Name = "rbFormat1";
            this.rbFormat1.Size = new System.Drawing.Size(102, 21);
            this.rbFormat1.TabIndex = 0;
            this.rbFormat1.TabStop = true;
            this.rbFormat1.Text = "EMOCIJA*.*";
            this.rbFormat1.UseVisualStyleBackColor = true;
            // 
            // numLength
            // 
            this.numLength.Location = new System.Drawing.Point(12, 266);
            this.numLength.Name = "numLength";
            this.numLength.Size = new System.Drawing.Size(135, 22);
            this.numLength.TabIndex = 4;
            this.numLength.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // lblLength
            // 
            this.lblLength.AutoSize = true;
            this.lblLength.Location = new System.Drawing.Point(9, 229);
            this.lblLength.Name = "lblLength";
            this.lblLength.Size = new System.Drawing.Size(146, 34);
            this.lblLength.TabIndex = 5;
            this.lblLength.Text = "Duljina koda emocije\r\n(pr: 123SRE.bmp = 3)\r\n";
            // 
            // tbStrah
            // 
            this.tbStrah.Location = new System.Drawing.Point(340, 115);
            this.tbStrah.Name = "tbStrah";
            this.tbStrah.Size = new System.Drawing.Size(100, 22);
            this.tbStrah.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(288, 118);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "Strah:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(277, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Srdžba:";
            // 
            // tbSrdzba
            // 
            this.tbSrdzba.Location = new System.Drawing.Point(340, 143);
            this.tbSrdzba.Name = "tbSrdzba";
            this.tbSrdzba.Size = new System.Drawing.Size(100, 22);
            this.tbSrdzba.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(268, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Gađenje:";
            // 
            // tbGadenje
            // 
            this.tbGadenje.Location = new System.Drawing.Point(340, 171);
            this.tbGadenje.Name = "tbGadenje";
            this.tbGadenje.Size = new System.Drawing.Size(100, 22);
            this.tbGadenje.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(277, 202);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 17);
            this.label4.TabIndex = 13;
            this.label4.Text = "Radost:";
            // 
            // tbRadost
            // 
            this.tbRadost.Location = new System.Drawing.Point(340, 199);
            this.tbRadost.Name = "tbRadost";
            this.tbRadost.Size = new System.Drawing.Size(100, 22);
            this.tbRadost.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(260, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "Neutralno:";
            // 
            // tbNeutralno
            // 
            this.tbNeutralno.Location = new System.Drawing.Point(340, 227);
            this.tbNeutralno.Name = "tbNeutralno";
            this.tbNeutralno.Size = new System.Drawing.Size(100, 22);
            this.tbNeutralno.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(289, 258);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 17);
            this.label6.TabIndex = 17;
            this.label6.Text = "Tuga:";
            // 
            // tbTuga
            // 
            this.tbTuga.Location = new System.Drawing.Point(340, 255);
            this.tbTuga.Name = "tbTuga";
            this.tbTuga.Size = new System.Drawing.Size(100, 22);
            this.tbTuga.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(245, 286);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 17);
            this.label7.TabIndex = 19;
            this.label7.Text = "Iznenađenje:";
            // 
            // tbIznenadenje
            // 
            this.tbIznenadenje.Location = new System.Drawing.Point(340, 283);
            this.tbIznenadenje.Name = "tbIznenadenje";
            this.tbIznenadenje.Size = new System.Drawing.Size(100, 22);
            this.tbIznenadenje.TabIndex = 18;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(250, 352);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(204, 43);
            this.button2.TabIndex = 20;
            this.button2.Text = "Izgradi klasifikator";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(309, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 34);
            this.label8.TabIndex = 21;
            this.label8.Text = "Kodovi u nazivu za \r\npojedinu emociju";
            // 
            // CustomDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 430);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tbIznenadenje);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.tbTuga);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbNeutralno);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbRadost);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbGadenje);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbSrdzba);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbStrah);
            this.Controls.Add(this.lblLength);
            this.Controls.Add(this.numLength);
            this.Controls.Add(this.gbNameFormat);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblLocation);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomDataForm";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "P42";
            this.gbNameFormat.ResumeLayout(false);
            this.gbNameFormat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numLength)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.GroupBox gbNameFormat;
        private System.Windows.Forms.RadioButton rbFormat2;
        private System.Windows.Forms.RadioButton rbFormat1;
        private System.Windows.Forms.NumericUpDown numLength;
        private System.Windows.Forms.Label lblLength;
        private System.Windows.Forms.TextBox tbStrah;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbSrdzba;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbGadenje;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbRadost;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbNeutralno;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbTuga;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbIznenadenje;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label8;
    }
}