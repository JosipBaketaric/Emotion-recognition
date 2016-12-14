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
using System.Timers;
using System.Threading;

namespace EmotionRecognitionForm
{
    public partial class Form1 : Form
    {
        private static string TrainingSet = @"C:\Users\Josip\Desktop\Emotion-recognition\Diplomski\Program\EmotionRecognition\Data\HaarCascade\haarcascade_frontalface_alt_tree.xml";
        private FilterInfoCollection VideoCaptureDevices;   //All devices
        private VideoCaptureDevice FinalVideoSource;    //Used one
        private Classifier faceClassifier;
        private System.Timers.Timer myTimer;
        private volatile string[] Emotions = new string[7] { "Strah", "Srdžba", "Gađenje", "Radost", "Neutralno", "Tuga", "Iznenađenje" };
        private bool FirstStart = true;
        private PleaseWaitForm pleaseWait;
        private object lockObject = new object();

        public Form1()
        {
            InitializeComponent();
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach(FilterInfo device in VideoCaptureDevices)
                comboBox1.Items.Add(device.Name);

            comboBox1.SelectedIndex = 0;

            faceClassifier = new Classifier(TrainingSet, 80, 700, 2, 1.05);
            myTimer = new System.Timers.Timer();

            pleaseWait = new PleaseWaitForm();

            //Buttons
            btnStop.Enabled = false;
            btnProcess.Enabled = false;
            button1.Enabled = false;
            btnProcess.Enabled = false;
            btnPokreni.Enabled = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(FinalVideoSource == null || !FinalVideoSource.IsRunning)
            {
                FinalVideoSource = new VideoCaptureDevice(VideoCaptureDevices[comboBox1.SelectedIndex].MonikerString);
                FinalVideoSource.NewFrame += new NewFrameEventHandler(FinalVideoSource_NewFrame);
                FinalVideoSource.Start();

                //buttons
                btnStop.Enabled = true;
                btnProcess.Enabled = true;
                button1.Enabled = true;
                btnProcess.Enabled = true;
                btnPokreni.Enabled = false;
            }

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
            if (FinalVideoSource != null && FinalVideoSource.IsRunning)
            {
                FinalVideoSource.Stop();
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Process();
        }

        public void Process()
        {           
            
            lock (lockObject)
            {
                Image img = GetImage();
                Bitmap imgBitmap = new Bitmap(img);
                //Get face
                var faceImg = faceClassifier.Find(imgBitmap);
                if (faceImg != null)
                {
                    if (FirstStart)
                    {
                        pleaseWait.Show();
                        Application.DoEvents();
                    }

                    //Set img to result
                    UpdatePictureBox((Bitmap)faceImg.Clone());
                    //Get data           

                    var data = ProcessImage.Process(faceImg);
                    //Get class

                    var result = Classify.GetInstance().GetClass(data);

                    if (FirstStart)
                    {
                        FirstStart = false;
                        pleaseWait.Close();
                    }

                    if(result != null)
                    {
                        int emotion;
                        int.TryParse(result.ToString(), out emotion);
                        UpdateLabel(Emotions[emotion]);
                    }
                    

                    
                }
                else
                {
                    //Print that couldn't find face
                }
            }
           
        }

        public void Process(Bitmap img)
        {
            //Get face
            var faceImg = faceClassifier.Find(img);
            if (faceImg != null)
            {
                //Set img to result
                UpdatePictureBox(faceImg);
                //Get data
                var data = ProcessImage.Process(faceImg);
                //Get class
                if (FirstStart)
                {
                    pleaseWait.Show();
                    Application.DoEvents();
                }

                var result = Classify.GetInstance().GetClass(data);

                if (FirstStart)
                {
                    FirstStart = false;
                    pleaseWait.Close();
                }

                if (result != null)
                {
                    int emotion;
                    int.TryParse(result.ToString(), out emotion);
                    UpdateLabel(Emotions[emotion]);
                }
            }
            else
            {
                //Print that couldn't find face
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopCamera();
        }

        private void StopCamera()
        {
            if (FinalVideoSource != null && FinalVideoSource.IsRunning)
            {
                FinalVideoSource.Stop();
                //buttons
                btnStop.Enabled = false;
                btnProcess.Enabled = false;
                button1.Enabled = false;
                btnProcess.Enabled = false;
                btnPokreni.Enabled = true;
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (button1.Text.Equals("Automatski"))
            {
                button1.Text = "Zaustavi";               
                myTimer.Elapsed += new ElapsedEventHandler(TimerMethod);
                myTimer.Interval = 2000; // 2000 ms is two seconds
                myTimer.Start();

                btnProcess.Enabled = false;
                btnLoadImg.Enabled = false;
            }
            else
            {
                button1.Text = "Automatski";
                myTimer.Stop();

                btnProcess.Enabled = true;
                btnLoadImg.Enabled = true;
            }
        }

        public void TimerMethod(object source, ElapsedEventArgs e)
        {
            Process();            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLoadImg_Click(object sender, EventArgs e)
        {
            string path;
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                path = file.FileName;
                Bitmap img = new Bitmap(path);
                //
                StopCamera();

                UpdatePreviewBox((Bitmap)img.Clone());

                Process(img);
                img.Dispose();               
            }
        }


        private void UpdateLabel(string text)
        {
            if (this.lblEmocija.InvokeRequired)
            {
                this.lblEmocija.BeginInvoke((MethodInvoker)delegate () { this.lblEmocija.Text = text; ; });
            }
            else
            {
                this.lblEmocija.Text = text; ;
            }
        }

        private void UpdatePictureBox(Bitmap img)
        {
            if (this.pbResult.InvokeRequired)
            {
                this.pbResult.BeginInvoke((MethodInvoker)delegate () { this.pbResult.Image = img; });
            }
            else
            {
                this.pbResult.Image = img;
            }
        }

        private void UpdatePreviewBox(Bitmap img)
        {
            if (this.pictureBox1.InvokeRequired)
            {
                this.pictureBox1.BeginInvoke((MethodInvoker)delegate () { this.pictureBox1.Image = img; });
            }
            else
            {
                this.pictureBox1.Image = img;
            }
        }

        private Bitmap GetImage()
        {
            Bitmap img = null;
            if (this.pictureBox1.InvokeRequired)
            {
                this.pictureBox1.BeginInvoke((MethodInvoker)delegate () 
                {
                    img = new Bitmap(this.pictureBox1.Image);
                });
                while (img == null) ;
                return img;
                
            }
            else
            {
                img = new Bitmap(this.pictureBox1.Image);
                return img;
            }
        }

    }
}
