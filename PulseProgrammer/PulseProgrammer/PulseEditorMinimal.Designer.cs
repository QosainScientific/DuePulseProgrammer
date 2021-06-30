namespace PulseProgrammer
{
    partial class PulseEditorMinimal
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
            this.ch2RB = new System.Windows.Forms.RadioButton();
            this.ch1RB = new System.Windows.Forms.RadioButton();
            this.okB = new System.Windows.Forms.Button();
            this.cancelB = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.heightTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.offsetTB = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.widthTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ch2RB
            // 
            this.ch2RB.AutoSize = true;
            this.ch2RB.Location = new System.Drawing.Point(160, 96);
            this.ch2RB.Name = "ch2RB";
            this.ch2RB.Size = new System.Drawing.Size(38, 17);
            this.ch2RB.TabIndex = 18;
            this.ch2RB.Text = "Rx";
            this.ch2RB.UseVisualStyleBackColor = true;
            // 
            // ch1RB
            // 
            this.ch1RB.AutoSize = true;
            this.ch1RB.Checked = true;
            this.ch1RB.Location = new System.Drawing.Point(60, 96);
            this.ch1RB.Name = "ch1RB";
            this.ch1RB.Size = new System.Drawing.Size(37, 17);
            this.ch1RB.TabIndex = 19;
            this.ch1RB.TabStop = true;
            this.ch1RB.Text = "Tx";
            this.ch1RB.UseVisualStyleBackColor = true;
            // 
            // okB
            // 
            this.okB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okB.Location = new System.Drawing.Point(141, 127);
            this.okB.Name = "okB";
            this.okB.Size = new System.Drawing.Size(33, 23);
            this.okB.TabIndex = 16;
            this.okB.Text = "OK";
            this.okB.UseVisualStyleBackColor = true;
            // 
            // cancelB
            // 
            this.cancelB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelB.Location = new System.Drawing.Point(180, 127);
            this.cancelB.Name = "cancelB";
            this.cancelB.Size = new System.Drawing.Size(53, 23);
            this.cancelB.TabIndex = 17;
            this.cancelB.Text = "Cancel";
            this.cancelB.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Channel";
            // 
            // heightTB
            // 
            this.heightTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.heightTB.Location = new System.Drawing.Point(60, 67);
            this.heightTB.Name = "heightTB";
            this.heightTB.Size = new System.Drawing.Size(173, 20);
            this.heightTB.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Height";
            // 
            // offsetTB
            // 
            this.offsetTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.offsetTB.Location = new System.Drawing.Point(60, 41);
            this.offsetTB.Name = "offsetTB";
            this.offsetTB.Size = new System.Drawing.Size(173, 20);
            this.offsetTB.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Offset";
            // 
            // widthTB
            // 
            this.widthTB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.widthTB.Location = new System.Drawing.Point(60, 15);
            this.widthTB.Name = "widthTB";
            this.widthTB.Size = new System.Drawing.Size(173, 20);
            this.widthTB.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Width";
            // 
            // PulseEditorMinimal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ch2RB);
            this.Controls.Add(this.ch1RB);
            this.Controls.Add(this.okB);
            this.Controls.Add(this.cancelB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.heightTB);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.offsetTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.widthTB);
            this.Controls.Add(this.label1);
            this.Name = "PulseEditorMinimal";
            this.Size = new System.Drawing.Size(248, 166);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton ch2RB;
        private System.Windows.Forms.RadioButton ch1RB;
        public System.Windows.Forms.Button okB;
        public System.Windows.Forms.Button cancelB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox heightTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox offsetTB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox widthTB;
        private System.Windows.Forms.Label label1;
    }
}
