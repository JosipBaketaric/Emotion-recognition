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
using EmotionRecognition.Common;
using System.IO;

namespace EmotionRecognitionForm
{
    public partial class Form1 : Form
    {
        //Strict
        private static string TrainingSetDebug = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\Data\HaarCascade\haarcascade_frontalface_alt_tree.xml");
        private string TrainingSetFinal;
        
        //Loose
        //private static string TrainingSet = Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\Data\HaarCascade\haarcascade_frontalface_alt.xml");
        private FilterInfoCollection VideoCaptureDevices;   //All devices
        private VideoCaptureDevice FinalVideoSource;    //Used one
        private Classifier faceClassifier;
        private System.Timers.Timer myTimer;
        private volatile string[] Emotions = new string[7] { "Strah", "Srdžba", "Gađenje", "Radost", "Neutralno", "Tuga", "Iznenađenje" };
        private bool FirstStart = true;
        private PleaseWaitForm pleaseWait;
        private InfoForm infoForm;
        private TestForm testForm;
        private object lockObject = new object();
        private ResultTransfer rf;

        private bool testDone = false;
        private double[,] TestResultMatrix;

        public Form1()
        {
            InitializeComponent();

            TrainingSetDebug = Path.GetFullPath(TrainingSetDebug);

            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            foreach(FilterInfo device in VideoCaptureDevices)
                comboBox1.Items.Add(device.Name);

            comboBox1.SelectedIndex = 0;

            TrainingSetFinal = Environment.CurrentDirectory + "\\Data\\HaarCascade\\haarcascade_frontalface_alt_tree.xml";

            if (File.Exists(TrainingSetDebug))
            {
                faceClassifier = new Classifier(TrainingSetDebug, 80, 700, 2, 1.05);
            }
            else if (File.Exists(TrainingSetFinal))
            {
                try
                {
                    faceClassifier = new Classifier(TrainingSetFinal, 80, 700, 2, 1.05);
                }
                catch(Exception e)
                {
                    MessageBox.Show("Exception: " + e.ToString());
                }
            }
            else
            {                
                MessageBox.Show("Missing HaarCascade training set!" + "\n" + TrainingSetFinal);
            }

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
            try{
                Process();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error occured in Process method");
                MessageBox.Show(ex.ToString());
            }

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
                     
                        if (result != null)
                        {
                            rf = result;
                        }
                    }

                    if(result != null)
                    {
                        int emotion;
                        int.TryParse(result.Result.ToString(), out emotion);
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

                    if(result != null)
                    {
                        rf = result;
                    }
                }

