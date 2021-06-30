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
    public partial class PulseEditorMinimal : UserControl
    {
        public PulseProgram Program;
        public Pulse Pulse { get; set; }
        public Pulse WorkingPulse { get; set; }
        public PulseEditorMinimal()
        {
            this.InitializeComponent();
        }

        public PulseEditorMinimal(Pulse pulse, PulseProgram Program)
        {
            this.Program = Program;
            Pulse = pulse;
            WorkingPulse = Pulse.Clone();
            InitializeComponent();
            widthTB.Text = Pulse.Width.ToString();
            heightTB.Text = Pulse.Height.ToString();
            offsetTB.Text = Pulse.Offset.ToString();
            ch1RB.Checked = pulse.Channel == Channel.Tx;
            ch2RB.Checked = pulse.Channel == Channel.Rx;
        }
        public void SubscribeToEvents()
        {
            this.widthTB.Leave += new System.EventHandler(this.syncPulse_UIevent);
            this.offsetTB.Leave += new System.EventHandler(this.syncPulse_UIevent);
            this.heightTB.Leave += new System.EventHandler(this.syncPulse_UIevent);
            this.ch1RB.CheckedChanged += new System.EventHandler(this.syncPulse_UIevent);
            this.ch2RB.CheckedChanged += new System.EventHandler(this.syncPulse_UIevent);
        }
        public void syncPulse_UIevent(object sender, EventArgs e)
        {
            WorkingPulse = new Pulse();
            WorkingPulse.Height = heightTB.Text;
            WorkingPulse.Width = widthTB.Text;
            WorkingPulse.Channel = ch1RB.Checked ? Channel.Tx : Channel.Rx;
            WorkingPulse.Height = "3.3V";
            WorkingPulse.Offset = offsetTB.Text;
            if (WorkingPulse.Channel == Channel.Tx)
                WorkingPulse.Color = Color.Red;
            else
                WorkingPulse.Color = Color.Blue;
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
    }
}
