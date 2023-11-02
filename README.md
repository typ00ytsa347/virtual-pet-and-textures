# SE702/CS705 Advanced HCI: Virtual Pet - Group 15

A virtual pet interaction program for group 15's research, where the user interacts with the virtual pet in Unity with pressure sensors connected to the Arduino board

### System Requirement
#### Hardware requirement: sensors that could be connected to the device
##### Components
- FSR406 * 4: A force-sensitive resistor that changes resistance based on applied force.
- Linear Voltage Conversion Modules * 4: Converts the FSR's resistance to a proportional voltage.
- Arduino Uno board: Reads the converted voltage and communicates the data to the PC.
- PC: Used to process and possibly visualize the force data.
- Breadboard * 1: For connecting all the hardware together.
  
##### Setup
1. Connecting the FSR406 to the Voltage Conversion Module:
    - Connect one end of the FSR to one of the input pins on the linear voltage converter module. Connect the other end of the FSR to a different pin.

2. Linking the Voltage Conversion Module to Arduino:
    - Connect the module's output (AO) to one of the Arduino's analog pins(A0-A3). Connect the power supply (VCC) and ground (GND) to the Arduino's 5V and GND pins via the breadboard.

3. Connecting Arduino to PC:
      - Using a USB 2.0 type B cable, connect the Arduino to one of the PC's USB ports. Install the required drivers using the Arduino's IDE.
      - Uploading the Arduino Code:
        - Open the Arduino IDE on your PC.
        - Write or paste the code in `HardwareCode` folder to read the analog value from pin A0.
        - Select the port connected to the PC.
        - Upload the code to the Arduino.
      - Reading Data on the PC:
        - Open the Serial Monitor in the Arduino IDE. You should see force data being printed from the FSR in real-time. (Note: When running Unity, make sure the Serial Monitor is closed)

    ![image](https://github.com/Uncleared/702VirtualPet/assets/79774614/df18f6b5-4972-4cfc-a9a9-27e542407ccb)

- Software requirement:
  - Requires [Arduino IDE](https://www.arduino.cc/en/software) to pass the pressure data to Unity
  - The system should be able to run [Unity](https://unity.com/download) v2019.4.37f1

### How to Run
1. Copy the repository to the local environment
2. Connect all hardware components, and connect the board to the PC (as shown in [Setup](https://github.com/Uncleared/702VirtualPet/edit/main/README.md#setup))
3. Open Arduino IDE and select the connected port number
   ![image](https://github.com/Uncleared/702VirtualPet/assets/79774614/c73ad8c0-c5ea-4897-80a3-7824dba8c5c6)
5. Open up the project in Unity v2019.4.37f1, located in the `Unity` folder
6. In the SerialController GameObject, change the Port Name in the Inspector to whichever port name your Arduino board is connected to
   
   ![image](https://github.com/Uncleared/702VirtualPet/assets/79774614/9095808c-50da-44cb-b850-e12ebcb5844b)

7. Press Play

##### Debug with unsuccessful connection of hardware
We use Arduino to debug issues related to the connection of hardware. Debug files are located in `HardwareCode` folder

### Functionality
- There are four pressures sensors, corresponding to the four "massage spots" on the virtual pet. The virtual pet will walk around in idle mode.
- The four massage spots will appear red when they show up.The user can press the corresponding pressure sensors to "massage" the pet. The spot will then gradually turn green, meaning it is being massaged.
- As the pet is being massaged, its affection towards the user wil grow, as shown in the affection meter at the top left. The user has to keep the meter filled at around 80% to make the pet happy. Once successful, the pet will walk towards the user and do a flip, showing their affection.
- There is a 60 seconds timer, and the game will end once the timer is up. Score is calculated based on how many times the player has successfully kept the pet's affection at around 80%.
- When the affection is more than 100%, the pet will explode with the excessive afffection and "die", so make sure don't give more affection than what the pet can take!

#### Libraries Used
- [Ardity](https://assetstore.unity.com/packages/tools/integration/ardity-arduino-unity-communication-made-easy-123819)

### Demo
[![Demo Video](http://img.youtube.com/vi/sWDHwbbPdQM/0.jpg)](http://www.youtube.com/watch?v=sWDHwbbPdQM)
- Executable file: `702VirtualPet.exe`
- Plan for conducting user studies: `UserStudyPlan.pdf`

### Contributors
- Guoying Jiang (gjia514)
- Jiaqi Zhao (jzha535)
- Xingyu Liu (xliu315)
- Yuewei Zhang (yzhb544)
- Yun-Shan Tsai (ytsa347)
- Zixuan Wen (zwen655)
