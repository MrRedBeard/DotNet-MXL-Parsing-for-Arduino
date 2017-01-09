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
            this.labelOpenedFile = new System.Windows.Forms.Label();
            this.textBoxNotes = new System.Windows.Forms.TextBox();
            this.numericUpDownLimiter = new System.Windows.Forms.NumericUpDown();
            this.labelLimit = new System.Windows.Forms.Label();
            this.textBoxDataHolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLimiter)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(13, 13);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenFile.TabIndex = 3;
            this.buttonOpenFile.Text = "Open MXL";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // labelOpenedFile
            // 
            this.labelOpenedFile.AutoSize = true;
            this.labelOpenedFile.Location = new System.Drawing.Point(111, 22);
            this.labelOpenedFile.Name = "labelOpenedFile";
            this.labelOpenedFile.Size = new System.Drawing.Size(0, 13);
            this.labelOpenedFile.TabIndex = 4;
            // 
            // textBoxNotes
            // 
            this.textBoxNotes.Location = new System.Drawing.Point(12, 69);
            this.textBoxNotes.Multiline = true;
            this.textBoxNotes.Name = "textBoxNotes";
            this.textBoxNotes.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxNotes.Size = new System.Drawing.Size(650, 349);
            this.textBoxNotes.TabIndex = 5;
            // 
            // numericUpDownLimiter
            // 
            this.numericUpDownLimiter.Increment = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.numericUpDownLimiter.Location = new System.Drawing.Point(55, 43);
            this.numericUpDownLimiter.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownLimiter.Name = "numericUpDownLimiter";
            this.numericUpDownLimiter.Size = new System.Drawing.Size(56, 20);
            this.numericUpDownLimiter.TabIndex = 6;
            this.numericUpDownLimiter.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
            this.numericUpDownLimiter.ValueChanged += new System.EventHandler(this.numericUpDownLimiter_ValueChanged);
            // 
            // labelLimit
            // 
            this.labelLimit.AutoSize = true;
            this.labelLimit.Location = new System.Drawing.Point(12, 46);
            this.labelLimit.Name = "labelLimit";
            this.labelLimit.Size = new System.Drawing.Size(37, 13);
            this.labelLimit.TabIndex = 7;
            this.labelLimit.Text = "Limiter";
            // 
            // textBoxDataHolder
            // 
            this.textBoxDataHolder.Location = new System.Drawing.Point(269, 43);
            this.textBoxDataHolder.Multiline = true;
            this.textBoxDataHolder.Name = "textBoxDataHolder";
            this.textBoxDataHolder.Size = new System.Drawing.Size(100, 20);
            this.textBoxDataHolder.TabIndex = 8;
            this.textBoxDataHolder.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(114, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Notes";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 415);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDataHolder);
            this.Controls.Add(this.labelLimit);
            this.Controls.Add(this.numericUpDownLimiter);
            this.Controls.Add(this.textBoxNotes);
            this.Controls.Add(this.labelOpenedFile);
            this.Controls.Add(this.buttonOpenFile);
            this.Name = "FormMain";
            this.Text = "Convert MXL to Note List for Arduino";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLimiter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialogMXL;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Label labelOpenedFile;
        private System.Windows.Forms.TextBox textBoxNotes;
        private System.Windows.Forms.NumericUpDown numericUpDownLimiter;
        private System.Windows.Forms.Label labelLimit;
        private System.Windows.Forms.TextBox textBoxDataHolder;
        private System.Windows.Forms.Label label1;
    }
}

