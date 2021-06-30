/*
 Name:		PulseProgrammerFW.ino
 Created:	8/9/2018 3:04:42 PM
 Author:	umar.hassan
*/

#define Ch1PinNotMask 0xFEFFFFFF
#define Ch2PinNotMask  0xFFFF7FFF
#define Ch1PinMask 0x04000000
#define Ch2PinMask 0x00008000
#define Ch1Port B
#define Ch2Port A
#define ConCat2(x,y) x##y
#define ConCat3(x,y,z) x##y##z
#define PIOReg(P) ConCat2(PIO,P)
#define Ch1PIO PIOReg(Ch1Port)
#define Ch2PIO PIOReg(Ch2Port)

#include "Program.h"
#include "Debugger.h"
#include "Pulse.h"

// the setup function runs once when you press reset or power the board
void setup() 
{
	Serial.begin(115200);
	PCDebugger.begin(&Serial);

	Ch1PIO->PIO_PER = Ch1PinMask;
	Ch2PIO->PIO_PER = Ch2PinMask;
	Ch1PIO->PIO_OER = Ch1PinMask;
	Ch2PIO->PIO_OER = Ch2PinMask;
}

void delayte_7(uint32_t te_7);
Program program;
// the loop function runs over and over again until power down or reset
void loop()
{
	while (Serial.available())
	{
		PacketCommand pc;
		uint8_t pe = PacketCommand_FromStream(pc, Serial);
		if (pe == ProtocolError_None)
		{
			String str = pc.PayLoadString();
			int cid = str.substring(0, str.indexOf(" ")).toInt();
			str = str.substring(str.indexOf(" ") + 1);
			String com = str.substring(0, str.indexOf(" "));
			String args= str.substring(str.indexOf(" ") + 1);
			if (com == F("add"))
			{
				int addAt = args.substring(0, args.indexOf(" ")).toInt();
				String offsetStr = args.substring(args.indexOf(" ") + 1);
				String widthStr = offsetStr.substring(offsetStr.indexOf(" ") + 1);
				String heightStr = widthStr.substring(widthStr.indexOf(" ") + 1);
				String channelStr = heightStr.substring(heightStr.indexOf(" ") + 1);
				offsetStr = offsetStr.substring(0, offsetStr.indexOf(" "));
				widthStr = widthStr.substring(0, widthStr.indexOf(" "));
				heightStr = heightStr.substring(0, heightStr.indexOf(" "));

				program.Pulses[addAt].Offset = offsetStr.toInt();
				program.Pulses[addAt].Width = widthStr.toInt();
				program.Pulses[addAt].Height = heightStr.toInt();
				program.Pulses[addAt].Channel = channelStr.toInt();
				//pinMode(program.Pulses[addAt].Channel, OUTPUT);
				//digitalWrite(program.Pulses[addAt].Channel, HIGH);

				program.Count++;
				PacketCommand(PacketCommandID_FB, String(cid) 
					+ F(" I=") + String(addAt)
					+ F(" O=") + String(program.Pulses[addAt].Offset)
					+ F(" W=") + String(program.Pulses[addAt].Width)
					+ F(" H=") + String(program.Pulses[addAt].Height)
					+ F(" Ch=") + String(program.Pulses[addAt].Channel)
				).SendCommand(Serial);
			}
			else if (com == F("count"))
			{
				program.Count = args.toInt();
				PacketCommand(PacketCommandID_FB, String(cid) + F(" C=") + String(program.Count)).SendCommand(Serial);
			}
			else if (com == F("deadtime"))
			{
				program.DeadTime = args.toInt();
				PacketCommand(PacketCommandID_FB, String(cid) + F(" DT=") + String(program.DeadTime)).SendCommand(Serial);
			}
			else if (com == F("repeat"))
			{
				program.Repeat = args.toInt();
				PacketCommand(PacketCommandID_FB, String(cid) + F(" R=") + String(program.Repeat)).SendCommand(Serial);
			}
			else if (com == F("run"))
			{
				program.reset();
				program.canRun = true;
				PacketCommand(PacketCommandID_FB, String(cid) + F(" Run")).SendCommand(Serial);
			}
			else if (com == F("stop"))
			{
				program.reset();
				program.canRun = false;
				PacketCommand(PacketCommandID_FB, String(cid) + F(" Stop")).SendCommand(Serial);
			}

		}
	}
	if (program.canRun)
	{
		if (program.CurrentRunCount >= program.Repeat && program.Repeat != 0)
		{
			program.canRun = false;
			return;
		}

		for (int i = 0; i < program.Count; i++)
		{
			if (program.Pulses[i].Channel == 1)
			{
				delayte_7(program.Pulses[i].Offset);
				Ch1PIO->PIO_SODR |= Ch1PinMask;
				Ch1PIO->PIO_CODR &= Ch1PinMask;
				delayte_7(program.Pulses[i].Width);
				Ch1PIO->PIO_SODR |= Ch1PinMask;
				Ch1PIO->PIO_CODR |= Ch1PinMask;
			}
			else
			{
				delayte_7(program.Pulses[i].Offset);
				Ch2PIO->PIO_SODR |= Ch2PinMask;
				Ch2PIO->PIO_CODR &= Ch2PinMask;
				delayte_7(program.Pulses[i].Width);
				Ch2PIO->PIO_SODR |= Ch2PinMask;
				Ch2PIO->PIO_CODR |= Ch2PinMask;
			}
		}
		delayte_7(program.DeadTime);
		program.CurrentRunCount++;
	}
}
void delayte_7(uint32_t te_7)
{
	if (te_7 < 10)
	{
		for (int i = 0; i < te_7; i++)
		{
			__asm__("NOP");
		}
	}
	else
		delayMicroseconds(te_7 / 10);
}


