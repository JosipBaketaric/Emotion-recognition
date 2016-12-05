using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using EmotionRecognition.Service;
using EmotionRecognition.Weka;

namespace EmotionRecognitionForm
{
    public partial class Form1 : Form
    {
        private FilterInfoCollection VideoCaptureDevices;   //All devices
        private VideoCaptureDevice FinalVideoSource;    //Used one

        public Form1()
        {
            InitializeComponent();
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach(FilterInfo device in VideoCaptureDevices)
                comboBox1.Items.Add(device.Name);

            comboBox1.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FinalVideoSource = new VideoCaptureDevice (VideoCaptureDevices[comboBox1.SelectedIndex].MonikerString);
            FinalVideoSource.ProvideSnapshots = true;
            FinalVideoSource.NewFrame += new NewFrameEventHandler(FinalVideoSource_NewFrame);
            FinalVideoSource.Start();
        }

        private void FinalVideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap image = (Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = image;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (FinalVideoSource.IsRunning)
            {
                FinalVideoSource.Stop();
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            var img = pictureBox1.Image;

            Bitmap imgBitmap = new Bitmap(img);

            var data = ProcessImage.Process(imgBitmap);
            var result = Classify.GetInstance().GetClass(data);
        }


    }
}
