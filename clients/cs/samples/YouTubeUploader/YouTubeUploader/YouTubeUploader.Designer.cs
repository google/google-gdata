namespace YouTubeUploader
{
    partial class YouTubeUploader
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
            this.components = new System.ComponentModel.Container();
            this.chooseCSVFile = new System.Windows.Forms.OpenFileDialog();
            this.OpenCSVFile = new System.Windows.Forms.Button();
            this.ChooseOutputFile = new System.Windows.Forms.Button();
            this.Upload = new System.Windows.Forms.Button();
            this.csvDisplayGrid = new System.Windows.Forms.DataGridView();
            this.saveOutput = new System.Windows.Forms.SaveFileDialog();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.MaxQueue = new System.Windows.Forms.NumericUpDown();
            this.ChunkSize = new System.Windows.Forms.NumericUpDown();
            this.automaticRetries = new System.Windows.Forms.NumericUpDown();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.CancelAsync = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RetryTimer = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.csvDisplayGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxQueue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChunkSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.automaticRetries)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chooseCSVFile
            // 
            this.chooseCSVFile.FileName = "chooseCSVFile";
            this.chooseCSVFile.Filter = "CSV Files (*.csv) | *.csv";
            // 
            // OpenCSVFile
            // 
            this.OpenCSVFile.Location = new System.Drawing.Point(18, 37);
            this.OpenCSVFile.Name = "OpenCSVFile";
            this.OpenCSVFile.Size = new System.Drawing.Size(140, 27);
            this.OpenCSVFile.TabIndex = 4;
            this.OpenCSVFile.Text = "Open CSV File...";
            this.toolTip1.SetToolTip(this.OpenCSVFile, "Opens an exported CSV file");
            this.OpenCSVFile.UseVisualStyleBackColor = true;
            this.OpenCSVFile.Click += new System.EventHandler(this.OpenCSVFile_Click);
            // 
            // ChooseOutputFile
            // 
            this.ChooseOutputFile.Enabled = false;
            this.ChooseOutputFile.Location = new System.Drawing.Point(633, 39);
            this.ChooseOutputFile.Name = "ChooseOutputFile";
            this.ChooseOutputFile.Size = new System.Drawing.Size(137, 27);
            this.ChooseOutputFile.TabIndex = 7;
            this.ChooseOutputFile.Text = "Save Output...";
            this.ChooseOutputFile.UseVisualStyleBackColor = true;
            this.ChooseOutputFile.Click += new System.EventHandler(this.ChooseOutputFile_Click);
            // 
            // Upload
            // 
            this.Upload.Enabled = false;
            this.Upload.Location = new System.Drawing.Point(164, 37);
            this.Upload.Name = "Upload";
            this.Upload.Size = new System.Drawing.Size(153, 27);
            this.Upload.TabIndex = 5;
            this.Upload.Text = "Start Upload";
            this.Upload.UseVisualStyleBackColor = true;
            this.Upload.Click += new System.EventHandler(this.Upload_Click);
            // 
            // csvDisplayGrid
            // 
            this.csvDisplayGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.csvDisplayGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.csvDisplayGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.csvDisplayGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.csvDisplayGrid.Location = new System.Drawing.Point(0, 0);
            this.csvDisplayGrid.Name = "csvDisplayGrid";
            this.csvDisplayGrid.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.csvDisplayGrid.RowTemplate.Height = 24;
            this.csvDisplayGrid.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.csvDisplayGrid.Size = new System.Drawing.Size(945, 385);
            this.csvDisplayGrid.TabIndex = 10;
            this.csvDisplayGrid.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.csvDisplayGrid_CellMouseEnter);
            // 
            // MaxQueue
            // 
            this.MaxQueue.Location = new System.Drawing.Point(229, 0);
            this.MaxQueue.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.MaxQueue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxQueue.Name = "MaxQueue";
            this.MaxQueue.Size = new System.Drawing.Size(88, 22);
            this.MaxQueue.TabIndex = 1;
            this.toolTip1.SetToolTip(this.MaxQueue, "Defines the number of files we try to upload at the same time");
            this.MaxQueue.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // ChunkSize
            // 
            this.ChunkSize.Location = new System.Drawing.Point(526, 0);
            this.ChunkSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ChunkSize.Name = "ChunkSize";
            this.ChunkSize.Size = new System.Drawing.Size(74, 22);
            this.ChunkSize.TabIndex = 2;
            this.toolTip1.SetToolTip(this.ChunkSize, "Defines the size of the upload in Megabytes that we are trying to upload at once." +
                    " If you have a 1000 MB file, e.g, and this is the default 25MB, the file will be" +
                    " uploaded in 40 parts");
            this.ChunkSize.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // automaticRetries
            // 
            this.automaticRetries.Location = new System.Drawing.Point(840, 3);
            this.automaticRetries.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.automaticRetries.Name = "automaticRetries";
            this.automaticRetries.Size = new System.Drawing.Size(88, 22);
            this.automaticRetries.TabIndex = 3;
            this.toolTip1.SetToolTip(this.automaticRetries, "Defines the number of files we try to upload at the same time");
            this.automaticRetries.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(811, 42);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(103, 17);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Documentation";
            this.toolTip1.SetToolTip(this.linkLabel1, "Opens up the wiki page documenting this tool");
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // CancelAsync
            // 
            this.CancelAsync.Location = new System.Drawing.Point(526, 39);
            this.CancelAsync.Name = "CancelAsync";
            this.CancelAsync.Size = new System.Drawing.Size(78, 27);
            this.CancelAsync.TabIndex = 6;
            this.CancelAsync.Text = "Cancel Upload";
            this.CancelAsync.UseVisualStyleBackColor = true;
            this.CancelAsync.Visible = false;
            this.CancelAsync.Click += new System.EventHandler(this.CancelAsync_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Number of simultanous uplads:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(362, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(154, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Upload part size in MB:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(630, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 17);
            this.label3.TabIndex = 10;
            this.label3.Text = "Automatic retries on failure";
            // 
            // RetryTimer
            // 
            this.RetryTimer.Interval = 6000;
            this.RetryTimer.Tick += new System.EventHandler(this.RetryTimer_Tick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.automaticRetries);
            this.panel1.Controls.Add(this.CancelAsync);
            this.panel1.Controls.Add(this.ChunkSize);
            this.panel1.Controls.Add(this.Upload);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ChooseOutputFile);
            this.panel1.Controls.Add(this.MaxQueue);
            this.panel1.Controls.Add(this.OpenCSVFile);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 388);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(945, 78);
            this.panel1.TabIndex = 12;
            // 
            // YouTubeUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(945, 466);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.csvDisplayGrid);
            this.Name = "YouTubeUploader";
            this.Text = "YouTubeUploader";
            this.Load += new System.EventHandler(this.YouTubeUploader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.csvDisplayGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxQueue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChunkSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.automaticRetries)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog chooseCSVFile;
        private System.Windows.Forms.Button OpenCSVFile;
        private System.Windows.Forms.Button ChooseOutputFile;
        private System.Windows.Forms.Button Upload;
        private System.Windows.Forms.DataGridView csvDisplayGrid;
        private System.Windows.Forms.SaveFileDialog saveOutput;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button CancelAsync;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown MaxQueue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ChunkSize;
        private System.Windows.Forms.NumericUpDown automaticRetries;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer RetryTimer;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}

