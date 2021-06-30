#include "PacketCommands.h"

uint8_t PacketCommand_FromStream(PacketCommand& command, Stream &stream, uint16_t timeOut, bool useExistingBuffer)
{
	unsigned long start = millis();

	bool gotStartBytes = false;
	bool hasAA = false;
	while (stream.available() >= 8 || (millis() - start) < timeOut)
	{
		if (stream.available() < 8)
		{
			delay(0); //yield
			continue;
		}
		if (!hasAA)
		{
			if (stream.read() != 0xAA)
				continue;
		}
		hasAA = false;
		uint8_t b = stream.read();
		if (b != 0x55 && b != 0xAA)
			continue;
		else if (b == 0xAA)
		{
			hasAA;
			continue;
		}
		gotStartBytes = true;
		break;
	}
	if (gotStartBytes)
	{
		stream.readBytes(command.comData_, 6);
		uint16_t dlen = command.PayLoadLength();
		if (!useExistingBuffer)
			command.PayLoadLength(dlen);

		if (dlen > 1024) return ProtocolError_BufferOverFlow;
		int i = 0;
		while (i < dlen && (millis() - start) < timeOut)
		{
			if (stream.available())
			{
				command.PayLoad()[i++] = stream.read();
			}
		}
		if (i < dlen) // not all of the bytes were received
		{
			return ProtocolError_ReadTimeout;
		}
		// calculate checksum
		if (command.TrueCheckSum() != command.ActualCheckSum())
		{
			return ProtocolError_CheckSumMismatch;
		}

		// we got the command. letes return
		return ProtocolError_None;
	}
	return ProtocolError_ReadTimeout;
}

void PacketCommand::SendCommand(Stream& serial_)
{
	serial_.write(0xAA);
	serial_.write(0x55);
	uint8_t sum = 0;
	for (int i = 0; i < 5; i++)
		sum ^= comData_[i];
	for (int i = 0; i < PayLoadLength(); i++)
		sum ^= data_[i];
	comData_[5] = sum;
	for (int i = 0; i < 6; i++)
		serial_.write(comData_[i]);
	if (PayLoadLength())
		serial_.write(data_, PayLoadLength());
}

PacketCommand::~PacketCommand()
{
	delete data_;
}
PacketCommand::PacketCommand()
{
	data_ = new uint8_t[1];
	SourceID(DefaultUARTNodeID);
	PayLoadLength(0);
	PacketID(0);
}
PacketCommand::PacketCommand(uint8_t commandID, String string, uint8_t srcID, uint8_t tgtID) :
	PacketCommand(commandID, string)
{
	SourceID(srcID);
	TargetID(tgtID);
}
PacketCommand::PacketCommand(uint8_t commandID, String string)
{
	data_ = new uint8_t[1];
	SourceID(DefaultUARTNodeID);
	PacketID(commandID);
	PayLoad((uint8_t*)string.c_str(), string.length());
}
PacketCommand::PacketCommand(uint8_t commandID, uint8_t srcID, uint8_t tgtID) :
	PacketCommand(commandID)
{
	SourceID(srcID);
	TargetID(tgtID);
}
PacketCommand::PacketCommand(uint8_t commandID)
{
	data_ = new uint8_t[1];
	SourceID(DefaultUARTNodeID);
	PayLoadLength(0);
	PacketID(commandID);
}
PacketCommand::PacketCommand(uint8_t commandID, uint8_t* payload, uint16_t length, uint8_t srcID, uint8_t tgtID) :
	PacketCommand(commandID, payload, length)
{
	SourceID(srcID);
	TargetID(tgtID);
}
PacketCommand::PacketCommand(uint8_t commandID, uint8_t* payload, uint16_t length)
{
	data_ = new uint8_t[1];
	SourceID(DefaultUARTNodeID);
	PacketID(commandID);
	PayLoad(payload, length);
}
uint8_t PacketCommand::SourceID()
{
	return comData_[3];
}
void PacketCommand::SourceID(uint8_t id)
{
	comData_[3] = id;
}
uint8_t PacketCommand::TargetID()
{
	return comData_[4];
}
void PacketCommand::TargetID(uint8_t id)
{
	comData_[4] = id;
}
uint8_t PacketCommand::TrueCheckSum()
{
	return comData_[5];
}
void PacketCommand::PayLoadLength(uint16_t len)
{
	comData_[1] = len & 0xFF;
	comData_[2] = ((uint16_t)len >> 8) & 0xFF;
	if (len == 0)
		len = 1;
	//if (data_)
	delete data_;
	data_ = new uint8_t[len + 1]; // for string processing
	data_[len] = 0;
}
uint16_t PacketCommand::PayLoadLength()
{
	return BytesToUInt16(comData_ + 1);
}
uint8_t PacketCommand::ActualCheckSum()
{
	uint8_t sum = 0;
	for (int i = 0; i < 5; i++)
	{
		sum ^= comData_[i];
	}
	for (int i = 0; i < PayLoadLength(); i++)
	{
		sum ^= PayLoad()[i];
	}
	return sum;
}
String PacketCommand::PayLoadString()
{
	PayLoad()[PayLoadLength()] = 0;
	return String((char*)PayLoad());
}
void PacketCommand::PayLoadString(String str)
{
	PayLoad((uint8_t*)str.c_str(), str.length());
}
uint8_t* PacketCommand::PayLoad()
{
	return data_;
}
void PacketCommand::PayLoad(uint8_t* payload, uint8_t length)
{
	PayLoadLength(length);
	Common_BlockCopy(payload, 0, PayLoad(), 0, length);
}
uint8_t PacketCommand::PacketID()
{
	return comData_[0];
}
void PacketCommand::PacketID(uint8_t packetID)
{
	comData_[0] = (uint8_t)packetID;
}