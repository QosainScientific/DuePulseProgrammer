using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PulseProgrammer
{
    public partial class PulseEditor : UserControl
    {
        public PulseProgram Program;
        public Pulse Pulse { get; set; }
        public PulseEditor()
        {
            InitializeComponent();
        }
        public void SubscribeToEvents()
        {
            this.widthTB.TextChanged += new System.EventHandler(this.TB_TextChanged);
            this.widthTB.Leave += new System.EventHandler(this.syncPulse_UIevent);
            this.okB.Click += new System.EventHandler(this.okB_Click);
            this.offsetTB.TextChanged += new System.EventHandler(this.TB_TextChanged);
            this.offsetTB.Leave += new System.EventHandler(this.syncPulse_UIevent);
            this.heightTB.TextChanged += new System.EventHandler(this.TB_TextChanged);
            this.heightTB.Leave += new System.EventHandler(this.syncPulse_UIevent);
            this.dac2RB.CheckedChanged += new System.EventHandler(this.syncPulse_UIevent);
            this.pinRB.CheckedChanged += new System.EventHandler(this.syncPulse_UIevent);
            this.dac2RB.CheckedChanged += new System.EventHandler(this.syncPulse_UIevent);
        }

        public PulseEditor(Pulse pulse, PulseProgram Program)
        {
            this.Program = Program;
            Pulse = pulse;
            WorkingPulse = Pulse.Clone();
            InitializeComponent();
            widthTB.Text = Pulse.Width.ToString();
            heightTB.Text = Pulse.Height.ToString();
            offsetTB.Text = Pulse.Offset.ToString();
            colorB.BackColor = Pulse.Color;

            if (pulse.Channel == Channel.DAC2)
                dac2RB.Checked = true;
            else
            {
                pinRB.CheckedChanged -= pinRB_CheckedChanged;
                pinRB.Checked = true;
                pinRB.CheckedChanged += pinRB_CheckedChanged;
                pinNumCB.Enabled = true;
                pinNumCB.Text = pulse.Channel.ToString().Substring(3);
            }

        }

        private void TB_TextChanged(object sender, EventArgs e)
        {
        }

        public Pulse WorkingPulse { get; set; }
        public void syncPulse_UIevent(object sender, EventArgs e)
        {
            pinNumCB.Enabled = pinRB.Checked;
            WorkingPulse = new Pulse();
            WorkingPulse.Height = heightTB.Text;
            WorkingPulse.Width = widthTB.Text;
            WorkingPulse.Channel = dac2RB.Checked ? Channel.DAC2 : (Channel)Enum.Parse(typeof(Channel), "Pin" + pinNumCB.Text);
            if (WorkingPulse.Channel != Channel.DAC2)
                WorkingPulse.Height = "3.3V";
           

            WorkingPulse.Offset = offsetTB.Text;

            if (pinRB.Checked)
            {
                var pulse = Program.ToList().Find(p => p.Channel == WorkingPulse.Channel);
                if (pulse != null)
                {
                    WorkingPulse.Color = pulse.Color;
                    colorB.BackColor = pulse.Color;
                }
            }
            else
            {
                var dac2 = Program.ToList().Find(p => p.Channel == Channel.DAC2);
                if (dac2 != null)
                {
                    WorkingPulse.Color = dac2.Color;
                    colorB.BackColor = dac2.Color;
                }
            }

            if (WorkingPulse.Height.Prefix == Prefix.one)
                WorkingPulse.Height = WorkingPulse.Height.MatchPrefix(Pulse.Height);
            if (WorkingPulse.Width.Prefix == Prefix.one)
                WorkingPulse.Width = WorkingPulse.Width.MatchPrefix(Pulse.Width);
            if (WorkingPulse.Offset.Prefix == Prefix.one)
                WorkingPulse.Offset = WorkingPulse.Offset.MatchPrefix(Pulse.Offset);
            widthTB.Text = WorkingPulse.Width.ToString();
            heightTB.Text = WorkingPulse.Height.ToString();
            offsetTB.Text = WorkingPulse.Offset.ToString();
        }
        private void okB_Click(object sender, EventArgs e)
        {
        }

        private void PulseEditor_Load(object sender, EventArgs e)
        {

        }

        private void pinNumCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Program == null)
                return;
            var pair = Program.ToList().Find(p => p.Channel == WorkingPulse.Channel);
            if (pair != null)
            {
                WorkingPulse.Color = pair.Color;
                colorB.BackColor = pair.Color;
            }
            syncPulse_UIevent(sender, e);
        }

        private void colorB_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            cd.Color = WorkingPulse.Color;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                colorB.BackColor = cd.Color;
                foreach (var pulse in Program)
                    if (pulse.Channel == WorkingPulse.Channel)
                        pulse.Color = cd.Color;
            }
            syncPulse_UIevent(sender, e);
        }

        private void pinRB_CheckedChanged(object sender, EventArgs e)
        {
                syncPulse_UIevent(sender, e);
        }

        private void widthTB_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
