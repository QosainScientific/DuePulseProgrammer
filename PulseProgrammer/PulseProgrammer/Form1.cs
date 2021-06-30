using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace PulseProgrammer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void loopForeverCB_CheckedChanged(object sender, EventArgs e)
        {
            if (loopForeverCB.Checked)
                loopTimesNUD.Value = 0;
            loopTimesNUD.Enabled = !loopForeverCB.Checked;
        }

        private void loopTimesNUD_ValueChanged(object sender, EventArgs e)
        {
            visualEditor1.Program.Repeat = (int)loopTimesNUD.Value;
            visualEditor1.Invalidate();
        }

        private void deadTimeTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                deadTimeTB_Leave(this, e);
            }
        }

        private void deadTimeTB_Leave(object sender, EventArgs e)
        {
            try
            {
                visualEditor1.Program.DeadTime = deadTimeTB.Text;
                if (visualEditor1.Program.DeadTime < 4.7e-6)
                {
                    visualEditor1.Program.DeadTime.Value = 4.7;
                    visualEditor1.Program.DeadTime.Prefix = Prefix.micro;
                }
                deadTimeTB.Text = visualEditor1.Program.DeadTime.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                deadTimeTB.Focus();
            }
        }

        SerialPort sp;
        private void openPortB_Click(object sender, EventArgs e)
        {
            if (sp != null)
            {
                if (sp.IsOpen)
                {
                    sp_Poll_Timer.Enabled = false;
                    try
                    { sp.Close(); }
                    catch { }
                    openPortB.Text = "Open";
                    return;
                }
            }
            string v = Utils.AddPrefix(1);
            sp = new SerialPort(serialPortsComboBox1.SelectedPort, 115200);

            try
            {
                sp.Open();
                sp_Poll_Timer.Enabled = true;
                openPortB.Text = "Disconnect";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void sp_Poll_Timer_Tick(object sender, EventArgs e)
        {
            if (sp.BytesToRead == 0)
                return;
            PacketCommand pc = new PacketCommand();
            var pe = PacketCommand.FromStream(ref pc, sp);
            if (pe == ProtocolError.None)
            {
                HandleCommand(pc);
            }
        }

        private void runB_Click(object sender, EventArgs e)
        {
            runB.BackColor = Color.Red;
            Application.DoEvents();
            if (!SendCom("stop", 3, 10000))
                ;
            for (int i = 0; i < visualEditor1.Program.Count; i++)
            {
                if (!SendCom("add " + i + " " + visualEditor1.Program[i].ToControllerString()))
                    return;
                else
                    ;
            }
            if (!SendCom("count " + visualEditor1.Program.Count))
                ;


            try
            {
                visualEditor1.Program.DeadTime = deadTimeTB.Text;
                deadTimeTB.Text = visualEditor1.Program.DeadTime.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                deadTimeTB.Focus();
            }

            if (!SendCom("deadtime " + Utils.dueDeadTimeDelay(visualEditor1.Program.DeadTime)))
                ;

            if (!SendCom("repeat " + visualEditor1.Program.Repeat))
                ;
            if (!SendCom("run"))
                ;
            runB.BackColor = Color.White;
        }
        void HandleCommand(PacketCommand pc)
        {
        }
        int cid = 0;
        bool SendCom(string command, int retries = 1, int timeout = 1000)
        {
            if (retries == 0)
                return false;
            cid++;
            if (sp == null) return false;
            if (!sp.IsOpen) return false;
            var pc = new PacketCommand();
            pc.PayLoadString = cid + " " + command;
            pc.SendCommand(sp);
            while (true)
            {
                PacketCommand resp = new PacketCommand();
                var pe = PacketCommand.FromStream(ref resp, sp, timeout);
                if (pe == ProtocolError.None)
                {
                    if (resp.PacketID == TCPCommandID.FB && resp.PayLoadString.StartsWith(cid.ToString()))
                        return true;
                    else
                        HandleCommand(resp);
                }
                else
                    return SendCom(command, retries - 1);
            }

        }

        private void stopB_Click(object sender, EventArgs e)
        {
            if (!SendCom("stop"))
                ;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sfd.Filter = "Text Script (*.txt)|*.txt|Any file (*.*)|*.*"; 
            ofd.Filter = "Text Script (*.txt)|*.txt|Any file (*.*)|*.*";

            if (System.IO.File.Exists("default.ppp"))
            {
                Text = "NMR Pulse Programmer -- default.ppp";
                visualEditor1.LoadFile("default.ppp");
                deadTimeTB.Text = visualEditor1.Program.DeadTime.ToString();
            }
        }

        OpenFileDialog ofd = new OpenFileDialog();
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd.FileName = "";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                sfd.FileName = ofd.FileName;
                visualEditor1.LoadFile(ofd.FileName);
                deadTimeTB.Text = visualEditor1.Program.DeadTime.ToString();
                Text = "NMR Pulse Programmer -- " + sfd.FileName;
            }
        }

        SaveFileDialog sfd = new SaveFileDialog();
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sfd.FileName == "")
            {
                if (sfd.ShowDialog() != DialogResult.OK)
                    sfd.FileName = "";
            }
            if (sfd.FileName == "")
                return;
            System.IO.File.WriteAllText(sfd.FileName, visualEditor1.getSaveFileString());
        }

        private void saveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sfd.FileName = "";
                if (sfd.ShowDialog() != DialogResult.OK)
                return;
        
            if (sfd.FileName == "")
                return;
            System.IO.File.WriteAllText(sfd.FileName, string.Join("\r\n", visualEditor1.Program));
        }

        private void deadTimeTB_TextChanged(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sp != null)
            {
                if (sp.IsOpen)
                    if (!SendCom("stop"))
                        ;
            }
        }
    }
}
