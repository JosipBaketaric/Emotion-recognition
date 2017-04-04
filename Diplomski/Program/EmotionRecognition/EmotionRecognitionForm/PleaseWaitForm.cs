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
    public partial class PleaseWaitForm : Form
    {
        public PleaseWaitForm(string text)
        {
            InitializeComponent();

            rtbWaitMessage.AppendText(text);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
