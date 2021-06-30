﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Ports;

namespace PulseProgrammer
{

    public class PacketCommand
    {
        public byte[] data_;
        byte[] comData_ = new byte[6];
        static void DoEvents()
        {
            //System.Windows.Forms.Application.DoEvents();
        }
        public static ProtocolError FromStream(ref PacketCommand command, SerialPort serial, int timeOut = 3000)
        {
            TimeSpan start = new TimeSpan(DateTime.Now.Ticks);
            bool gotStartBytes = false;
            bool hasAA = false;
            while (serial.BytesToRead >= (hasAA ? 7 : 8) || ((new TimeSpan(DateTime.Now.Ticks) - start).TotalMilliseconds) < timeOut)
            {
                if (serial.BytesToRead < (hasAA ? 7 : 8))
                {
                    System.Threading.Thread.Sleep(10);
                    DoEvents();
                    continue;
                }
                if (!hasAA)
                {
                    int bb = serial.ReadByte();
                    if (bb != 0xAA)
                    {
                        int btr = serial.BytesToRead;
                        byte[] b2 = new byte[btr];
                        serial.Read(b2, 0, btr);
                        continue;
                    }
                }
                int b = serial.ReadByte();
                hasAA = false;
                if (b != 0x55 && b != 0xAA)
                    continue;
                else if (b == 0xAA)
                {
                    hasAA = true;
                    continue;
                }
                gotStartBytes = true;
                break;
            }
            byte[] buffer = new byte[6];
            if (gotStartBytes)
            {
                serial.Read(buffer,0,  6);
                command.PacketID = (TCPCommandID)buffer[0];
                int dlen = ((int)buffer[1]) + (int)(buffer[2] << 8);
                command.PayLoadLength = dlen;
                command.SourceID = buffer[3];
                command.TargetID = buffer[4];
                command.TrueCheckSum = buffer[5];
                buffer = new byte[command.PayLoadLength];
                if (command.PayLoadLength > 1024)
                {
                    return ProtocolError.BufferOverFlow;
                }
                int i = 0;
                while (i < command.PayLoadLength && ((new TimeSpan(DateTime.Now.Ticks) - start).TotalMilliseconds) < timeOut)
                {
                    if (serial.BytesToRead > 0)
                        buffer[i++] = (byte)serial.ReadByte();
                    else
                    {
                        System.Threading.Thread.Sleep(10);
                        DoEvents();
                    }
                }
                command.PayLoad = buffer;
                if (i < command.PayLoadLength) // not all of the bytes were received
                {
                    return ProtocolError.ReadTimeout;
                }
                // calculate checksum
                if (command.TrueCheckSum != command.ActualCheckSum)
                {
                    return ProtocolError.CheckSumMismatch;
                }
                else
                {
                }

                // we got the command. letes return
                return ProtocolError.None;
            }
            return ProtocolError.ReadTimeout;
        }

        public void SendCommand(SerialPort dStream, bool dontPacketize = false)
        {
            try
            {
                if (!dontPacketize)
                {
                    dStream.Write(new byte[] { 0xAA, 0x55 }, 0, 2);
                    TrueCheckSum = ActualCheckSum;
                    dStream.Write(comData_, 0, 6);
                }
                if (PayLoadLength > 0)
                    dStream.Write(data_, 0, PayLoadLength);
            }
            catch
            {
            }
        }
        public string PayLoadString
        {
            get { return new UTF8Encoding().GetString(PayLoad); }
            set { PayLoad = new UTF8Encoding().GetBytes(value); }
        }
        public byte[] PayLoad
        {
            get
            {
                return data_;
            }
            set
            {
                PayLoadLength = value.Length;
                Buffer.BlockCopy(value, 0, data_, 0, data_.Length);

            }
        }


        public byte SourceID
        {
            get { return comData_[3]; }
            set { comData_[3] = value; }
        }
        public byte TargetID
        {
            get { return comData_[4]; }
            set { comData_[4] = value; }
        }
        public TCPCommandID PacketID
        {
            get
            {
                return (TCPCommandID)comData_[0];
            }
            set
            {
                comData_[0] = (byte)value;
            }
        }
        public PacketCommand()
        {
            PayLoad = new byte[0];
            PacketID = 0;
        }
        public PacketCommand(TCPCommandID commandID)
        {
            PayLoad = new byte[0];
            PacketID = commandID;
        }

        public PacketCommand(TCPCommandID commandID, string str, int srcID, int tgtID)
        {
            SourceID = (byte)srcID;
            TargetID = (byte)tgtID;
            PacketID = commandID;
            PayLoad = new UTF8Encoding().GetBytes(str);
        }
        public PacketCommand(TCPCommandID commandID, byte[] payload, int srcID, int tgtID)
        {
            SourceID = (byte)srcID;
            TargetID = (byte)tgtID;
            PacketID = commandID;
            PayLoad = payload;

        }
        public PacketCommand(TCPCommandID commandID, byte[] payload)
        {
            PacketID = commandID;
            PayLoad = payload;
        }
        byte TrueCheckSum
        {
            get
            {
                return comData_[5];
            }
            set
            {
                comData_[5] = value;
            }
        }
        public int PayLoadLength
        {
            get
            {
                return data_.Length;
            }
            set
            {
                data_ = new byte[value];
                comData_[1] = (byte)value;
                comData_[2] = (byte)(value >> 8);
            }
        }
        byte ActualCheckSum
        {
            get
            {
                byte sum = 0;
                for (int i = 0; i < 5; i++)
                {
                    sum ^= comData_[i];
                }
                for (int i = 0; i < PayLoadLength; i++)
                {
                    sum ^= PayLoad[i];
                }
                return sum;
            }
        }

    }
    public enum TCPCommandID
    {
        Com = 1,
        FB = 2,
        Debug = 90
    }
    public enum ProtocolError : byte
    {
        Unknown = 0,
        UsernameOrPasswordWrong = 1,
        ServerUnreachable = 2,
        NoInternet = 3,
        None = 4,
        AlreadyConnected = 5,
        CookieMismatch = 6,
        ReadTimeout = 7,
        NodeUnavailable = 8,
        PairRefused = 9,
        CheckSumMismatch = 10,
        DataCorruption = 11,
        UpdateRequired = 12,
        BufferOverFlow = 15,
    }
}