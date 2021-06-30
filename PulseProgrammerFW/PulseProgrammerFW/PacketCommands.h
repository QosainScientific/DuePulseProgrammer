#pragma once
#include <Arduino.h>
#include "commonMethods.h"
#include "Enums.h"
#define DefaultUARTNodeID 0
#define Common_BlockCopy BlockCopy
class PacketCommand
{

protected:
	uint8_t *data_ = new uint8_t[1];

public:
	uint8_t comData_[6];
	uint8_t PacketID();
	void PacketID(uint8_t value);
public:
	PacketCommand();
	~PacketCommand();
	PacketCommand(uint8_t commandID);
	PacketCommand(uint8_t commandID, String string, uint8_t srcID, uint8_t tgtID);
	PacketCommand(uint8_t commandID, uint8_t* payload, uint16_t length, uint8_t srcID, uint8_t tgtID);
	PacketCommand(uint8_t commandID, uint8_t* Payload, uint16_t length);
	PacketCommand(uint8_t commandID, uint8_t srcID, uint8_t tgtID);
	PacketCommand(uint8_t commandID, String string);

	uint8_t SourceID();
	void SourceID(uint8_t id);
	uint8_t TargetID();
	void TargetID(uint8_t id);
	uint8_t ActualCheckSum();
	uint8_t TrueCheckSum();
	void PayLoad(uint8_t* payload, uint8_t length);
	void PayLoadLength(uint16_t length);
	void PayLoadString(String);
	void SendCommand(Stream& serial_);
	uint8_t* PayLoad();
	String PayLoadString();
	uint16_t PayLoadLength();
};

uint8_t PacketCommand_FromStream(PacketCommand& command, Stream &stream, uint16_t timeOut = 3000, bool useExistingBuffer = false);
