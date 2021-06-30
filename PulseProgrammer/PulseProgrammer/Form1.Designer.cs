namespace PulseProgrammer
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
            this.components = new System.ComponentModel.Container();
            PulseProgrammer.Pulse pulse1 = new PulseProgrammer.Pulse();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.loopTimesNUD = new System.Windows.Forms.NumericUpDown();
            this.loopForeverCB = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.deadTimeTB = new System.Windows.Forms.TextBox();
            this.runB = new System.Windows.Forms.Button();
            this.stopB = new System.Windows.Forms.Button();
            this.openPortB = new System.Windows.Forms.Button();
            this.sp_Poll_Timer = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serialPortsComboBox1 = new SerialPortsComboBox.SerialPortsComboBox();
            this.visualEditor1 = new PulseProgrammer.VisualEditor();
            ((System.ComponentModel.ISupportInitialize)(this.loopTimesNUD)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Loop";
            // 
            // loopTimesNUD
            // 
            this.loopTimesNUD.Location = new System.Drawing.Point(50, 34);
            this.loopTimesNUD.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.loopTimesNUD.Name = "loopTimesNUD";
            this.loopTimesNUD.Size = new System.Drawing.Size(56, 20);
            this.loopTimesNUD.TabIndex = 2;
            this.loopTimesNUD.ValueChanged += new System.EventHandler(this.loopTimesNUD_ValueChanged);
            // 
            // loopForeverCB
            // 
            this.loopForeverCB.AutoSize = true;
            this.loopForeverCB.Location = new System.Drawing.Point(112, 35);
            this.loopForeverCB.Name = "loopForeverCB";
            this.loopForeverCB.Size = new System.Drawing.Size(85, 17);
            this.loopForeverCB.TabIndex = 3;
            this.loopForeverCB.Text = "Run Forever";
            this.loopForeverCB.UseVisualStyleBackColor = true;
            this.loopForeverCB.CheckedChanged += new System.EventHandler(this.loopForeverCB_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dead Time";
            // 
            // deadTimeTB
            // 
            this.deadTimeTB.Location = new System.Drawing.Point(269, 33);
            this.deadTimeTB.Name = "deadTimeTB";
            this.deadTimeTB.Size = new System.Drawing.Size(100, 20);
            this.deadTimeTB.TabIndex = 4;
            this.deadTimeTB.Text = "10ms";
            this.deadTimeTB.TextChanged += new System.EventHandler(this.deadTimeTB_TextChanged);
            this.deadTimeTB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.deadTimeTB_KeyDown);
            this.deadTimeTB.Leave += new System.EventHandler(this.deadTimeTB_Leave);
            // 
            // runB
            // 
            this.runB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.runB.Location = new System.Drawing.Point(732, 401);
            this.runB.Name = "runB";
            this.runB.Size = new System.Drawing.Size(75, 23);
            this.runB.TabIndex = 5;
            this.runB.Text = "Run";
            this.runB.UseVisualStyleBackColor = true;
            this.runB.Click += new System.EventHandler(this.runB_Click);
            // 
            // stopB
            // 
            this.stopB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stopB.Location = new System.Drawing.Point(651, 401);
            this.stopB.Name = "stopB";
            this.stopB.Size = new System.Drawing.Size(75, 23);
            this.stopB.TabIndex = 5;
            this.stopB.Text = "Stop";
            this.stopB.UseVisualStyleBackColor = true;
            this.stopB.Click += new System.EventHandler(this.stopB_Click);
            // 
            // openPortB
            // 
            this.openPortB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.openPortB.Location = new System.Drawing.Point(234, 401);
            this.openPortB.Name = "openPortB";
            this.openPortB.Size = new System.Drawing.Size(75, 23);
            this.openPortB.TabIndex = 5;
            this.openPortB.Text = "Connect";
            this.openPortB.UseVisualStyleBackColor = true;
            this.openPortB.Click += new System.EventHandler(this.openPortB_Click);
            // 
            // sp_Poll_Timer
            // 
            this.sp_Poll_Timer.Interval = 30;
            this.sp_Poll_Timer.Tick += new System.EventHandler(this.sp_Poll_Timer_Tick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(819, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveasToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveToolStripMenuItem.Text = "&Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveasToolStripMenuItem
            // 
            this.saveasToolStripMenuItem.Name = "saveasToolStripMenuItem";
            this.saveasToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.saveasToolStripMenuItem.Text = "Save &as";
            this.saveasToolStripMenuItem.Click += new System.EventHandler(this.saveasToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // serialPortsComboBox1
            // 
            this.serialPortsComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.serialPortsComboBox1.FormattingEnabled = true;
            this.serialPortsComboBox1.Items.AddRange(new object[] {
            "com0com - bus for serial port pair emulator 1 (COM33 <-> COM34)",
            "com0com - serial port emulator (COM32)",
            "com0com - serial port emulator CNCB1 (COM34)",
            "Communications Port (COM1)",
            "Intel(R) Active Management Technology - SOL (COM4)",
            "com0com - serial port emulator (COM31)",
            "com0com - serial port emulator CNCA1 (COM33)"});
            this.serialPortsComboBox1.Location = new System.Drawing.Point(15, 403);
            this.serialPortsComboBox1.Name = "serialPortsComboBox1";
            this.serialPortsComboBox1.Size = new System.Drawing.Size(213, 21);
            this.serialPortsComboBox1.TabIndex = 6;
            this.serialPortsComboBox1.Text = "Communications Port (COM1)";
            // 
            // visualEditor1
            // 
            this.visualEditor1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.visualEditor1.Location = new System.Drawing.Point(12, 59);
            this.visualEditor1.Name = "visualEditor1";
            pulse1.Channel = PulseProgrammer.Channel.Tx;
            pulse1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            new PulseProgrammer.PulseProgram().Add(pulse1);
            this.visualEditor1.ScrollBarWidth = 13F;
            this.visualEditor1.Size = new System.Drawing.Size(795, 337);
            this.visualEditor1.TabIndex = 0;
            this.visualEditor1.TabStop = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 430);
            this.Controls.Add(this.serialPortsComboBox1);
            this.Controls.Add(this.openPortB);
            this.Controls.Add(this.stopB);
            this.Controls.Add(this.runB);
            this.Controls.Add(this.deadTimeTB);
            this.Controls.Add(this.loopForeverCB);
            this.Controls.Add(this.loopTimesNUD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.visualEditor1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(499, 300);
            this.Name = "Form1";
            this.Text = "NMR Pulse Programmer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.loopTimesNUD)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VisualEditor visualEditor1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown loopTimesNUD;
        private System.Windows.Forms.CheckBox loopForeverCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox deadTimeTB;
        private System.Windows.Forms.Button runB;
        private System.Windows.Forms.Button stopB;
        private SerialPortsComboBox.SerialPortsComboBox serialPortsComboBox1;
        private System.Windows.Forms.Button openPortB;
        private System.Windows.Forms.Timer sp_Poll_Timer;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveasToolStripMenuItem;
    }
}

