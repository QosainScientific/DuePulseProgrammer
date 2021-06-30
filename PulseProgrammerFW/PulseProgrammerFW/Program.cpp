// 
// 
// 

#include "Program.h"

Program::Program()
{
	for (int i = 0; i < MaxCount; i++)
	{
		Pulses[i] = Pulse();
	}
}
void Program::reset()
{
	CurrentRunCount = 0;
	canRun = false;
}