                if (result != null)
                {
                    int emotion;
                    int.TryParse(result.Result.ToString(), out emotion);
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
            try
            {
                Process();
            }
               catch(Exception)
            {
                MessageBox.Show("Error while using automatic recognition");
            }     
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLoadImg_Click(object sender, EventArgs e)
        {
            try
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
            catch(Exception)
            {
                MessageBox.Show("Error while loading image!");
            }
        }


        private void UpdateLabel(string text)
        {
            try
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
            catch(Exception)
            {
                MessageBox.Show("Error while updating label text");
            }
        }

        private void UpdatePictureBox(Bitmap img)
        {
            try
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
            catch(Exception)
            {
                MessageBox.Show("Error while updating picture box");
            }
        }

        private void UpdatePreviewBox(Bitmap img)
        {
            try
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
            catch(Exception)
            {
                MessageBox.Show("Error while updating preview box");
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

        private void btnInfo_Click(object sender, EventArgs e)
        {
            if(rf != null)
            {
                infoForm = new InfoForm(rf, Emotions);
                infoForm.Show();
                Application.DoEvents();
            }
            else
            {
                MessageBox.Show("Unavaible");
            }

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                //Process everything
                if (testDone == false)
                {
                    TestResultMatrix = ProcessTest(); //Return results
                }
                
                if(TestResultMatrix != null)
                {
                    testForm = new TestForm(TestResultMatrix);  //Send results
                    testForm.Show();
                    Application.DoEvents();
                    testDone = true;
                }

                
            }
           catch(Exception)
            {
                MessageBox.Show("Error while trying to execute Test!");
            }
        }

        private double[,] ProcessTest()
        {
            TestResultMatrix = new double[7,7];
            //Init
            for(int i = 0; i < TestResultMatrix.Length/7; i++)
            {
                for (int j = 0; j < TestResultMatrix.Length/7; j++)
                {
                    TestResultMatrix[i, j] = 0;
                }
            }
                

            string database = @"C:\Users\" + Environment.UserName + @"\Desktop\cohn-kanade-images\";
            string emotionCodesDatabase = @"C:\Users\" + Environment.UserName + @"\Desktop\Emotion\";

            if (!Directory.Exists(database))
            {
                MessageBox.Show("Missing: " + database);
                return null;
            }
            if (!Directory.Exists(emotionCodesDatabase))
            {
                MessageBox.Show("Missing: " + emotionCodesDatabase);
                return null;
            }


            //string[] Emotions = new string[7] { "Strah", "Srdžba", "Gađenje", "Radost", "Neutralno", "Tuga", "Iznenađenje" };
            int[] emotionCodes = new int[8] { 4, 1, -1, 2, 0, 3, 5, 6 };

            List<string> dirs = new List<string>(System.IO.Directory.EnumerateDirectories(database));

            foreach(var directory in dirs)
            {
                List<string> subDirs = new List<string>(System.IO.Directory.EnumerateDirectories(directory));
                foreach(var subDirectory in subDirs)
                {
                    List<string> files = new List<string>(System.IO.Directory.EnumerateFiles(subDirectory));
                    //Target image
                    Bitmap targetItem = new Bitmap(files.Last());
                    //Find emotion code
                    string emotionCode;
                    try
                    {
                        string emotionCodePath = emotionCodesDatabase + Path.GetFileName(Path.GetDirectoryName(subDirectory)) + @"\" + Path.GetFileName(subDirectory);
                        List<string> emotionCodeFiles = new List<string>(System.IO.Directory.EnumerateFiles(emotionCodePath));

                        emotionCode = File.ReadAllText(emotionCodeFiles.First());
                        if (emotionCode.Equals(""))
                            continue;
                    }
                    catch
                    {
                        continue;
                    }
                    int emotionCodeInt = -1; ;
                    
                    for(int i = 0; i < emotionCode.Length; i++)
                    {
                        if (emotionCode.Substring(i, 1).Equals("0") || emotionCode.Substring(i, 1).Equals("1") || emotionCode.Substring(i, 1).Equals("2")
                            || emotionCode.Substring(i, 1).Equals("3") || emotionCode.Substring(i, 1).Equals("4") || emotionCode.Substring(i, 1).Equals("5")
                            || emotionCode.Substring(i, 1).Equals("6") || emotionCode.Substring(i, 1).Equals("7"))
                        {
                            int.TryParse(emotionCode.Substring(i, 1), out emotionCodeInt);
                            break;
                        }                           
                    }

                    if (emotionCodeInt == -1 || emotionCodeInt == 2)    //2 = contempt
                        continue;

                    //Classfy
                    var faceImg = faceClassifier.Find(targetItem);
                    if (faceImg == null)
                        continue;
                    var data = ProcessImage.Process(faceImg);
                    var result = Classify.GetInstance().GetClass(data);

                    int emotion;
                    int.TryParse(result.Result.ToString(), out emotion);
                    //True
                    if (emotion.Equals(emotionCodes[emotionCodeInt]))
                    {
                        TestResultMatrix[emotion, emotion] += 1;
                    }
                    //Failed
                    else
                    {
                        TestResultMatrix[emotionCodes[emotionCodeInt], emotion] += 1;
                    }

                }

            }
            return TestResultMatrix;
        }

        private void lblTestWait_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
