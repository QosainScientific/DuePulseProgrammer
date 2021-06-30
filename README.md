
# DuePulseProgrammer
 
Turns an arduino due into a high speed dual channel pulser, which is runtime programmable through a GUI.

Features:
1. Two digital channels
2. Arduino compatible
3. GUI
4. Time resolution up to 100ns (Needs testing)

# Building the Code
The code is divided into two main parts:
## Firmware
Firmware can be built either in Arduino or through Visual micro extension in Visual Studio. The later is preferred for development since it has Intellisense.

Please note that Arduino is required for both options though.

## Option 1 (Arduino)
1. Download and install arduino from [the official Arduino website](https://www.arduino.cc/en/software).
2. Also isntall Arduino 32 SAM boards development tools from Tools > Borad > Board Manager ![enter image description here](https://raw.githubusercontent.com/QosainScientific/DuePulseProgrammer/main/Docs/samboards.PNG)
3. Clone this repository or download from 
4. Open PulseProgrammerFW/PulseProgrammerFW/PulseProgrammerFW.ino in arduino. It also loads other files in the directory into arduino. ![arduino also loads other documents](https://raw.githubusercontent.com/QosainScientific/DuePulseProgrammer/main/Docs/loadedfiles.PNG)
5. Open arduino and select "Arduino Due (Programming Port)" from Tools > Board
6. Press Ctrl+R to verify that the code is OK.
7. Connect arduino to the computer through the USB programming port and select it from Tools > Port ![arduino due USBs](https://projectiot123.com/wp-content/uploads/2019/05/Arduino-DUE.jpg)
8. Select the Press Ctrl+U to upload.

## Option 2 (Visual Micro)
1. Follow step 1-3 of Option 1.
2. Install Visual Studio and make sure the minimum components required for C++ development are installed.  A community edition of VS can be downloaded from [this Microsoft website](https://visualstudio.microsoft.com/vs/community/)![VS setup for c++](https://raw.githubusercontent.com/QosainScientific/DuePulseProgrammer/main/Docs/FWEnv.PNG)
3. Install Visual Micro for Visual Studio ([Visual Micro Downlad Page](https://www.visualmicro.com/page/Arduino-Visual-Studio-Downloads.aspx))
4. Open PulseProgrammerFW/PulseProgrammerFW.sln in visual studio. ![arduino also loads other documents](https://raw.githubusercontent.com/QosainScientific/DuePulseProgrammer/main/Docs/loadedfiles.PNG)
5. Connect arduino to the computer through the USB programming port.
6. Set board to Ardunio Due (Programming Port), build to Release, and com port to the one on which Due is attached. ![enter image description here](https://raw.githubusercontent.com/QosainScientific/DuePulseProgrammer/main/Docs/vm.PNG)
7. Select Debug > Build Solution to build or Debud > Start Debugging to build and upload.
 
## Desktop application
In order to build the desktop application, an installation of MS Visual studio is required. A community edition can be downloaded from this [Microsoft website](https://visualstudio.microsoft.com/vs/community/). Only desktop development parts are needed to build the application.

![installation view of visual studio](https://raw.githubusercontent.com/QosainScientific/DuePulseProgrammer/main/Docs/DotNetEnv.PNG)

# The Basic Concept of Working
The firmware is built to start working only when it has received a set of program commands from the serial port. These commands are created in the background as the user uses the graphical tools on the GUI to define a "Pulse Program" and transferred in packets to the arduino using the serial port selected by the user. For delays greater than 1us, the inbuilt arduino delayMicroseconds() is used and for shorter delays, "Nop" statements are used.
