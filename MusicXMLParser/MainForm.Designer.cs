namespace MusicXMLParser
{
    partial class FormMain
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
            this.openFileDialogMXL = new System.Windows.Forms.OpenFileDialog();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.lstFreq = new System.Windows.Forms.ListBox();
            this.lstDurations = new System.Windows.Forms.ListBox();
            this.rtbArduinoCode = new System.Windows.Forms.RichTextBox();
            this.btnPlayPreview = new System.Windows.Forms.Button();
            this.txtMXLFile = new System.Windows.Forms.TextBox();
            this.btnConvert = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(15, 12);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(92, 23);
            this.buttonOpenFile.TabIndex = 3;
            this.buttonOpenFile.Text = "Open MXL";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // lstFreq
            // 
            this.lstFreq.FormattingEnabled = true;
            this.lstFreq.Location = new System.Drawing.Point(15, 40);
            this.lstFreq.Name = "lstFreq";
            this.lstFreq.Size = new System.Drawing.Size(120, 303);
            this.lstFreq.TabIndex = 10;
            // 
            // lstDurations
            // 
            this.lstDurations.FormattingEnabled = true;
            this.lstDurations.Location = new System.Drawing.Point(159, 40);
            this.lstDurations.Name = "lstDurations";
            this.lstDurations.Size = new System.Drawing.Size(120, 303);
            this.lstDurations.TabIndex = 11;
            // 
            // rtbArduinoCode
            // 
            this.rtbArduinoCode.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbArduinoCode.Location = new System.Drawing.Point(303, 40);
            this.rtbArduinoCode.Name = "rtbArduinoCode";
            this.rtbArduinoCode.ReadOnly = true;
            this.rtbArduinoCode.Size = new System.Drawing.Size(357, 305);
            this.rtbArduinoCode.TabIndex = 12;
            this.rtbArduinoCode.Text = "";
            // 
            // btnPlayPreview
            // 
            this.btnPlayPreview.Location = new System.Drawing.Point(489, 12);
            this.btnPlayPreview.Name = "btnPlayPreview";
            this.btnPlayPreview.Size = new System.Drawing.Size(92, 23);
            this.btnPlayPreview.TabIndex = 14;
            this.btnPlayPreview.Text = "Play Preview";
            this.btnPlayPreview.UseVisualStyleBackColor = true;
            this.btnPlayPreview.Click += new System.EventHandler(this.btnPlayPreview_Click);
            // 
            // txtMXLFile
            // 
            this.txtMXLFile.Location = new System.Drawing.Point(114, 13);
            this.txtMXLFile.Name = "txtMXLFile";
            this.txtMXLFile.ReadOnly = true;
            this.txtMXLFile.Size = new System.Drawing.Size(369, 20);
            this.txtMXLFile.TabIndex = 15;
            // 
            // btnConvert
            // 
            this.btnConvert.Location = new System.Drawing.Point(587, 12);
            this.btnConvert.Name = "btnConvert";
            this.btnConvert.Size = new System.Drawing.Size(73, 23);
            this.btnConvert.TabIndex = 13;
            this.btnConvert.Text = "Convert";
            this.btnConvert.UseVisualStyleBackColor = true;
            this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 356);
            this.Controls.Add(this.txtMXLFile);
            this.Controls.Add(this.btnPlayPreview);
            this.Controls.Add(this.btnConvert);
            this.Controls.Add(this.rtbArduinoCode);
            this.Controls.Add(this.lstDurations);
            this.Controls.Add(this.lstFreq);
            this.Controls.Add(this.buttonOpenFile);
            this.Name = "FormMain";
            this.Text = "Convert MXL to Note List for Arduino";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialogMXL;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.ListBox lstFreq;
        private System.Windows.Forms.ListBox lstDurations;
        private System.Windows.Forms.RichTextBox rtbArduinoCode;
        private System.Windows.Forms.Button btnPlayPreview;
        private System.Windows.Forms.TextBox txtMXLFile;
        private System.Windows.Forms.Button btnConvert;
    }
}

