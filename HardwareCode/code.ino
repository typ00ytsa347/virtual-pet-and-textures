#include <Arduino.h>

#define DEBUGSerial Serial
int sensorPin = A0; 	//定义传感器的引脚

#define PRESS_MIN	20
#define PRESS_MAX	6000
#define VOLTAGE_MIN 100
#define VOLTAGE_MAX 3300

char outputP1[4];
char outputP2[4];
char outputP3[4];
char outputP4[4];

void setup()
{
	DEBUGSerial.begin(9600); // setup serial
}

void loop()
{
	unsigned long p1 = getPressValue(A0);
	long p2 = getPressValue(A1);
  long p3 = getPressValue(A2);
  long p4 = getPressValue(A3);

  ltoa(p1, outputP1, 10);
  Serial.print("P1: ");
  Serial.println(outputP1);
  delay(100);

  ltoa(p2, outputP2, 10);
  Serial.print("P2: ");
  Serial.println(outputP2);
  delay(100);

  ltoa(p3, outputP3, 10);
  Serial.print("P3: ");
  Serial.println(outputP3);
  delay(100);

  ltoa(p4, outputP4, 10);
  Serial.print("P4: ");
  Serial.println(outputP4);
	delay(100);
  
}

long getPressValue(int pin)
{	
  int value = analogRead(pin);
	
  return value;
}

