namespace EmotionRecognitionForm
{
    partial class PleaseWaitForm
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
            this.rtbWaitMessage = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbWaitMessage
            // 
            this.rtbWaitMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.rtbWaitMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rtbWaitMessage.Location = new System.Drawing.Point(12, 12);
            this.rtbWaitMessage.Name = "rtbWaitMessage";
            this.rtbWaitMessage.ReadOnly = true;
            this.rtbWaitMessage.Size = new System.Drawing.Size(345, 151);
            this.rtbWaitMessage.TabIndex = 0;
            this.rtbWaitMessage.Text = "";
            // 
            // PleaseWaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 175);
            this.ControlBox = false;
            this.Controls.Add(this.rtbWaitMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PleaseWaitForm";
            this.Text = "P42";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbWaitMessage;
    }
}