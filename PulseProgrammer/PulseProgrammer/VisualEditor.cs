using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace PulseProgrammer
{
    public class VisualEditor:Panel
    {
        public PulseProgram Program { get; set; } = new PulseProgram();
        public void LoadFile(string file)
        {
            Program.Clear();
            foreach (string str in System.IO.File.ReadAllLines(file))
            {
                try
                {
                    string recordType = str.Substring(0, str.IndexOf(":")).ToLower().Trim(new char[] { ' ' });
                    string recordValue = str.Substring(str.IndexOf(":") + 1).Trim(new char[] { ' ' });
                    if (recordType == "pulse")
                        Program.Add(recordValue);
                    else if (recordType == "programparamter")
                    {
                        recordValue = recordValue.Replace(";", ",");
                        var vSets = recordValue.Replace(" ", "").Split(new char[] { ',' });
                        Pulse g = new Pulse();
                        foreach (string vSet in vSets)
                        {
                            var pair = vSet.Split(new char[] { '=' });
                            if (pair[0] == "xOffsetV")
                                xOffsetV = (float)Convert.ToDouble(pair[1]);
                            else if ((pair[0] == "XPPU"))
                                XPPU = (float)Convert.ToDouble(pair[1]);
                            else if ((pair[0] == "DeadTime"))
                                Program.DeadTime = pair[1];
                        }
                    }
                }
                catch { }
            }
            invalidate = true;
        }
        public string getSaveFileString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var pulse in Program)
                sb.AppendLine("Pulse: " + pulse);
            sb.AppendLine("ProgramParamter: xOffsetV = " + xOffsetV);
            sb.AppendLine("ProgramParamter: XPPU = " + XPPU);;
            sb.AppendLine("ProgramParamter: DeadTime = " + Program.DeadTime);
            return sb.ToString();
        }
        public VisualEditor()
        {
            new Timer() { Interval = 30, Enabled = true }.Tick += VisualEditor_Tick;
            DoubleBuffered = true;
            Program.Add("d = 500us, w = 500us, h = 3.3V, ch = 1, cl = 255.0.0");
            MouseMove += VisualEditor_MouseMove;
            MouseUp += VisualEditor_MouseUp;
            MouseDown += VisualEditor_MouseDown;
        }


        bool invalidate = false;
        private void VisualEditor_Tick(object sender, EventArgs e)
        {
            if (invalidate)
            {
                invalidate = false;
                Invalidate();
            }
        }

        public PointF LastMoveCursorG = new PointF();
        // Cursor Tolerance
        float CST = 2;
        float AddPulseWidth = 15;
        public float ScrollBarWidth { get; set; } = 13;
        PointF LastMoveCursorV { get { return GtoV(LastMoveCursorG); } }
        RectangleF ScrollBarRectangleG
        {
            get
            {
                double totalLengthV = (Program.Sum(k => k.Offset + k.Width) + Program.DeadTime);
                double totalLengthG = totalLengthV * XPPU;
                double shownLengthV = Width / XPPU;
                float widFac = (float)(shownLengthV / totalLengthV);
                float offsetFac = (float)(xOffsetG / totalLengthG);
                return new RectangleF(-Width * offsetFac, 1, Width * widFac, ScrollBarWidth - 1);
            }
        }
        private void VisualEditor_MouseMove(object sender, MouseEventArgs e)
        {
            PointF pG = new PointF(e.X, e.Y);
            PointF pV = GtoV(pG);
            float xoV = 0;
            if (!mouseAction.HasStarted)
            {
                mouseAction = new NullMouseAction();
                if (ScrollBarRectangleG.Contains(e.Location))
                {
                    mouseAction = new PanelMouseAction() { StartedAtG = pG, Action = panelMouseActions.Scroll };
                    Invalidate();
                    return;
                }
                foreach (var pulse in Program)
                {
                    float startXG = XVtoXG(xoV);
                    float midXG = XVtoXG(xoV + pulse.Offset);
                    float endXG = XVtoXG(xoV + pulse.Offset + pulse.Width);
                    float pulseYG = YVtoYG(pulse.Height);
                    float baseYG = YVtoYG(0);

                    mouseAction = new PulseMouseAction();
                    // if the comparison is with G, it works like the forms coordinate system.
                    if (pG.X >= startXG - CST && 
                        pG.X <= endXG + CST && 
                        pG.Y < baseYG + CST && 
                        pG.Y > pulseYG - CST)
                    {
                        mouseAction = new PulseMouseAction() { StartedAtG = pG, Pulse = pulse, Action = pulseMouseActions.None };

                        if (pG.X <= midXG + CST) // offset
                        {
                            if (pG.X >= midXG - CST)
                            {
                                ((PulseMouseAction)mouseAction).Action = pulseMouseActions.OffsetAdjust;
                                Cursor = Cursors.SizeWE;
                            }
                            else if (pG.X <= startXG + AddPulseWidth)
                            {
                                ((PulseMouseAction)mouseAction).Action = pulseMouseActions.AddBefore;
                                Cursor = Cursors.Hand;
                            }
                            else
                            {
                                ((PulseMouseAction)mouseAction).Action = pulseMouseActions.HoverOver;
                                Cursor = Cursors.Default;
                            }
                        }
                        else if (pG.X >= endXG - AddPulseWidth) // Width
                        {
                            if (pG.X >= endXG - CST)
                            {
                                ((PulseMouseAction)mouseAction).Action = pulseMouseActions.WidthAdjust;
                                Cursor = Cursors.SizeWE;
                            }
                            else
                            {
                                ((PulseMouseAction)mouseAction).Action = pulseMouseActions.AddAfter;
                                Cursor = Cursors.Hand;
                            }
                        }
                        else if (pG.Y < pulseYG + CST) // height
                        {
                            ((PulseMouseAction)mouseAction).Action = pulseMouseActions.HeightAdjust;
                            Cursor = Cursors.SizeNS;
                        }
                        else
                        {
                            ((PulseMouseAction)mouseAction).Action = pulseMouseActions.HoverOver;
                            Cursor = Cursors.Default;
                        }
                    }

                    xoV += pulse.Offset + (float)pulse.Width;

                    if (((PulseMouseAction)mouseAction).Action == pulseMouseActions.None)
                    {
                        mouseAction = new NullMouseAction();
                    }
                    else
                    {
                        break;
                    }
                }
                if (mouseAction is NullMouseAction)
                    Cursor = Cursors.Default;
                else if (mouseAction is PulseMouseAction)
                    if (((PulseMouseAction)mouseAction).Action == pulseMouseActions.None)
                        Cursor = Cursors.Default;
                Invalidate();
            }
            else // already in an action
            {
                if (mouseAction is PulseMouseAction)
                {
                    var action = (PulseMouseAction)mouseAction;
                    float changeXG = LastMoveCursorG.X - e.X;
                    float changeYG = LastMoveCursorG.Y - e.Y;
                    float changeXV = changeXG / XPPU;
                    float changeYV = changeYG / YPPU;

                    if (action.Action == pulseMouseActions.HeightAdjust || action.Action == pulseMouseActions.Delete)
                    {
                        action.Pulse.Height = ((PrefixedValue)(action.Pulse.Height + changeYV)).MatchPrefix(action.Pulse.Height);
                        action.Pulse.Height.Value = Math.Round(action.Pulse.Height.Value, 3);
                        if (action.Pulse.Height < 0)
                        {
                            action.Action = pulseMouseActions.Delete;
                        }
                        else
                            action.Action = pulseMouseActions.HeightAdjust;
                    } 
                    else if (action.Action == pulseMouseActions.WidthAdjust)
                    {
                        action.Pulse.Width = ((PrefixedValue)(action.Pulse.Width - changeXV)).MatchPrefix(action.Pulse.Width);
                        action.Pulse.Width.Value = Math.Round(action.Pulse.Width.Value, 3);
                    }
                    else if (action.Action == pulseMouseActions.OffsetAdjust)
                    {
                        action.Pulse.Offset = ((PrefixedValue)(action.Pulse.Offset - changeXV)).MatchPrefix(action.Pulse.Offset);
                        action.Pulse.Offset.Value = Math.Round(action.Pulse.Offset.Value, 3);
                    }
                    Invalidate();
                }
                else if (mouseAction is PanelMouseAction)
                {
                    var action = (PanelMouseAction)mouseAction;
                    if (action.Action == panelMouseActions.XYScale || action.Action == panelMouseActions.XScale || action.Action == panelMouseActions.YScale) // not decided which direction to expand
                    {
                        if (action.Action == panelMouseActions.XYScale)
                        {
                            if (Math.Abs(mouseAction.StartedAtG.X - e.X) > Math.Abs(mouseAction.StartedAtG.Y - e.Y)) // its X
                                action.Action = panelMouseActions.XScale;
                            else
                                action.Action = panelMouseActions.YScale;
                        }
                        if (action.Action == panelMouseActions.XScale)
                        {
                            pG = e.Location;
                            pV = GtoV(pG);
                            float addWid = Width * 0.02F * (e.X - LastMoveCursorG.X);
                            float newWid = Width + addWid;
                            float newXPPU = XPPU / Width * newWid;
                            XPPU = newXPPU;
                            var newPV = GtoV(pG);
                            var pVxDiff = newPV.X - pV.X;
                            var pGxDiff = pVxDiff * XPPU;
                            xOffsetG += pGxDiff;
                            Invalidate();
                        }
                        else if (action.Action == panelMouseActions.YScale)
                        {
                            pG = e.Location;
                            pV = GtoV(pG);
                            float addHei = Height * 0.02F * (e.Y - LastMoveCursorG.Y);
                            float newHei = Height + addHei;
                            float newYPPU = YPPU / Height * newHei;
                            YPPU = newYPPU;
                            Invalidate();
                        }
                    }
                    else if (action.Action == panelMouseActions.Pan)
                    {
                        float changeXG = LastMoveCursorG.X - e.X;
                        float changeYG = LastMoveCursorG.Y - e.Y;
                        xOffsetG -= changeXG;
                        Invalidate();
                    }
                    else if (action.Action == panelMouseActions.Scroll)
                    {
                        double totalLengthV = (Program.Sum(k => k.Offset + k.Width) + Program.DeadTime);
                        double totalLengthG = totalLengthV * XPPU;
                        double shownLengthV = Width / XPPU;
                        double widFac = shownLengthV / totalLengthV;
                        double offsetFac = xOffsetG / totalLengthG;
                        double shownOffset = -Width * offsetFac;
                        double newShownOffset = shownOffset + (e.X - LastMoveCursorG.X);
                        double newOffsetFac = newShownOffset / -Width;
                        xOffsetG = (float)(newOffsetFac * totalLengthG);
                        Invalidate();
                    }
                }
            }
            LastMoveCursorG = pG;
        }

        MouseAction mouseAction = new NullMouseAction();
        PulseEditorMinimal pef = null;
        private void VisualEditor_MouseDown(object sender, MouseEventArgs e)
        {
            if (mouseAction.HasStarted)
                return;
            if (e.Button == MouseButtons.Middle)
            {
                mouseAction = new PanelMouseAction() { Action = panelMouseActions.XYScale, StartedAtG = LastMoveCursorG, HasStarted = true };
            }
            else if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                if (mouseAction is PulseMouseAction)
                {
                    mouseAction.StartedAtG = LastMoveCursorG;
                    mouseAction.HasStarted = true;
                }
                else if (mouseAction is PanelMouseAction)
                {
                    if (((PanelMouseAction)mouseAction).Action == panelMouseActions.Scroll)
                        mouseAction.HasStarted = true;
                }
                else if (mouseAction is NullMouseAction && e.Y > ScrollBarWidth)
                {
                    Cursor = Cursors.NoMoveHoriz;
                    mouseAction = new PanelMouseAction() { Action = panelMouseActions.Pan, StartedAtG = LastMoveCursorG, HasStarted = true };
                }
            }
            else if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                if (pef == null)
                {
                    ((PulseMouseAction)mouseAction).Action = pulseMouseActions.EditPulse;
                    ((PulseMouseAction)mouseAction).HasStarted = true;
                    pef = new PulseEditorMinimal(((PulseMouseAction)mouseAction).Pulse, Program);

                    pef.Left = (Width - pef.Width) / 2;
                    pef.Top = (Height - pef.Height) / 2;
                    Invalidate();
                    Application.DoEvents();
                }
            }
        }
        private void VisualEditor_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseAction is PulseMouseAction)
            {
                if (((PulseMouseAction)mouseAction).Action == pulseMouseActions.AddAfter || ((PulseMouseAction)mouseAction).Action == pulseMouseActions.AddBefore)
                {
                    var refPulse = ((PulseMouseAction)mouseAction).Pulse;
                    Pulse p = new Pulse();
                    p.Height = ((PrefixedValue)(Height / YPPU / 4)).MatchPrefix(refPulse.Height);
                    p.Offset = ((PrefixedValue)(Width / XPPU / 10)).MatchPrefix(refPulse.Width);
                    p.Width = ((PrefixedValue)(p.Offset * 2)).MatchPrefix(refPulse.Offset);

                    p.Channel = refPulse.Channel;
                    p.Height.Value = refPulse.Height.Value;
                    p.Color = refPulse.Color;

                    if (p.Channel == Channel.DAC2)
                        p.Height.Value = 2.5;
                    else
                        p.Height.Value = 3.3;
                    p.Height.Value = refPulse.Height.Value;
                    p.Height.Value = Math.Round(p.Height.Value, 3);
                    p.Offset.Value = Math.Round(p.Offset.Value, 3);
                    p.Width.Value = Math.Round(p.Width.Value, 3);

                    p.Height.Units = refPulse.Height.Units;
                    p.Offset.Units = refPulse.Offset.Units;
                    p.Width.Units = refPulse.Width.Units;

                    if (((PulseMouseAction)mouseAction).Action == pulseMouseActions.AddBefore)
                        Program.Insert(Program.IndexOf(refPulse), p);
                    else
                        Program.Insert(Program.IndexOf(refPulse) + 1, p);
                    Invalidate();
                }
                else if (((PulseMouseAction)mouseAction).Action == pulseMouseActions.OffsetAdjust)
                {
                    Pulse p = ((PulseMouseAction)mouseAction).Pulse;
                    if (p.Offset < 24e-9)
                        p.Offset = "24ns";
                    Invalidate();
                }
                else if (((PulseMouseAction)mouseAction).Action == pulseMouseActions.WidthAdjust)
                {
                    Pulse p = ((PulseMouseAction)mouseAction).Pulse;
                    if (p.Width < 24e-9)
                        p.Width = "24ns";
                    Invalidate();
                }
                else if (((PulseMouseAction)mouseAction).Action == pulseMouseActions.Delete)
                {
                    if (Program.Count > 1)
                        Program.Remove(((PulseMouseAction)mouseAction).Pulse);
                    else
                    {
                        Pulse p = ((PulseMouseAction)mouseAction).Pulse;
                        p.Height = ((PrefixedValue)(Height / YPPU / 4)).MatchPrefix(p.Height);

                        p.Height.Value = Math.Round(p.Height.Value, 3);

                        Invalidate();
                    }
                    Invalidate();
                }
                else if (((PulseMouseAction)mouseAction).Action == pulseMouseActions.HeightAdjust)
                {
                    Pulse p = ((PulseMouseAction)mouseAction).Pulse;
                    if (p.Channel != Channel.DAC2)
                        p.Height = "3.3V";
                }
            }
            Cursor = Cursors.Default;
            if (pef != null)
            {
                if (!Controls.Contains(pef))
                {
                    Controls.Add(pef);
                    pef.SubscribeToEvents();
                    pef.okB.Click += OkB_Click;
                    pef.cancelB.Click += CancelB_Click;
                }
                Invalidate();
                return;
            }
            mouseAction = new NullMouseAction();
        }

        private void CancelB_Click(object sender, EventArgs e)
        {
            Controls.Remove(pef);
            pef = null;
            mouseAction = new NullMouseAction();
            Invalidate();
        }

        private void OkB_Click(object sender, EventArgs e)
        {
            foreach (var pulse in Program)
            {
                if (pulse.Channel == pef.WorkingPulse.Channel)
                    pulse.Color = pef.WorkingPulse.Color;
            }
            pef.syncPulse_UIevent(null, null);
            Program[Program.IndexOf(pef.Pulse)] = pef.WorkingPulse;
            Controls.Remove(pef);
            pef = null;
            mouseAction = new NullMouseAction();
            Invalidate();
        }

        float XVtoXG(float xV)
        {
            return (xV + xOffsetV) * XPPU;
        }
        float YVtoYG(float yV)
        {
            return Height - (yV + yOffsetV) * YPPU;
        }

        PointF VtoG(PointF pV)
        {
            return new PointF(XVtoXG(pV.X), YVtoYG(pV.Y));
        }

        float XGtoXV(float xG)
        {
            return (xG - xOffsetG) / XPPU;
        }
        float YGtoYV(float yG)
        {
            return ((Height - yG) - yOffsetG) / YPPU;
        }
        PointF GtoV(PointF pG)
        {
            return new PointF(XGtoXV(pG.X), YGtoYV(pG.Y));
        }
        float xOffsetG { get { return xOffsetV * XPPU; } set { xOffsetV = value / XPPU; } }
        float xOffsetV { get; set; } = 200e-6F;
        float yOffsetG { get; set; } = 20;
        float yOffsetV { get { return yOffsetG / YPPU; } set { yOffsetG = value * YPPU; } }
        float XPPU { get; set; } = 10.0F / 100e-6F; // pixels per second
        float YPPU { get; set; } = 60; // pixels per volt
        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                var g = e.Graphics;
                g.Clear(Color.White);
                g.DrawString(LastMoveCursorV.ToString(), Font, Brushes.Black, 0, 0);
                if (!(mouseAction is NullMouseAction))
                {
                    g.DrawString(mouseAction.ToString(), Font, Brushes.Black, 0, 10);
                }
                //Draw the scales
                drawXAxis(g, Width, Height, XPPU, xOffsetG, 0, false, BackColor);

                g.ScaleTransform(1, -1);
                g.TranslateTransform(0, -Height + yOffsetG);


                // draw the pulses and graphics relaed to their events
                float xo = xOffsetV;
                foreach (var pulse in Program)
                {
                    var p0 = new PointF(xo * XPPU, 0);
                    var s = pulse.Offset.ToString();
                    var p1 = new PointF((xo + pulse.Offset) * XPPU, 0);
                    var p2 = new PointF((xo + pulse.Offset) * XPPU, pulse.Height * YPPU);
                    var p3 = new PointF((xo + pulse.Offset + pulse.Width) * XPPU, pulse.Height * YPPU);
                    var p4 = new PointF((xo + pulse.Offset + pulse.Width) * XPPU, 0);
                    if (mouseAction is PulseMouseAction)
                    {
                        var action = ((PulseMouseAction)mouseAction);
                        if (action.Pulse == pulse)
                        {
                            if (action.Action == pulseMouseActions.AddBefore)
                                DrawCompleteSeparator(p0, p2, g);
                            else if (action.Action == pulseMouseActions.AddAfter)
                                DrawCompleteSeparator(p4, p2, g);
                            else if (action.Action == pulseMouseActions.Delete)
                            {
                                if (p2.Y != p1.Y)
                                {
                                    var b = new LinearGradientBrush(p2, p1, Color.Red, Color.Transparent);
                                    if (p2.Y > p1.Y)
                                        g.FillRectangle(b, p0.X, 0, p4.X - p0.X, p2.Y - p1.Y + 1);
                                    else
                                        g.FillRectangle(b, p0.X, p2.Y - 2, p4.X - p0.X, p1.Y - p2.Y - 1);
                                }
                            }
                            else if (action.Action == pulseMouseActions.HoverOver)
                            {
                                g.FillRectangle(new SolidBrush(Color.FromArgb(60, pulse.Color)), p0.X, -1, p4.X - p0.X, p2.Y - p1.Y + 1);
                            }
                        }
                    }
                    try
                    {
                        Pen p = new Pen(pulse.Color);
                        g.DrawLine(p, p0, p1);
                        g.DrawLine(p, p1, p2);
                        g.DrawLine(p, p2, p3);
                        g.DrawLine(p, p3, p4);
                        invertedString(pulse.Offset.ToString(), Font, (p0.X + p1.X) / 2, p0.Y, g);
                        invertedString(pulse.Width.ToString(), Font, (p2.X + p3.X) / 2, p2.Y, g);
                        invertedString(pulse.Height.ToString(), Font, (p2.X + p3.X) / 2, (p1.Y + p2.Y) / 2 - Font.Height, g);
                        invertedString(pulse.Channel.ToString(), Font, (p2.X + p3.X) / 2, (p1.Y + p2.Y) / 2 -  2 * Font.Height, g);
                        xo += (float)pulse.Offset + pulse.Width;
                    }
                    catch (Exception ex)
                    { }
                }

                // Draw Scrollbar now
                g.TranslateTransform(0, Height - yOffsetG);
                g.ScaleTransform(1, -1);
                Color light = Color.FromArgb(70, ForeColor);
                Color dark = Color.FromArgb(95, ForeColor);
                var bsb = new SolidBrush(light);
                if (mouseAction is PanelMouseAction)
                    bsb = new SolidBrush(dark);
                g.FillRectangle(bsb, ScrollBarRectangleG);
                g.DrawRectangle(new Pen(light), 0, 0, Width - 1, ScrollBarWidth);
                if (mouseAction is PulseMouseAction)
                {
                    if (((PulseMouseAction)mouseAction).Action == pulseMouseActions.EditPulse)
                        g.FillRectangle(new SolidBrush(Color.FromArgb(150, Color.Black)), 0, 0, Width, Height);
                }
            }
            catch { }
        }
        public void drawXAxis(Graphics g, float w, float h, float scale, float tx, float ty, bool grid, Color backColor)
        {
            var axisP = new Pen(Color.LightGray, 1.5F);
            var majLine = new Pen(Color.Gray, 1.5F);
            var minLine = new Pen(Color.LightGray, 1F);
            g.DrawLine(axisP, tx, 0, tx, h);

            float unitX = 1.0F / 100000000.0F;
            float multF = 5;
            // determine scale first
            while (unitX * scale < 35)
            {
                unitX *= multF;
                multF = multF == 2 ? 5 : 2;
            }

            float minX = 0, maxX = 0;
            while (minX * scale < -tx)
                minX += unitX;
            while (minX * scale > -tx)
                minX -= unitX;

            while (maxX * scale > w - tx)
                maxX -= unitX;
            while (maxX * scale < w - tx)
                maxX += unitX;

            Font f = new Font("ARIAL", 8);

            bool isMinLine = false;
            for (float i = minX; i <= maxX; i += unitX / 2)
            {
                PointF drawableMid = VtoG(new PointF(i, 0));
                if (!isMinLine)
                {
                    PointF drawable1 = new PointF(drawableMid.X, drawableMid.Y - 1.5F);
                    PointF drawable2 = new PointF(drawableMid.X, drawableMid.Y + 1.5F);
                    if (grid) drawable1 = new PointF(drawable1.X, 0);
                    if (grid) drawable2 = new PointF(drawable2.X, h);
                    string s = roundedFrac(i, unitX);
                    var xyo = g.MeasureString(s, f);
                    PointF drawableStr = new PointF(drawableMid.X - xyo.Width / 2, drawableMid.Y + 2);
                    g.DrawLine(majLine, drawable1, drawable2);
                    g.FillRectangle(new SolidBrush(backColor), drawableStr.X, drawableStr.Y, xyo.Width, xyo.Height);
                    g.DrawString(s, f, Brushes.Gray, drawableStr);
                }
                else
                {
                    PointF drawable1 = new PointF(drawableMid.X, drawableMid.Y - 1);
                    PointF drawable2 = new PointF(drawableMid.X, drawableMid.Y + 1);

                    if (grid) drawable1 = new PointF(drawable1.X, 0);
                    if (grid) drawable2 = new PointF(drawable2.X, h);

                    g.DrawLine(minLine, drawable1, drawable2);
                }
                isMinLine = !isMinLine;
            }
        }
        static string roundedFrac(float frac, float leastCount = 0, bool FixLeastcount = true)
        {
            float fracBkp = frac;
            if (frac <= 1e-10 && frac >= -1e-10)
                return "0";
            double thisFrac = bringAbove1(frac);
            double nextFrac = bringAbove1(frac + leastCount);
            int sigFigure = 1;
            while (thisFrac == nextFrac)
            {
                sigFigure++;
                thisFrac = bringAbove1(frac, sigFigure);
                nextFrac = bringAbove1(frac + leastCount, sigFigure);
            }
            return Utils.AddPrefix(thisFrac, "s");
        }
        static double bringAbove1(float frac, int sigFigures = 1)
        {
            if (frac == 0)
                return 0;
            bool isNeg = frac < 0;
            if (isNeg) frac *= -1;
            int mp = 0;
            while (frac < 1)
            {
                frac *= 10;
                mp++;
            }
            if (Math.Floor(frac) == 9)
            {
                mp--;
                frac /= 10;
            }
            float mult = 1;
            while (mp-- > 0)
                mult *= 10;

            return (double)(Math.Round(frac * (isNeg ? -1 : 1), sigFigures) / mult);
        }
        void DrawCompleteSeparator(PointF sp1, PointF sp2, Graphics g)
        {
            var b = new LinearGradientBrush(sp1, new PointF(sp1.X + AddPulseWidth, sp1.Y), Color.CadetBlue, Color.Transparent);
            g.FillRectangle(b, sp1.X, 0, AddPulseWidth, sp2.Y - sp1.Y);
            var b2 = new LinearGradientBrush(sp1, new PointF(sp1.X - AddPulseWidth, sp1.Y), Color.CadetBlue, Color.Transparent);
            g.FillRectangle(b2, sp1.X - AddPulseWidth, 0, AddPulseWidth, sp2.Y - sp1.Y);
            drawSeparatorLines(sp1, new PointF(sp1.X, sp2.Y), g);
        }
        void drawSeparatorLines(PointF p1, PointF p2, Graphics g)
        {
            g.DrawLine(Pens.Red, p1.X - 2, p1.Y - 5, p2.X - 2, p2.Y + 5);
            g.DrawLine(Pens.Red, p1.X + 1, p1.Y - 5, p2.X + 1, p2.Y + 5);

            g.DrawLine(Pens.Red, p1.X - 2, p1.Y - 5, p1.X - 6, p1.Y - 5);
            g.DrawLine(Pens.Red, p2.X - 2, p2.Y + 5, p2.X - 6, p2.Y + 5);

            g.DrawLine(Pens.Red, p1.X + 1, p1.Y - 5, p1.X + 5, p1.Y - 5);
            g.DrawLine(Pens.Red, p2.X + 1, p2.Y + 5, p2.X + 5, p2.Y + 5);
        }
        void drawDimensionArrow(PointF p1, PointF p2)
        {

        }
        void invertedString(string str, Font font, float x, float y, Graphics g)
        {
            g.TranslateTransform(0, y);
            g.ScaleTransform(1, -1);

            g.DrawString(str, font, Brushes.Black, x, -font.Height, new StringFormat() { Alignment = StringAlignment.Center });

            g.ScaleTransform(1, -1);
            g.TranslateTransform(0, -y);
        }

    }
    class MouseAction
    {
        public bool HasStarted { get; set; } = false;
        protected MouseAction() { }
        public PointF StartedAtG { get; set; }
    }
    class NullMouseAction : MouseAction
    {
    }
    class PanelMouseAction : MouseAction
    {
        public panelMouseActions Action { get; set; }
        public override string ToString()
        {
            return "Panel: " + Action.ToString();
        }
    }
    class PulseMouseAction : MouseAction
    {
        public Pulse Pulse { get; set; }
        public pulseMouseActions Action { get; set; }
        public override string ToString()
        {
            return Pulse.ToString() + ": " + Action.ToString();
        }
    }


    enum pulseMouseActions
    {
        None,
        HeightAdjust,
        WidthAdjust,
        OffsetAdjust,
        AddBefore,
        AddAfter,
        HoverOver,
        EditPulse,
        Delete
    }
    enum panelMouseActions
    {
        None, 
        Pan,
        XScale,
        YScale,
        XYScale,
        Scroll
    }
}
