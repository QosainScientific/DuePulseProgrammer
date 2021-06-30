namespace PulseProgrammer
{
    partial class PulseEditor
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.widthTB = new System.Windows.Forms.TextBox();
            this.cancelB = new System.Windows.Forms.Button();
            this.okB = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.offsetTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.heightTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dac2RB = new System.Windows.Forms.RadioButton();
            this.pinRB = new System.Windows.Forms.RadioButton();
            this.pinNumCB = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.colorB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width";
            // 
            // widthTB
            // 
            this.widthTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.widthTB.Location = new System.Drawing.Point(59, 3);
            this.widthTB.Name = "widthTB";
            this.widthTB.Size = new System.Drawing.Size(173, 20);
            this.widthTB.TabIndex = 0;
            this.widthTB.TextChanged += new System.EventHandler(this.widthTB_TextChanged);
            // 
            // cancelB
            // 
            this.cancelB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelB.Location = new System.Drawing.Point(179, 132);
            this.cancelB.Name = "cancelB";
            this.cancelB.Size = new System.Drawing.Size(53, 23);
            this.cancelB.TabIndex = 4;
            this.cancelB.Text = "Cancel";
            this.cancelB.UseVisualStyleBackColor = true;
            // 
            // okB
            // 
            this.okB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okB.Location = new System.Drawing.Point(140, 132);
            this.okB.Name = "okB";
            this.okB.Size = new System.Drawing.Size(33, 23);
            this.okB.TabIndex = 3;
            this.okB.Text = "OK";
            this.okB.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Offset";
            // 
            // offsetTB
            // 
            this.offsetTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.offsetTB.Location = new System.Drawing.Point(59, 29);
            this.offsetTB.Name = "offsetTB";
            this.offsetTB.Size = new System.Drawing.Size(173, 20);
            this.offsetTB.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Height";
            // 
            // heightTB
            // 
            this.heightTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.heightTB.Location = new System.Drawing.Point(59, 55);
            this.heightTB.Name = "heightTB";
            this.heightTB.Size = new System.Drawing.Size(173, 20);
            this.heightTB.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 84);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Channel";
            // 
            // dac2RB
            // 
            this.dac2RB.AutoSize = true;
            this.dac2RB.Checked = true;
            this.dac2RB.Location = new System.Drawing.Point(59, 84);
            this.dac2RB.Name = "dac2RB";
            this.dac2RB.Size = new System.Drawing.Size(53, 17);
            this.dac2RB.TabIndex = 5;
            this.dac2RB.TabStop = true;
            this.dac2RB.Text = "DAC2";
            this.dac2RB.UseVisualStyleBackColor = true;
            // 
            // pinRB
            // 
            this.pinRB.AutoSize = true;
            this.pinRB.Location = new System.Drawing.Point(125, 84);
            this.pinRB.Name = "pinRB";
            this.pinRB.Size = new System.Drawing.Size(40, 17);
            this.pinRB.TabIndex = 5;
            this.pinRB.Text = "Pin";
            this.pinRB.UseVisualStyleBackColor = true;
            this.pinRB.CheckedChanged += new System.EventHandler(this.pinRB_CheckedChanged);
            // 
            // pinNumCB
            // 
            this.pinNumCB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pinNumCB.Enabled = false;
            this.pinNumCB.FormattingEnabled = true;
            this.pinNumCB.Items.AddRange(new object[] {
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "50",
            "51",
            "52",
            "53",
            "A0",
            "A1",
            "A2",
            "A3",
            "A4",
            "A5",
            "A6",
            "A7",
            "A8",
            "A9",
            "A10",
            "A11"});
            this.pinNumCB.Location = new System.Drawing.Point(171, 82);
            this.pinNumCB.Name = "pinNumCB";
            this.pinNumCB.Size = new System.Drawing.Size(61, 21);
            this.pinNumCB.TabIndex = 6;
            this.pinNumCB.SelectedIndexChanged += new System.EventHandler(this.pinNumCB_SelectedIndexChanged);
            this.pinNumCB.Click += new System.EventHandler(this.syncPulse_UIevent);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 107);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Color";
            // 
            // colorB
            // 
            this.colorB.FlatAppearance.BorderSize = 0;
            this.colorB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.colorB.Location = new System.Drawing.Point(59, 106);
            this.colorB.Name = "colorB";
            this.colorB.Size = new System.Drawing.Size(75, 18);
            this.colorB.TabIndex = 7;
            this.colorB.UseVisualStyleBackColor = true;
            this.colorB.Click += new System.EventHandler(this.colorB_Click);
            // 
            // PulseEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.colorB);
            this.Controls.Add(this.pinNumCB);
            this.Controls.Add(this.pinRB);
            this.Controls.Add(this.dac2RB);
            this.Controls.Add(this.okB);
            this.Controls.Add(this.cancelB);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.heightTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.offsetTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.widthTB);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(235, 160);
            this.MinimumSize = new System.Drawing.Size(100, 105);
            this.Name = "PulseEditor";
            this.Size = new System.Drawing.Size(235, 160);
            this.Load += new System.EventHandler(this.PulseEditor_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox widthTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox offsetTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox heightTB;
        public System.Windows.Forms.Button cancelB;
        public System.Windows.Forms.Button okB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton dac2RB;
        private System.Windows.Forms.RadioButton pinRB;
        private System.Windows.Forms.ComboBox pinNumCB;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button colorB;
    }
}
