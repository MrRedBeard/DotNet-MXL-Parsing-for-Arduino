using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO.Compression;

namespace MusicXMLParser
{
    /// <summary>
    /// https://github.com/shvelo/musicxml_to_arduino
    /// https://en.wikibooks.org/wiki/XQuery/MusicXML_to_Arduino
    /// </summary>
    public partial class FormMain : Form
    {
        class Notes
        {
            public string voice { get; set; }
            public string noteString { get; set; }
            public string duration { get; set; }
        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        public void LoadMXL(string xml)
        {
            textBoxDataHolder.Text = "";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);

            //// Get elements
            XmlNodeList nodes = xmlDoc.DocumentElement.SelectNodes("/score-partwise/part");
            List<Notes> notes = new List<Notes>();

            double divisions = 4;
            try
            {
                divisions = Convert.ToDouble(xmlDoc.DocumentElement.SelectSingleNode("/score-partwise/part/measure/attributes/divisions").InnerText.Trim(' '));
            }
            catch (Exception) { }

            double tempo = 120;
            try
            {
                tempo = Convert.ToDouble(xmlDoc.DocumentElement.SelectSingleNode("/score-partwise/part/measure/direction/sound").Attributes["tempo"].Value.Trim(' '));
            }
            catch (Exception) { }

            double qnote = 0;
            qnote = 60.0 / tempo / divisions * 1000.0;

            foreach (XmlNode node in nodes)
            {
                XmlNodeList subnodes = node.SelectNodes("measure/note");
                foreach (XmlNode snode in subnodes)
                {
                    Notes n = new Notes();
                    try
                    {
                        n.voice = snode.SelectSingleNode("voice").InnerText;
                        //n.note = snode.SelectSingleNode("pitch").SelectSingleNode("step").InnerText;
                        //n.octave = snode.SelectSingleNode("pitch").SelectSingleNode("octave").InnerText;
                        n.noteString = "NOTE_" + snode.SelectSingleNode("pitch").SelectSingleNode("step").InnerText + snode.SelectSingleNode("pitch").SelectSingleNode("octave").InnerText;
                        switch (snode.SelectSingleNode("type").InnerText)
                        {
                            case "whole":
                                n.duration = Math.Round((qnote * 4), 0).ToString();
                                break;
                            case "half":
                                n.duration = Math.Round((qnote * 2), 0).ToString();
                                break;
                            case "quarter":
                                n.duration = Math.Round((qnote), 0).ToString();
                                break;
                            case "eighth":
                                n.duration = Math.Round((qnote / 2), 0).ToString();
                                break;
                            default:
                                n.duration = Math.Round((qnote * 4), 0).ToString();
                                break;
                        }

                        if (n.duration.Length < 1)
                        {
                            n.duration = "0";
                        }

                        notes.Add(n);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            string voice = ""; //track when change
            string output = "";

            string headerString = "";
            string notesString = "";
            string durationString = "";
            string voiceString = "";
            foreach (Notes no in notes.OrderBy(n => n.voice).ToList<Notes>())
            {
                if (voice == "")
                {
                    voice = no.voice;
                }
                if (voice != no.voice)
                {
                    headerString = Environment.NewLine + Environment.NewLine + Environment.NewLine + "Voice:" + voiceString + Environment.NewLine + Environment.NewLine;
                    notesString = notesString.Substring(0, notesString.Length - 2);
                    durationString = durationString.Substring(0, durationString.Length - 2);
                    output += headerString + notesString + Environment.NewLine + Environment.NewLine + durationString + Environment.NewLine;

                    durationString = "";
                    notesString = "";
                    voice = no.voice;
                }
                voiceString = no.voice;
                notesString += no.noteString + ", ";
                durationString += no.duration + ", ";
            }
            //Only one voice in file
            if (output.Length < 5)
            {
                headerString = Environment.NewLine + Environment.NewLine + Environment.NewLine + "Voice:" + voiceString + Environment.NewLine + Environment.NewLine;
                notesString = notesString.Substring(0, notesString.Length - 1);
                durationString = durationString.Substring(0, durationString.Length - 1);
                output += headerString + notesString + Environment.NewLine + Environment.NewLine + durationString + Environment.NewLine;
            }
            textBoxNotes.Text = output;
            textBoxDataHolder.Text = output;
        }

        private void buttonOpenFile_Click(object sender, EventArgs e)
        {
            numericUpDownLimiter.Value = 400;
            openFileDialogMXL.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialogMXL.Title = "Open MXL";
            //openFileDialogMXL.DefaultExt = "mxl";
            openFileDialogMXL.Filter = "MXL/XML Files (*.xml; *.mxl)|*.xml; *.mxl|All files (*.*)|*.*";
            openFileDialogMXL.CheckFileExists = true;
            openFileDialogMXL.CheckPathExists = true;
            openFileDialogMXL.Multiselect = false;

            if (openFileDialogMXL.ShowDialog() == DialogResult.OK)
            {
                labelOpenedFile.Text = "Opened File: " + openFileDialogMXL.FileName;
                if (openFileDialogMXL.FileName.Length > 1)
                {
                    try
                    {
                        unZipMXL(openFileDialogMXL.FileName);
                    }
                    catch (Exception)
                    {
                        LoadMXL(File.ReadAllText(openFileDialogMXL.FileName));
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

        private void numericUpDownLimiter_ValueChanged(object sender, EventArgs e)
        {
            textBoxNotes.Text = "";
            List<string> ls = new List<string>();
            string outputline = "";
            foreach (string line in textBoxDataHolder.Lines)
            {
                if (line.Contains(','))
                {
                    ls = line.Split(',').ToList<string>();
                    foreach (string item in ls)
                    {
                        outputline += item + ", ";
                        if (outputline.Split(',').Count() >= numericUpDownLimiter.Value)
                        {
                            break;
                        }
                    }
                    outputline = outputline.Replace("  ", " ");
                    outputline = outputline.Substring(0, outputline.Length - 2);
                }
                else
                {
                    outputline = line;
                }
                textBoxNotes.Text += outputline + Environment.NewLine;
                outputline = "";
            }
        }
    }
}
