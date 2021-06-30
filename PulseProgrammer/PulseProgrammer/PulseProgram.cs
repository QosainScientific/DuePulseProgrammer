using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PulseProgrammer
{
    public class PulseProgram : Collection<Pulse>
    {
        public PrefixedValue DeadTime { get; set; } = 0;
        public int Repeat { get; set; } = 0;

    }
    public class Pulse
    {
        public System.Drawing.Color Color { get; set; } = System.Drawing.Color.Red;
        public Channel Channel { get; set; } = Channel.DAC2;
        public PrefixedValue Width { get; set; } = 0;
        public PrefixedValue Height { get; set; } = 0;
        public PrefixedValue Offset { get; set; } = 0;
        public static implicit operator Pulse(string string_)
        {
            string_ = string_.Replace(";", ",");
            var vSets = string_.Replace(" ", "").Split(new char[] { ',' });
            Pulse g = new Pulse();
            foreach (string vSet in vSets)
            {
                var pair = vSet.Split(new char[] { '=' });
                if (pair[0].ToLower() == "w")
                    g.Width = pair[1];
                else if ((pair[0].ToLower() == "h"))
                    g.Height = pair[1];
                else if (pair[0].ToLower() == "d" || pair[0].ToLower() == "o")
                    g.Offset = pair[1];
                else if (pair[0].ToLower() == "ch")
                    g.Channel = (Channel)Enum.Parse(typeof(Channel), pair[1]);
                else if (pair[0].ToLower() == "cl")
                {
                    var p = pair[1].Split(new char[] { '.' });

                    int R = Convert.ToInt32(p[0]);
                    int G = Convert.ToInt32(p[1]);
                    int B = Convert.ToInt32(p[2]);
                    g.Color = System.Drawing.Color.FromArgb(R, G, B);
                }
            }
            return g;
        }
        public Pulse Clone()
        {
            var p = new Pulse();
            p.Width = Width.Clone();
            p.Height = Height.Clone();
            p.Offset = Offset.Clone();
            p.Color = Color;
            p.Channel = Channel;
            return p;
        }
        public override string ToString()
        {
            return "O = " + Offset.ToString() + ", W = " + Width.ToString() + ", H = " + Height.ToString() + ", Ch = " + Channel.ToString() + ", Cl = " + Color.R + "." + Color.G + "." + Color.B;
        }

        public string ToControllerString()
        {
            string str = "";
            if (Channel.ToString().Length > 3)
                str = Utils.dueTime(Offset) + " " + Utils.dueTime(Width) + " " + Utils.dueDac2(Height) + " " + (Channel == Channel.DAC2 ? "0" : Channel.ToString().Substring(3));
            else if (Channel == Channel.Tx || Channel == Channel.Rx)
                str = Utils.dueTime(Offset) + " " + Utils.dueTime(Width) + " " + Utils.dueDac2(Height) + " " + (Channel == Channel.Tx ? "1" : "2");
            return str;
        }

    }

    public enum Channel : byte
    {
        Tx = 1,
        Rx = 2,
        DAC2 = 0,
    }
    //public enum Channel : byte
    //{
    //    DAC2 = 1,
    //    Pin2 = 2,
    //    Pin3 = 3,
    //    Pin4 = 4,
    //    Pin5 = 5,
    //    Pin6 = 6,
    //    Pin7 = 7,
    //    Pin8 = 8,
    //    Pin9 = 9,
    //    Pin10 = 10,
    //    Pin11 = 11,
    //    Pin12 = 12,
    //    Pin14 = 13,
    //    Pin15 = 14,
    //    Pin16 = 15,
    //    Pin17 = 16,
    //    Pin18 = 17,
    //    Pin19 = 18,
    //    Pin20 = 19,
    //    Pin21 = 20,
    //    Pin22 = 21,
    //    Pin23 = 22,
    //    Pin24 = 23,
    //    Pin25 = 24,
    //    Pin26 = 25,
    //    Pin27 = 26,
    //    Pin28 = 27,
    //    Pin29 = 28,
    //    Pin30 = 29,
    //    Pin31 = 30,
    //    Pin50 = 31,
    //    Pin51 = 32,
    //    Pin52 = 33,
    //    Pin53 = 34,
    //    PinA0 = 35,
    //    PinA1 = 36,
    //    PinA2 = 37,
    //    PinA3 = 38,
    //    PinA4 = 39,
    //    PinA5 = 40,
    //    PinA6 = 41,
    //    PinA7 = 42,
    //    PinA8 = 43,
    //    PinA9 = 44,
    //    PinA10 = 45,
    //    PinA11 = 46
    //}
}
