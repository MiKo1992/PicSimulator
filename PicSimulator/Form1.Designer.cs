namespace PicSimulator
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consoleLog = new System.Windows.Forms.TextBox();
            this.consoleLabel = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Sourcecode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.storageGridView = new System.Windows.Forms.DataGridView();
            this.spalte00 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte01 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte02 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte03 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte04 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte05 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte06 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.spalte07 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_nextStep = new System.Windows.Forms.Button();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.supportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stackGridView = new System.Windows.Forms.DataGridView();
            this.stackColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tmr0Label = new System.Windows.Forms.Label();
            this.timeLabel = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label_time = new System.Windows.Forms.Label();
            this.pclLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.pclLabel1 = new System.Windows.Forms.Label();
            this.pcLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.irpLabel = new System.Windows.Forms.Label();
            this.rp1Label = new System.Windows.Forms.Label();
            this.rp0Label = new System.Windows.Forms.Label();
            this.zLabel = new System.Windows.Forms.Label();
            this.toLabel = new System.Windows.Forms.Label();
            this.pdLabel = new System.Windows.Forms.Label();
            this.dcLabel = new System.Windows.Forms.Label();
            this.cLabel = new System.Windows.Forms.Label();
            this.labelirp = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.wLabel = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.storageGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1144, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openToolStripMenuItem.Text = "Open ...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // consoleLog
            // 
            this.consoleLog.Location = new System.Drawing.Point(12, 52);
            this.consoleLog.Multiline = true;
            this.consoleLog.Name = "consoleLog";
            this.consoleLog.ReadOnly = true;
            this.consoleLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.consoleLog.Size = new System.Drawing.Size(269, 231);
            this.consoleLog.TabIndex = 4;
            // 
            // consoleLabel
            // 
            this.consoleLabel.AutoSize = true;
            this.consoleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F);
            this.consoleLabel.Location = new System.Drawing.Point(12, 24);
            this.consoleLabel.Name = "consoleLabel";
            this.consoleLabel.Size = new System.Drawing.Size(123, 25);
            this.consoleLabel.TabIndex = 5;
            this.consoleLabel.Text = "Console Log";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Sourcecode});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Location = new System.Drawing.Point(12, 289);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1039, 267);
            this.dataGridView1.TabIndex = 6;
            // 
            // Sourcecode
            // 
            this.Sourcecode.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Sourcecode.HeaderText = "Sourcecode";
            this.Sourcecode.Name = "Sourcecode";
            this.Sourcecode.ReadOnly = true;
            // 
            // storageGridView
            // 
            this.storageGridView.AllowUserToAddRows = false;
            this.storageGridView.AllowUserToDeleteRows = false;
            this.storageGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.storageGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.spalte00,
            this.spalte01,
            this.spalte02,
            this.spalte03,
            this.spalte04,
            this.spalte05,
            this.spalte06,
            this.spalte07});
            this.storageGridView.Location = new System.Drawing.Point(629, 55);
            this.storageGridView.Name = "storageGridView";
            this.storageGridView.ReadOnly = true;
            this.storageGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.storageGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.storageGridView.Size = new System.Drawing.Size(339, 222);
            this.storageGridView.TabIndex = 10;
            // 
            // spalte00
            // 
            this.spalte00.HeaderText = "00";
            this.spalte00.Name = "spalte00";
            this.spalte00.ReadOnly = true;
            this.spalte00.Width = 30;
            // 
            // spalte01
            // 
            this.spalte01.HeaderText = "01";
            this.spalte01.Name = "spalte01";
            this.spalte01.ReadOnly = true;
            this.spalte01.Width = 30;
            // 
            // spalte02
            // 
            this.spalte02.HeaderText = "02";
            this.spalte02.Name = "spalte02";
            this.spalte02.ReadOnly = true;
            this.spalte02.Width = 30;
            // 
            // spalte03
            // 
            this.spalte03.HeaderText = "03";
            this.spalte03.Name = "spalte03";
            this.spalte03.ReadOnly = true;
            this.spalte03.Width = 30;
            // 
            // spalte04
            // 
            this.spalte04.HeaderText = "04";
            this.spalte04.Name = "spalte04";
            this.spalte04.ReadOnly = true;
            this.spalte04.Width = 30;
            // 
            // spalte05
            // 
            this.spalte05.HeaderText = "05";
            this.spalte05.Name = "spalte05";
            this.spalte05.ReadOnly = true;
            this.spalte05.Width = 30;
            // 
            // spalte06
            // 
            this.spalte06.HeaderText = "06";
            this.spalte06.Name = "spalte06";
            this.spalte06.ReadOnly = true;
            this.spalte06.Width = 30;
            // 
            // spalte07
            // 
            this.spalte07.HeaderText = "07";
            this.spalte07.Name = "spalte07";
            this.spalte07.ReadOnly = true;
            this.spalte07.Width = 30;
            // 
            // btn_nextStep
            // 
            this.btn_nextStep.Location = new System.Drawing.Point(1057, 289);
            this.btn_nextStep.Name = "btn_nextStep";
            this.btn_nextStep.Size = new System.Drawing.Size(77, 23);
            this.btn_nextStep.TabIndex = 11;
            this.btn_nextStep.Text = "Next Step";
            this.btn_nextStep.UseVisualStyleBackColor = true;
            this.btn_nextStep.Click += new System.EventHandler(this.btn_nextStep_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.supportToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // supportToolStripMenuItem
            // 
            this.supportToolStripMenuItem.Name = "supportToolStripMenuItem";
            this.supportToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.supportToolStripMenuItem.Text = "Support";
            this.supportToolStripMenuItem.Click += new System.EventHandler(this.supportToolStripMenuItem_Click);
            // 
            // stackGridView
            // 
            this.stackGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.stackGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stackColumn});
            this.stackGridView.Location = new System.Drawing.Point(983, 55);
            this.stackGridView.Name = "stackGridView";
            this.stackGridView.Size = new System.Drawing.Size(151, 222);
            this.stackGridView.TabIndex = 12;
            // 
            // stackColumn
            // 
            this.stackColumn.HeaderText = "Stack-Value";
            this.stackColumn.Name = "stackColumn";
            this.stackColumn.Width = 105;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tmr0Label);
            this.groupBox1.Controls.Add(this.timeLabel);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label_time);
            this.groupBox1.Controls.Add(this.pclLabel);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.pclLabel1);
            this.groupBox1.Controls.Add(this.pcLabel);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.wLabel);
            this.groupBox1.Location = new System.Drawing.Point(287, 52);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 231);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Spezialregister";
            // 
            // tmr0Label
            // 
            this.tmr0Label.AutoSize = true;
            this.tmr0Label.Location = new System.Drawing.Point(50, 123);
            this.tmr0Label.Name = "tmr0Label";
            this.tmr0Label.Size = new System.Drawing.Size(10, 13);
            this.tmr0Label.TabIndex = 10;
            this.tmr0Label.Text = "-";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(46, 142);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(10, 13);
            this.timeLabel.TabIndex = 12;
            this.timeLabel.Text = "-";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(3, 123);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(40, 13);
            this.label16.TabIndex = 9;
            this.label16.Text = "TMR0:";
            // 
            // label_time
            // 
            this.label_time.AutoSize = true;
            this.label_time.Location = new System.Drawing.Point(3, 142);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(36, 13);
            this.label_time.TabIndex = 11;
            this.label_time.Text = "Time: ";
            // 
            // pclLabel
            // 
            this.pclLabel.AutoSize = true;
            this.pclLabel.Location = new System.Drawing.Point(57, 89);
            this.pclLabel.Name = "pclLabel";
            this.pclLabel.Size = new System.Drawing.Size(10, 13);
            this.pclLabel.TabIndex = 10;
            this.pclLabel.Text = "-";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 105);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 13);
            this.label8.TabIndex = 9;
            this.label8.Text = "PCLATH:";
            // 
            // pclLabel1
            // 
            this.pclLabel1.AutoSize = true;
            this.pclLabel1.Location = new System.Drawing.Point(3, 89);
            this.pclLabel1.Name = "pclLabel1";
            this.pclLabel1.Size = new System.Drawing.Size(30, 13);
            this.pclLabel1.TabIndex = 9;
            this.pclLabel1.Text = "PCL:";
            // 
            // pcLabel
            // 
            this.pcLabel.AutoSize = true;
            this.pcLabel.Location = new System.Drawing.Point(297, 212);
            this.pcLabel.Name = "pcLabel";
            this.pcLabel.Size = new System.Drawing.Size(10, 13);
            this.pcLabel.TabIndex = 6;
            this.pcLabel.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(172, 212);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "Program Counter:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.irpLabel);
            this.groupBox2.Controls.Add(this.rp1Label);
            this.groupBox2.Controls.Add(this.rp0Label);
            this.groupBox2.Controls.Add(this.zLabel);
            this.groupBox2.Controls.Add(this.toLabel);
            this.groupBox2.Controls.Add(this.pdLabel);
            this.groupBox2.Controls.Add(this.dcLabel);
            this.groupBox2.Controls.Add(this.cLabel);
            this.groupBox2.Controls.Add(this.labelirp);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(230, 65);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Status";
            // 
            // irpLabel
            // 
            this.irpLabel.AutoSize = true;
            this.irpLabel.Location = new System.Drawing.Point(16, 39);
            this.irpLabel.Name = "irpLabel";
            this.irpLabel.Size = new System.Drawing.Size(10, 13);
            this.irpLabel.TabIndex = 4;
            this.irpLabel.Text = "-";
            // 
            // rp1Label
            // 
            this.rp1Label.AutoSize = true;
            this.rp1Label.Location = new System.Drawing.Point(50, 40);
            this.rp1Label.Name = "rp1Label";
            this.rp1Label.Size = new System.Drawing.Size(10, 13);
            this.rp1Label.TabIndex = 4;
            this.rp1Label.Text = "-";
            // 
            // rp0Label
            // 
            this.rp0Label.AutoSize = true;
            this.rp0Label.Location = new System.Drawing.Point(81, 40);
            this.rp0Label.Name = "rp0Label";
            this.rp0Label.Size = new System.Drawing.Size(10, 13);
            this.rp0Label.TabIndex = 4;
            this.rp0Label.Text = "-";
            // 
            // zLabel
            // 
            this.zLabel.AutoSize = true;
            this.zLabel.Location = new System.Drawing.Point(164, 40);
            this.zLabel.Name = "zLabel";
            this.zLabel.Size = new System.Drawing.Size(10, 13);
            this.zLabel.TabIndex = 4;
            this.zLabel.Text = "-";
            // 
            // toLabel
            // 
            this.toLabel.AutoSize = true;
            this.toLabel.Location = new System.Drawing.Point(108, 40);
            this.toLabel.Name = "toLabel";
            this.toLabel.Size = new System.Drawing.Size(10, 13);
            this.toLabel.TabIndex = 4;
            this.toLabel.Text = "-";
            // 
            // pdLabel
            // 
            this.pdLabel.AutoSize = true;
            this.pdLabel.Location = new System.Drawing.Point(137, 40);
            this.pdLabel.Name = "pdLabel";
            this.pdLabel.Size = new System.Drawing.Size(10, 13);
            this.pdLabel.TabIndex = 4;
            this.pdLabel.Text = "-";
            // 
            // dcLabel
            // 
            this.dcLabel.AutoSize = true;
            this.dcLabel.Location = new System.Drawing.Point(188, 40);
            this.dcLabel.Name = "dcLabel";
            this.dcLabel.Size = new System.Drawing.Size(10, 13);
            this.dcLabel.TabIndex = 4;
            this.dcLabel.Text = "-";
            // 
            // cLabel
            // 
            this.cLabel.AutoSize = true;
            this.cLabel.Location = new System.Drawing.Point(213, 40);
            this.cLabel.Name = "cLabel";
            this.cLabel.Size = new System.Drawing.Size(10, 13);
            this.cLabel.TabIndex = 4;
            this.cLabel.Text = "-";
            // 
            // labelirp
            // 
            this.labelirp.AutoSize = true;
            this.labelirp.Location = new System.Drawing.Point(9, 16);
            this.labelirp.Name = "labelirp";
            this.labelirp.Size = new System.Drawing.Size(25, 13);
            this.labelirp.TabIndex = 3;
            this.labelirp.Text = "IRP";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(43, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(28, 13);
            this.label9.TabIndex = 3;
            this.label9.Text = "RP1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(103, 17);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 13);
            this.label14.TabIndex = 3;
            this.label14.Text = "TO";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(210, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(14, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "C";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(162, 17);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(14, 13);
            this.label12.TabIndex = 3;
            this.label12.Text = "Z";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(74, 17);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(28, 13);
            this.label15.TabIndex = 3;
            this.label15.Text = "RP0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(182, 17);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "DC";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(131, 17);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(22, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "PD";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 204);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "W-Register";
            // 
            // wLabel
            // 
            this.wLabel.AutoSize = true;
            this.wLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wLabel.Location = new System.Drawing.Point(137, 204);
            this.wLabel.Name = "wLabel";
            this.wLabel.Size = new System.Drawing.Size(16, 24);
            this.wLabel.TabIndex = 1;
            this.wLabel.Text = "-";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1144, 564);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.stackGridView);
            this.Controls.Add(this.btn_nextStep);
            this.Controls.Add(this.storageGridView);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.consoleLabel);
            this.Controls.Add(this.consoleLog);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.storageGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stackGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.TextBox consoleLog;
        private System.Windows.Forms.Label consoleLabel;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sourcecode;
        private System.Windows.Forms.DataGridView storageGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte00;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte01;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte02;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte03;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte04;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte05;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte06;
        private System.Windows.Forms.DataGridViewTextBoxColumn spalte07;
        private System.Windows.Forms.Button btn_nextStep;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem supportToolStripMenuItem;
        private System.Windows.Forms.DataGridView stackGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn stackColumn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label tmr0Label;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Label pclLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label pclLabel1;
        private System.Windows.Forms.Label pcLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label irpLabel;
        private System.Windows.Forms.Label rp1Label;
        private System.Windows.Forms.Label rp0Label;
        private System.Windows.Forms.Label zLabel;
        private System.Windows.Forms.Label toLabel;
        private System.Windows.Forms.Label pdLabel;
        private System.Windows.Forms.Label dcLabel;
        private System.Windows.Forms.Label cLabel;
        private System.Windows.Forms.Label labelirp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label wLabel;
    }
}

