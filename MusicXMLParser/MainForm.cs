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
        HelperContainer helper = new HelperContainer(); //used to store dict of frequncies
        List<Note> notes = new List<Note>(); //used to store the notes

        public FormMain()
        {
            InitializeComponent();
        }        

        public void LoadMXL(string xml)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            
            XmlNode node = xmlDoc.DocumentElement.SelectSingleNode("/score-partwise/part");
           
            double divisions = Convert.ToDouble(xmlDoc.DocumentElement.SelectSingleNode("/score-partwise/part/measure/attributes/divisions").InnerText.Trim(' '));

            double tempo;

            if (xmlDoc.DocumentElement.SelectSingleNode("/score-partwise/part/measure/direction/sound").Attributes["tempo"].Value != null)
            {
                tempo = Math.Round(Convert.ToDouble(xmlDoc.DocumentElement.SelectSingleNode("/score-partwise/part/measure/direction/sound").Attributes["tempo"].Value.Trim(' ')));
            }
            else
            {
                tempo = 30;
            }
                        
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

                lstFreq.Items.Add(n.frequency);
                lstDurations.Items.Add(n.duration);
            }
        }


        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            
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
            if (notes.Count == 0)
            {
                MessageBox.Show("There are no notes to play");
                return;
            }

            foreach (Note note in notes)
            {
                if (note.frequency == 0)
                {
                    Thread.Sleep(note.duration);
                }
                else
                {
                    Console.Beep(note.frequency, note.duration);
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
    }
}
