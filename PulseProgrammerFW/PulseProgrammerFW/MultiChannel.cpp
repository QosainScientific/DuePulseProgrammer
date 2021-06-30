//#include "Program.h"
//#include "Debugger.h"
//#include "Pulse.h"
//
//uint32_t R2R0Map[4096];
//int DAC0Val = 0;
////#define PortCMap ((uint32_t)0b00000000000111000000001111111110)
//#define PortCMap ((uint32_t)0xFFFFFFFF)
//uint32_t mapNumber(uint32_t number, int* b2pMap, int maxBit)
//{
//	uint32_t mapped = 0;
//	for (int i = 0; i <= maxBit; i++)
//	{
//		mapped |= (uint32_t)((number >> i) % 2) << (uint32_t)(b2pMap[i]);
//	}
//	return mapped;
//}
//
//// the setup function runs once when you press reset or power the board
//void setup()
//{
//	Serial.begin(115200);
//	PCDebugger.begin(&Serial);
//	PCDebugger.println("Hi");
//
//	PIOC->PIO_PER = PortCMap;
//	PIOC->PIO_OER = PortCMap;
//
//	int presetMap[] = { 16, 19, 18, 8, 9, 6, 7, 4, 5, 2, 3, 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
//	int BitToPin[32];
//	int maxBit = -1;
//	for (int i = 0; i < 32; i++)
//	{
//		BitToPin[i] = presetMap[i];
//		if (BitToPin[i] <= -1)
//		{
//			if (maxBit == -1)
//				maxBit = i - 1;
//		}
//	}
//
//	for (uint32_t i = 0; i < 4096; i++)
//	{
//		uint32_t num = mapNumber(i, BitToPin, maxBit);
//		R2R0Map[i] = num;
//	}
//	pinMode(2, OUTPUT);
//
//	PCDebugger.println("Begin");
//}
//
//void analogWrite2(int val)
//{
//	//if (val >= 0 && val <= 4095)
//	{
//		PIOC->PIO_SODR |= R2R0Map[val];
//		PIOC->PIO_CODR |= ~(R2R0Map[val]);
//		//WaitForDACReady;
//		//DAC0Val = val;
//		//DACC_INTERFACE->DACC_CDR = DAC0Val;
//
//	}
//}
//void delayte_7(uint32_t te_7);
//Program program;
//// the loop function runs over and over again until power down or reset
//void loop()
//{
//	while (Serial.available())
//	{
//		PacketCommand pc;
//		uint8_t pe = PacketCommand_FromStream(pc, Serial);
//		if (pe == ProtocolError_None)
//		{
//			String str = pc.PayLoadString();
//			int cid = str.substring(0, str.indexOf(" ")).toInt();
//			str = str.substring(str.indexOf(" ") + 1);
//			String com = str.substring(0, str.indexOf(" "));
//			String args = str.substring(str.indexOf(" ") + 1);
//			if (com == F("add"))
//			{
//				int addAt = args.substring(0, args.indexOf(" ")).toInt();
//				String offsetStr = args.substring(args.indexOf(" ") + 1);
//				String widthStr = offsetStr.substring(offsetStr.indexOf(" ") + 1);
//				String heightStr = widthStr.substring(widthStr.indexOf(" ") + 1);
//				String channelStr = heightStr.substring(heightStr.indexOf(" ") + 1);
//				offsetStr = offsetStr.substring(0, offsetStr.indexOf(" "));
//				widthStr = widthStr.substring(0, widthStr.indexOf(" "));
//				heightStr = heightStr.substring(0, heightStr.indexOf(" "));
//
//				program.Pulses[addAt].Offset = offsetStr.toInt();
//				program.Pulses[addAt].Width = widthStr.toInt();
//				program.Pulses[addAt].Height = heightStr.toInt();
//				program.Pulses[addAt].Channel = channelStr.toInt();
//				//pinMode(program.Pulses[addAt].Channel, OUTPUT);
//				//digitalWrite(program.Pulses[addAt].Channel, HIGH);
//
//				program.Count++;
//				PacketCommand(PacketCommandID_FB, String(cid)
//					+ F(" I=") + String(addAt)
//					+ F(" O=") + String(program.Pulses[addAt].Offset)
//					+ F(" W=") + String(program.Pulses[addAt].Width)
//					+ F(" H=") + String(program.Pulses[addAt].Height)
//					+ F(" Ch=") + String(program.Pulses[addAt].Channel)
//				).SendCommand(Serial);
//			}
//			else if (com == F("count"))
//			{
//				program.Count = args.toInt();
//				PacketCommand(PacketCommandID_FB, String(cid) + F(" C=") + String(program.Count)).SendCommand(Serial);
//			}
//			else if (com == F("deadtime"))
//			{
//				program.DeadTime = args.toInt();
//				PacketCommand(PacketCommandID_FB, String(cid) + F(" DT=") + String(program.DeadTime)).SendCommand(Serial);
//			}
//			else if (com == F("repeat"))
//			{
//				program.Repeat = args.toInt();
//				PacketCommand(PacketCommandID_FB, String(cid) + F(" R=") + String(program.Repeat)).SendCommand(Serial);
//			}
//			else if (com == F("run"))
//			{
//				program.reset();
//				program.canRun = true;
//				PacketCommand(PacketCommandID_FB, String(cid) + F(" Run")).SendCommand(Serial);
//			}
//			else if (com == F("stop"))
//			{
//				program.reset();
//				program.canRun = false;
//				PacketCommand(PacketCommandID_FB, String(cid) + F(" Stop")).SendCommand(Serial);
//			}
//
//		}
//	}
//	if (program.canRun)
//	{
//		if (program.CurrentRunCount >= program.Repeat && program.Repeat != 0)
//		{
//			program.canRun = false;
//			return;
//		}
//
//		for (int i = 0; i < program.Count; i++)
//		{
//			if (program.Pulses[i].Channel == 0)
//			{
//				//analogWrite2(0);
//				delayte_7(program.Pulses[i].Offset);
//
//				PIOC->PIO_SODR |= R2R0Map[program.Pulses[i].Height];
//				PIOC->PIO_CODR |= ~(R2R0Map[program.Pulses[i].Height]);
//				//analogWrite2(program.Pulses[i].Height);
//				delayte_7(program.Pulses[i].Width);
//				PIOC->PIO_SODR |= R2R0Map[0l];
//				PIOC->PIO_CODR |= ~(R2R0Map[0]);
//			}
//			else
//			{
//				digitalWrite(program.Pulses[i].Channel, 0);
//				delayte_7(program.Pulses[i].Offset);
//				digitalWrite(program.Pulses[i].Channel, program.Pulses[i].Height > 0);
//				delayte_7(program.Pulses[i].Width);
//				digitalWrite(program.Pulses[i].Channel, 0);
//			}
//		}
//		delayte_7(program.DeadTime);
//		program.CurrentRunCount++;
//	}
//}
//void delayte_7(uint32_t te_7)
//{
//	if (te_7 < 10)
//	{
//		for (int i = 0; i < te_7; i++)
//		{
//			__asm__("NOP");
//			__asm__("NOP");
//			__asm__("NOP");
//			__asm__("NOP");
//			__asm__("NOP");
//		}
//	}
//	else
//		delayMicroseconds(te_7 / 10);
//}
//
//
