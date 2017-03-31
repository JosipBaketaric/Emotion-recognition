using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private List<string> haarNamesList;
        private List<string> haarPathList;
        private List<String> modelNamesList;
        private List<string> modelPathList;
        private List<int> ComboIntervalTimes;
        
        private FilterInfoCollection VideoCaptureDevices;   //All devices
        private VideoCaptureDevice FinalVideoSource;    //Used one
        private Classifier faceClassifier;

        private volatile string[] Emotions = new string[7] { "Strah", "Srdžba", "Gađenje", "Radost", "Neutralno", "Tuga", "Iznenađenje" };

        private PleaseWaitForm pleaseWait;
        private InfoForm infoForm;
        private TestForm testForm;

        private object lockObject = new object();

        private ResultTransfer rf;
       
        private double[,] TestResultMatrix;

        private volatile bool fEvaluated;
        private volatile bool fAutomatic;
        private bool testDone = false;


        public Form1()
        {         
            try
            {
                InitializeComponent();

                comboSetup();
                pleaseWait = new PleaseWaitForm();

                fEvaluated = false;
                fAutomatic = false;

                //Buttons
                btnStop.Enabled = false;
                btnProcess.Enabled = false;
                button1.Enabled = false;
                btnProcess.Enabled = false;
                btnPokreni.Enabled = true;
            }
            catch(Exception e)
            {
                MessageBox.Show("Exception: " + e);
            }

        }

        private void comboSetup()
        {
            //Combo setup
            ComboIntervalTimes = new List<int>();
            modelNamesList = new List<string>();
            modelPathList = new List<string>();

            //Camera combo
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in VideoCaptureDevices)
                comboBox1.Items.Add(device.Name);

            comboBox1.SelectedIndex = 0;


            //Models combo
            LoadModelsList();
            foreach (var item in modelNamesList)
                cbClassificatorList.Items.Add(item);

            cbClassificatorList.SelectedIndex = 0;

            //Face classifiers combo
            LoadHaarComboList();
            foreach (var item in haarNamesList)
                cbClassifiers.Items.Add(item);

            cbClassifiers.SelectedIndex = 0;
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
                Application.Exit();
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try{
                
                Process();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Greška se dogodila u metodi za obradu!");
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
                    //Set img to result
                    UpdatePictureBox((Bitmap)faceImg.Clone());
                    //Get data           
                    var data = ProcessImage.Process(faceImg);
                    //Get class
                    var result = Classify.GetInstance().GetClass(data);

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
                var result = Classify.GetInstance().GetClass(data);

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
            if (!fAutomatic)
            {
                fAutomatic = true;
                button1.Text = "Zaustavi";
                automatic();              
            }             
            else
            {
                fAutomatic = false;
                button1.Text = "Automatski";
            }
                
        }

        public void automatic()
        {
            new Thread(delegate () {
                while (fAutomatic)
                {
                    Process();
                    Thread.Sleep(500);
                }
            }).Start();
           
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            fAutomatic = true;
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
                MessageBox.Show("Greška prilikom učitavanja slike!");
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
                MessageBox.Show("Greška prilikom promjene teksta na labeli");
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
                MessageBox.Show("Greška prilikom promjene slike");
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
                MessageBox.Show("Greška prilikom promjene slike");
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
            if (fEvaluated)
            {
                infoForm = new InfoForm(rf, Emotions);
                infoForm.Show();
                Application.DoEvents();
            }
            else
            {
                openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory().ToString();
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string dataSetPath = openFileDialog1.FileName;
                    new Thread(delegate () {
                        evaluate(dataSetPath);
                    }).Start();               
                }               
            }                       
        }


        private void evaluate(string path)
        {

            if (!File.Exists(path))
            {
                MessageBox.Show("Error! dataset not found: " + path);
                return;
            }
            pleaseWait.Show();
            Application.DoEvents();

            //Button disable
            if (this.btnInfo.InvokeRequired)
                this.btnInfo.BeginInvoke((MethodInvoker)delegate () { this.btnInfo.Enabled = false; });
            else
                this.btnInfo.Enabled = false;

            if (this.btnInfo.InvokeRequired)
                this.btnProcess.BeginInvoke((MethodInvoker)delegate () { this.btnProcess.Enabled = false; });
            else
                this.btnProcess.Enabled = false;

            if (this.button1.InvokeRequired)
                this.button1.BeginInvoke((MethodInvoker)delegate () { this.button1.Enabled = false; });
            else
                this.button1.Enabled = false;

            if (this.btnLoadImg.InvokeRequired)
                this.btnLoadImg.BeginInvoke((MethodInvoker)delegate () { this.btnLoadImg.Enabled = false; });
            else
                this.btnLoadImg.Enabled = false;
            if (this.btnTest.InvokeRequired)
                this.btnTest.BeginInvoke((MethodInvoker)delegate () { this.btnTest.Enabled = false; });
            else
                this.btnTest.Enabled = false;


            //Evaluate
            Classify.GetInstance().evaluateClassifier(path);
            rf = Classify.GetInstance().rf;

            pleaseWait.Hide();

            //Button enable
            if (this.btnInfo.InvokeRequired)
                this.btnInfo.BeginInvoke((MethodInvoker)delegate () { this.btnInfo.Enabled = true; });
            else
                this.btnInfo.Enabled = true;

            if (this.btnInfo.InvokeRequired)
                this.btnProcess.BeginInvoke((MethodInvoker)delegate () { this.btnProcess.Enabled = true; });
            else
                this.btnProcess.Enabled = true;

            if (this.button1.InvokeRequired)
                this.button1.BeginInvoke((MethodInvoker)delegate () { this.button1.Enabled = true; });
            else
                this.button1.Enabled = true;

            if (this.btnLoadImg.InvokeRequired)
                this.btnLoadImg.BeginInvoke((MethodInvoker)delegate () { this.btnLoadImg.Enabled = true; });
            else
                this.btnLoadImg.Enabled = true;

            if (this.btnTest.InvokeRequired)
                this.btnTest.BeginInvoke((MethodInvoker)delegate () { this.btnTest.Enabled = true; });
            else
                this.btnTest.Enabled = true;

            //Show results
            if (rf != null)
                {

                if (this.btnInfo.InvokeRequired)
                    this.btnInfo.BeginInvoke((MethodInvoker)delegate () { this.btnInfo.Text = "Results"; });
                else
                    this.btnInfo.Text = "Results";
                    fEvaluated = true;
            }
                else
                {
                    MessageBox.Show("Nedostupno");
                }
        }


        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                //Process everything
                if (testDone == false)
                {
                    MessageBox.Show("Ovo može potrajati minutu - dvije (ovisno o performansama računala)\nPrilikom izvođenja testiranja preporučava se ne korištenje programa");
                    new Thread(() =>
                    {
                        if (this.btnTest.InvokeRequired)
                            this.btnTest.BeginInvoke((MethodInvoker)delegate () { this.btnTest.Enabled = false; });
                        else
                            this.btnTest.Enabled = false;

                        TestResultMatrix = ProcessTest(); //Return results

                        if (this.btnTest.InvokeRequired)
                            this.btnTest.BeginInvoke((MethodInvoker)delegate () { this.btnTest.Enabled = true; btnTest.Text = "Rezultati"; });
                        else
                        {
                            this.btnTest.Enabled = true;
                            btnTest.Text = "Rezultati";
                        }

                        faceClassifier = new Classifier(haarPathList.ElementAt(cbClassifiers.SelectedIndex), 80, 700, 2, 1.05);

                        testDone = true;
                    }).Start();

                }

                if (TestResultMatrix != null)
                {
                    testForm = new TestForm(TestResultMatrix);  //Send results
                    testForm.Show();
                    Application.DoEvents();
                    testDone = true;
                }                             
            }
           catch(Exception)
            {
                MessageBox.Show("Greška prilikom pokušaja izvršenja testa");
            }
        }

        private double[,] ProcessTest()
        {
            try
            {
                TestResultMatrix = new double[7, 7];
                //Init
                for (int i = 0; i < TestResultMatrix.Length / 7; i++)
                {
                    for (int j = 0; j < TestResultMatrix.Length / 7; j++)
                    {
                        TestResultMatrix[i, j] = 0;
                    }
                }

                string database = @"C:\Users\" + Environment.UserName + @"\Desktop\cohn-kanade-images\";
                string emotionCodesDatabase = @"C:\Users\" + Environment.UserName + @"\Desktop\Emotion\";

                if (!Directory.Exists(database))
                {
                    MessageBox.Show("Nedostaje: " + database);
                    return null;
                }
                if (!Directory.Exists(emotionCodesDatabase))
                {
                    MessageBox.Show("Nedostaje: " + emotionCodesDatabase);
                    return null;
                }


                //string[] Emotions = new string[7] { "Strah", "Srdžba", "Gađenje", "Radost", "Neutralno", "Tuga", "Iznenađenje" };
                int[] emotionCodes = new int[8] { 4, 1, -1, 2, 0, 3, 5, 6 };

                List<string> dirs = new List<string>(System.IO.Directory.EnumerateDirectories(database));

                foreach (var directory in dirs)
                {
                    List<string> subDirs = new List<string>(System.IO.Directory.EnumerateDirectories(directory));
                    foreach (var subDirectory in subDirs)
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

                        for (int i = 0; i < emotionCode.Length; i++)
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
            catch(Exception ex)
            {
                MessageBox.Show("Greška prilikom izvršenja testa! \n " + ex);
                return null;
            }
            
        }

        private void lblTestWait_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbClassifiers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                faceClassifier = new Classifier(haarPathList.ElementAt(cbClassifiers.SelectedIndex), 80, 700, 2, 1.05);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Nemoguće pronaći: " + haarNamesList.ElementAt(cbClassifiers.SelectedIndex) + "\n\n" + "Greška:" + "\n" + ex);
            }
            
        }

        private void cbIntervalTimes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void LoadModelsList()
        {
            modelNamesList = new List<string>();
            modelPathList = new List<string>();
            String[] allModels;  
                 
            String modelsFolderPath = Directory.GetCurrentDirectory().ToString() + "\\Data\\Models\\";
           
            if(Directory.Exists(modelsFolderPath))
            {
                allModels = Directory.GetFiles(modelsFolderPath);
            }
            else
            {
                string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
                directory = System.IO.Directory.GetParent(directory).ToString();
                directory = System.IO.Directory.GetParent(directory).ToString();
                String debugModelsFolderPath = directory + "\\Data\\Models\\";
                allModels = Directory.GetFiles(debugModelsFolderPath);
            }

            foreach (var model in allModels)
            {
                var modelSplit = model.Split('\\');
                
                modelNamesList.Add( modelSplit.Last() );
                modelPathList.Add(model);
            }

        }

        private void LoadHaarComboList()
        {
            haarNamesList = new List<string>();
            haarPathList = new List<string>();
            String[] allHaars;

            String modelsFolderPath = Directory.GetCurrentDirectory().ToString() + "\\Data\\HaarCascade\\";
           
            if (Directory.Exists(modelsFolderPath))
            {
                allHaars = Directory.GetFiles(modelsFolderPath);
            }
            else
            {
                string directory = System.IO.Directory.GetParent(System.IO.Directory.GetParent(Environment.CurrentDirectory).ToString()).ToString();
                directory = System.IO.Directory.GetParent(directory).ToString();
                directory = System.IO.Directory.GetParent(directory).ToString();
                String debugModelsFolderPath = directory + "\\Data\\HaarCascade\\";

                allHaars = Directory.GetFiles(debugModelsFolderPath);
            }


            foreach (var model in allHaars)
            {
                var haarSplit = model.Split('\\');
                string name;

                name = haarSplit.Last();

                haarNamesList.Add(name);
                haarPathList.Add(model);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void cbClassificatorList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Classify.GetInstance().setClassifier( modelPathList.ElementAt(cbClassificatorList.SelectedIndex) );
                fEvaluated = false;
                btnInfo.Text = "Evaluate";
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show("Nemoguće pronaći: " + modelNamesList.ElementAt(cbClassificatorList.SelectedIndex) + "\n\n" + "Greška:" + "\n" + ex);
            }
        }

        private void btnCustomSet_Click(object sender, EventArgs e)
        {
            //Show form and send data
            CustomDataForm customDataForm = new CustomDataForm(faceClassifier);
            customDataForm.Show();
            Application.DoEvents();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            fAutomatic = true;
            Application.Exit();
        }

        private void pictureBox1_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void pictureBox1_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
        }
    }
}
