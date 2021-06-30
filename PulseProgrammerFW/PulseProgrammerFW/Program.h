#pragma once
#include <Arduino.h>
#include "Pulse.h"

#define MaxCount 50
class Program
{
public:
	Pulse Pulses[MaxCount];
	uint32_t DeadTime = 0;
	uint16_t Repeat = 0;
	bool canRun = 0;
	int CurrentRunCount = 0;
	void reset();
	Program();
	int Count = 0;
};

