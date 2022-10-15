using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.IO.Compression;
using System.Media;
using System.Runtime.InteropServices;
using NAudio.Wave.SampleProviders;
using NAudio.Wave;

namespace MusicXMLParser
{
    /// <summary>
    /// Original source written in ruby was found at:
    /// https://github.com/shvelo/musicxml_to_arduino
    /// 
    /// C# conversion found here:
    /// https://github.com/MrRedBeard/DotNet-MXL-Parsing-for-Arduino
    /// 
    /// Refactored and updated here:
    /// https://github.com/DavidWeed/DotNet-MXL-Parsing-for-Arduino
    /// </summary>
    

    public partial class FormMain : Form
    {
        [DllImport("kernel32.dll")]
        private static extern bool Beep(int frequency, int duration);

        private Thread SoundPlayThread;
        private CancellationTokenSource stopSoundPlay;

        HelperContainer helper = new HelperContainer(); //used to store dict of frequncies
        List<Note> notes = new List<Note>(); //used to store the notes

        public FormMain()
        {
            InitializeComponent();

            stopSoundPlay = new CancellationTokenSource();
            resizeForm();

            //Right Click Copy on RTB
            ToolStripMenuItem tsmiCopy = new ToolStripMenuItem("Copy");
            tsmiCopy.Click += (sender, e) => rtbArduinoCode.Copy();
            contextMenuStripArduinoRTB.Items.Add(tsmiCopy);

            rtbArduinoCode.ContextMenuStrip = contextMenuStripArduinoRTB;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {

        }

        public void LoadMXL(string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            
            XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("/score-partwise/part");
           
            double divisions = Convert.ToDouble(xmlDoc.DocumentElement.SelectSingleNode("/score-partwise/part/measure/attributes/divisions").InnerText.Trim(' '));

            double tempo = 120; //Temp override 

            //if (xmlDoc.DocumentElement.SelectSingleNode("/score-partwise/part/measure/direction/sound").Attributes["tempo"].Value != null)
            //{
            //    tempo = Math.Round(Convert.ToDouble(xmlDoc.DocumentElement.SelectSingleNode("/score-partwise/part/measure/direction/sound").Attributes["tempo"].Value.Trim(' ')));
            //}
            //else
            //{
            //    tempo = 30;
            //}

            int oneDuration = (int)Math.Round(60.0 / tempo / divisions * 1000.0);
                        
            XmlNodeList subnodes = node.SelectNodes("measure/note");

            foreach (XmlNode snode in subnodes)
            {
                Note n = new Note();
                string step = "";
                string octave = "";

                double freqMult = 1.0;

                n.voice = snode.SelectSingleNode("voice").InnerText;


                if (snode.SelectSingleNode("rest") != null)
                {                    
                    n.frequency = 0;
                    n.duration = oneDuration * Convert.ToInt32(snode.SelectSingleNode("duration").InnerText);                   
                }
                else if (snode.SelectSingleNode("pitch").SelectSingleNode("alter") != null)
                {
                    step = snode.SelectSingleNode("pitch").SelectSingleNode("step").InnerText;
                    octave = snode.SelectSingleNode("pitch").SelectSingleNode("octave").InnerText;
                    n.noteString = "NOTE_" + step + octave;
                    n.frequency = helper.pitches[n.noteString];
                    if (snode.SelectSingleNode("pitch").SelectSingleNode("alter").InnerText == "1")
                    {
                        n.frequency = (int)Math.Round(n.frequency * 1.05946);
                    }
                    else
                    {
                        n.frequency = (int)Math.Round(n.frequency / 1.05946);
                    }
                    
                    n.duration = oneDuration * Convert.ToInt32(snode.SelectSingleNode("duration").InnerText);                    
                }
                else
                {
                    step = snode.SelectSingleNode("pitch").SelectSingleNode("step").InnerText;
                    octave = snode.SelectSingleNode("pitch").SelectSingleNode("octave").InnerText; n.noteString = "NOTE_" + step + octave;

                    n.frequency = helper.pitches[n.noteString];
                    n.duration = oneDuration * Convert.ToInt32(snode.SelectSingleNode("duration").InnerText);                    
                }

                int fifths = Convert.ToInt32(xmlDoc.DocumentElement.SelectSingleNode("/score-partwise/part/measure/attributes/key").SelectSingleNode("fifths").Value);

                char[] sharps = { 'F', 'C', 'G', 'D', 'A', 'E', 'B' };
                char[] flats = { 'B', 'E', 'A', 'D', 'G', 'C', 'F' };

                if (fifths > 0)
                {
                    for (int i = 0; i < fifths; i++)
                    {
                        if (step[0] == sharps[i])
                        {
                            freqMult = 1.05946;
                        }
                    }
                }
                else if (fifths < 0)
                {
                    fifths *= -1;

                    for (int i = 0; i < fifths; i++)
                    {
                        if (step[0] == flats[i])
                        {
                            freqMult = (1.0 / 1.05946);
                        }
                    }
                }
                
                n.frequency = (int)Math.Round(n.frequency * freqMult);
                n.noteString = n.frequency.ToString();
                notes.Add(n);
                if(n.duration < 1200)
                {
                    lstFreq.Items.Add(n.frequency);
                    lstDurations.Items.Add(n.duration);
                }
            }
        }


        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            ResetForm();
            openFileDialogMXL.InitialDirectory = Environment.CurrentDirectory;
            openFileDialogMXL.Title = "Open MXL File";
            openFileDialogMXL.Filter = "MXL/XML Files (*.xml; *.mxl)|*.xml; *.mxl";
            openFileDialogMXL.CheckFileExists = true;
            openFileDialogMXL.CheckPathExists = true;
            openFileDialogMXL.Multiselect = false;

            if (openFileDialogMXL.ShowDialog() == DialogResult.OK)
            {
                txtMXLFile.Text = openFileDialogMXL.FileName;
                if (openFileDialogMXL.FileName.Length > 1)
                {
                    if (openFileDialogMXL.FileName.Contains(".xml"))
                    {
                        LoadMXL(File.ReadAllText(openFileDialogMXL.FileName));
                    }
                    else if (openFileDialogMXL.FileName.Contains(".mxl"))
                    {
                        unZipMXL(openFileDialogMXL.FileName);
                    }
                    else
                    {
                        MessageBox.Show("Please select either a '.mxl' or '.xml' file type.", "File Type Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        public void unZipMXL(string file)
        {
            FileInfo f = new FileInfo(file);
            FileStream originalFileStream = f.OpenRead();

            ZipArchive z = new ZipArchive(originalFileStream, ZipArchiveMode.Read);
            foreach (ZipArchiveEntry e in z.Entries)
            {
                if (!e.Name.Contains("container.xml"))
                {
                    e.ExtractToFile(Path.Combine(System.IO.Path.GetTempPath(), "temp.mxl"), true);
                    LoadMXL(File.ReadAllText(Path.Combine(System.IO.Path.GetTempPath(), "temp.mxl")));
                    break;
                }
            }
        }


        private void btnPlayPreview_Click(object sender, EventArgs e)
        {
            SoundPlayThread = new Thread(PlayPreview);
            SoundPlayThread.Name = "SoundPlayThread";
            SoundPlayThread.IsBackground = true;
            SoundPlayThread.Start();
        }

        private void btnStopPreview_Click(object sender, EventArgs e)
        {
            SoundPlayThread.Abort();
        }

        private void PlayPreview()
        {
            if (notes.Count == 0)
            {
                MessageBox.Show("There are no notes to play");
                return;
            }

            foreach (Note note in notes)
            {
                var sine = new SignalGenerator()
                {
                    Gain = 0.2,
                    Frequency = note.frequency,
                    Type = SignalGeneratorType.Sin
                }.Take(TimeSpan.FromMilliseconds(note.duration));
                using (var wo = new WaveOutEvent())
                {
                    wo.Init(sine);
                    wo.Play();
                    while (wo.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(50);
                    }
                }
            }
        }


        private void btnConvert_Click(object sender, EventArgs e)
        {
            rtbArduinoCode.Text = "";

            string melodyString = "\nint melody[] = { \n";
            string durationString = "int noteDurations[] = { \n";

            foreach (var freq in lstFreq.Items)
            {
                melodyString += freq.ToString() + ", ";
            }

            foreach (var dur in lstDurations.Items)
            {
                durationString += dur + ", ";
            }

            melodyString += "0 \n};\n";
            durationString += "0 \n};\n";

            rtbArduinoCode.Text += melodyString;
            rtbArduinoCode.Text += durationString;
            rtbArduinoCode.Text += helper.ArduinoBottom;
        }

        public void ResetForm()
        {
            lstFreq.Items.Clear();
            lstFreq.Text = "";
            lstDurations.Items.Clear();
            lstDurations.Text = "";
            rtbArduinoCode.Text = "";
            txtMXLFile.Text = "";
            openFileDialogMXL.Reset();
            notes = new List<Note>();
        }

        public void resizeForm()
        {
            buttonOpenFile.Left = 15;
            buttonOpenFile.Height = 23;
            buttonOpenFile.Top = 15;
            
            txtMXLFile.Height = 23;
            txtMXLFile.Top = 15 + 2;
            txtMXLFile.Left = buttonOpenFile.Width + buttonOpenFile.Location.X + 15;
            
            btnPlayPreview.Height = 23;
            btnPlayPreview.Top = 15;
            btnPlayPreview.Left = txtMXLFile.Width + txtMXLFile.Location.X + 15;

            btnStopPreview.Top = 15;
            btnStopPreview.Height = 23;
            btnStopPreview.Left = btnPlayPreview.Width + btnPlayPreview.Location.X + 15;

            btnConvert.Height = 23;
            btnConvert.Top = 15;
            btnConvert.Left = btnStopPreview.Width + btnStopPreview.Location.X + 15;
           
            btnReset.Height = 23;
            btnReset.Top = 15;
            btnReset.Left = btnConvert.Width + btnConvert.Location.X + 15;

            //First row height + first row margin + 2nd row margin
            int secondRowYMargin = 23 + 15 + 15;

            lstFreq.Top = secondRowYMargin;
            lstDurations.Top = secondRowYMargin;
            rtbArduinoCode.Top = secondRowYMargin;

            //Additional bottom margin of 15
            int secondRowHeight = this.Height - secondRowYMargin - (15 * 3);

            lstFreq.Height = secondRowHeight;
            lstDurations.Height = secondRowHeight;
            rtbArduinoCode.Height = secondRowHeight;

            //Form width - margins
            int FormWidth = this.Width - (5 * 15);

            lstFreq.Width = (FormWidth / 4); //25%
            lstDurations.Width = (FormWidth / 4); //25%
            rtbArduinoCode.Width = (FormWidth / 2); //50%

            //Left Margin 15 plus left element width
            lstFreq.Left = 15;
            lstDurations.Left = (15 * 2) + lstFreq.Width;
            rtbArduinoCode.Left = (15 * 3) + lstFreq.Width + lstDurations.Width;

            //Test output of form width
            //rtbArduinoCode.Text = "Width: " + this.Width + " Height: " + this.Height;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            resizeForm();
        }

        private void rtbArduinoCode_MouseClick(object sender, MouseEventArgs e)
        {
            rtbArduinoCode.SelectAll();
        }

        
    }
}
