﻿using EmotionRecognition.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmotionRecognitionForm
{
    public partial class CustomDataForm : Form
    {
        private volatile string[] Emotions = new string[7] { "Strah", "Srdžba", "Gađenje", "Radost", "Neutralno", "Tuga", "Iznenađenje" };
        private string emotionCods;
        private string location;
        private Classifier faceClassifier;
        private bool headerAppended;
        private int featureNumber;
        private string featurePath;
        private string nameCustom;
        private bool fBuilt;

        public CustomDataForm(Classifier face)
        {
            InitializeComponent();
            faceClassifier = face;
            headerAppended = false;
            fBuilt = false;

            nameCustom = "custom-" + GetTimestamp(DateTime.Now); ;         
            rbFormat1.Checked = true;

            //Access
            string dir1 = Directory.GetCurrentDirectory().ToString() + "\\Data\\CustomFeatures\\";
            string dir2 = Directory.GetCurrentDirectory().ToString() + "\\Data\\Models\\";

            
            /**
            setAccessRights(dir1);
            setAccessRights(dir2);
            */

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (CheckInputs())
            {
                PleaseWaitForm pleaseWait = new PleaseWaitForm("Molimo pričekajte dok se ne izgradi skup podataka");
                pleaseWait.Show();
                Application.DoEvents();

                ProcessEmotionCodes();
                ProcessData();
                MessageBox.Show("Done!");
                fBuilt = true;

                pleaseWait.Hide();
            }
            else
            {
                MessageBox.Show("Input data error!");
            }
        }
        private bool CheckInputs()
        {
            if (location == null || location.Length == 0)
                return false;
            if (numLength.Value <= 0)
                return false;
            //Textboxes
            if (tbStrah.Text == null || tbStrah.Text == "")
                return false;
            if (tbSrdzba.Text == null || tbSrdzba.Text == "")
                return false;
            if (tbGadenje.Text == null || tbGadenje.Text == "")
                return false;
            if (tbRadost.Text == null || tbRadost.Text == "")
                return false;
            if (tbNeutralno.Text == null || tbNeutralno.Text == "")
                return false;
            if (tbTuga.Text == null || tbTuga.Text == "")
                return false;
            if (tbIznenadenje.Text == null || tbIznenadenje.Text == "")
                return false;

            return true;
        }

        private void ProcessEmotionCodes()
        {
            emotionCods = "{" + tbStrah.Text + "," + tbSrdzba.Text + "," + tbGadenje.Text + "," +
                tbRadost.Text + "," + tbNeutralno.Text + "," + tbTuga.Text + "," + tbIznenadenje.Text + "}";
        }

        private void ProcessData()
        {
            List<string> dirs = new List<string>(System.IO.Directory.EnumerateDirectories(location));
            List<string> files = new List<string>(System.IO.Directory.EnumerateFiles(location));

            if(dirs.Count > files.Count)
            {
                dirSetup(dirs);
            }
            else
            {
                fileSetup(files);
            }

        }

        private void dirSetup(List<string> dirs)
        {
            foreach(var dir in dirs)
            {
                List<string> files = new List<string>(System.IO.Directory.EnumerateFiles(dir));
                foreach (var file in files)
                {
                    //Load image
                    Bitmap image = new Bitmap(file);
                    //Get face
                    Bitmap face = faceClassifier.Find(image);
                    if (face == null)
                        continue;
                    //name
                    String name = Path.GetFileNameWithoutExtension(file);
                    //Code
                    String emotionCode = getEmotionCodeFromName(name);
                    List<double> features = ProcessImage.Process(face);

                    writeToFile(features, emotionCode);
                }
            }


        }

        private void fileSetup(List<string> files)
        {
            foreach (var file in files)
            {
                //Load image
                Bitmap image = new Bitmap(file);
                //Get face
                Bitmap face = faceClassifier.Find(image);
                if (face == null)
                    continue;
                //name
                String name = Path.GetFileNameWithoutExtension(file);
                //Code
                String emotionCode = getEmotionCodeFromName(name);
                List<double> features = ProcessImage.Process(face);

                writeToFile(features, emotionCode);
            }
        }

        private void writeToFile(List<double> features, String emotionCode)
        {
            //Append header
            if (!headerAppended)
            {
                featurePath = Directory.GetCurrentDirectory().ToString() + "\\Data\\CustomFeatures\\" + nameCustom + ".arff";
                featureNumber = features.Count;
                headerAppended = true;
                EmotionRecognition.Service.Write.WriteArff.AppendHeader(featurePath, featureNumber, emotionCods);
            }
            //Write features
            if (featureNumber == features.Count)
            {
                EmotionRecognition.Service.Write.WriteArff.Write(features, emotionCode);
            }
        }


        private String getEmotionCodeFromName(String name)
        {
            String Code = "";
            if (rbFormat1.Checked)
            {
                for(int i = 0; i < numLength.Value; i++)
                {
                    Code += name.ElementAt(i);
                }
                
            }
            else if (rbFormat2.Checked)
            {
                int size;
                int.TryParse(numLength.Value.ToString(), out size);

                for (int i = (name.Length - size); i < name.Length; i++)
                {
                    Code += name.ElementAt(i);
                }
            }

            return Code;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lblLocation.Text = folderBrowserDialog1.SelectedPath;
                location = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (fBuilt)
            {
                PleaseWaitForm pleaseWait = new PleaseWaitForm("Molimo pričekajte dok se ne izgradi model");
                pleaseWait.Show();
                Application.DoEvents();

                string saveLocation = Directory.GetCurrentDirectory().ToString() + "\\Data\\Models\\" + nameCustom + ".model";
                featurePath = Directory.GetCurrentDirectory().ToString() + "\\Data\\CustomFeatures\\" + nameCustom + ".arff";

                EmotionRecognition.Weka.Classifiers.SVMBuildAndSave(featurePath, saveLocation);

                pleaseWait.Hide();
                MessageBox.Show("Done!");
            }
            else
            {
                MessageBox.Show("First build dataset!");
            }
            
        }

        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyyMMddHHmmssffff");
        }

        private void setAccessRights(string path)
        {
            var rule = allAccess();
            DirectorySecurity dirSecurity = Directory.GetAccessControl(path);
            dirSecurity.AddAccessRule(rule);

            Directory.SetAccessControl(path, dirSecurity);
        }

        private FileSystemAccessRule allAccess()
        {
            IdentityReference everybodyIdentity = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

            FileSystemAccessRule rule = new FileSystemAccessRule(
                everybodyIdentity,
                FileSystemRights.FullControl,
                InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                PropagationFlags.None,
                AccessControlType.Allow);
            return rule;
        }

    }
}
