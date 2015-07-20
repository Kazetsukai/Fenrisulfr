/*
 * APDS-9301.c
 *
 *  Created on: 16/07/2015
 *      Author: Joseph
 */

#include "APDS-9301.h"

uint8_t SENSOR_GAIN = 16;

void SensorInit()
{
	SoftI2CInit();
}

void WriteSensorRegister(uint8_t address, uint8_t data)
{
	SoftI2CStart();											//Start
	SoftI2CWriteByte(SENSOR_ADDR);							//Slave address with WRITE
	SoftI2CWriteByte(SENSOR_REG_COMMAND |  address);		//Command code with address of register
	SoftI2CWriteByte(data);									//Write data to specified register
	SoftI2CStop();											//Stop condition
}

uint8_t ReadSensorRegister(uint8_t address)
{
	SoftI2CStart();											//Start
	SoftI2CWriteByte(SENSOR_ADDR);							//Slave address with WRITE
	SoftI2CWriteByte(SENSOR_REG_COMMAND |  address);		//Command code with address of register
	SoftI2CRepeatedStart();												//Restart
	SoftI2CWriteByte(SENSOR_ADDR | 0x01);					//Slave address with READ
	uint8_t value = SoftI2CReadByte(1);						//Read 1 byte to get data. Master will reply with ACK
	SoftI2CStop();											//Stop condition
	return value;
}

void SensorPowerUp()
{
	WriteSensorRegister(SENSOR_REG_CONTROL, 0x03);
}

uint8_t SensorCommsAreWorking()
{
	//Power up the sensor. Can be used to confirm device is communicating properly if READ is done after power up and the value is 0x03.
	SensorPowerUp();

	if (ReadSensorRegister(SENSOR_REG_CONTROL) == 0x03)
	{
		return 1;
	}
	else
	{
		return 0;
	}
}

uint16_t ReadSensorDATA0()
{
	SoftI2CStart();																//Start
	SoftI2CWriteByte(SENSOR_ADDR);												//Slave address with WRITE
	SoftI2CWriteByte(SENSOR_REG_COMMAND | SENSOR_WORD | SENSOR_REG_DATA0LOW);	//Command code with address of register
	SoftI2CStart();														//Restart
	SoftI2CWriteByte(SENSOR_ADDR | 0x01);										//Slave address with READ
	uint16_t value = SoftI2CReadByte(1);										//Read LOW data. Master will reply with ACK
	value |= SoftI2CReadByte(1) << 8;											//Read HIGH data. Master will reply with ACK
	SoftI2CStop();																//Stop condition
	return value;
}

uint16_t ReadSensorDATA1()
{
	SoftI2CStart();																//Start
	SoftI2CWriteByte(SENSOR_ADDR);												//Slave address with WRITE
	SoftI2CWriteByte(SENSOR_REG_COMMAND | SENSOR_WORD | SENSOR_REG_DATA1LOW);	//Command code with address of register
	SoftI2CStart();														//Restart
	SoftI2CWriteByte(SENSOR_ADDR | 0x01);										//Slave address with READ
	uint16_t value = SoftI2CReadByte(1);										//Read LOW data. Master will reply with ACK
	value |= SoftI2CReadByte(1) << 8;											//Read HIGH data. Master will reply with ACK
	SoftI2CStop();																//Stop condition
	return value;
}

uint16_t GetSensorData(uint8_t channel)
{
	if (channel == 0)
	{
		return ReadSensorDATA0();
	}

	else if (channel == 1)
	{
		return ReadSensorDATA1();
	}
	else
	{
		return 0;
	}
}

void SetSensorGain_1()
{
	//Get register contents to preserve other values
	uint8_t regContents = ReadSensorRegister(SENSOR_REG_TIMING);

	//Set sensor gain to 1x
	WriteSensorRegister(SENSOR_REG_TIMING, regContents & ~_BV(4));	//Set GAIN bit to 0

	SENSOR_GAIN = 1;
}

void SetSensorGain_16()
{
	//Get register contents to preserve other values
	uint8_t regContents = ReadSensorRegister(SENSOR_REG_TIMING);

	//Set sensor gain to 16x
	WriteSensorRegister(SENSOR_REG_TIMING, regContents | _BV(4));	//Set GAIN bit to 1

	SENSOR_GAIN = 16;
}

void SetSensorADCIntegTime(uint8_t SENSOR_ADC_INTEG_TIME)
{
	//Get register contents to preserve other values
	uint8_t regContents = ReadSensorRegister(SENSOR_REG_TIMING);

	//Set sensor gain to 16x
	WriteSensorRegister(SENSOR_REG_TIMING, regContents & SENSOR_ADC_INTEG_TIME);	//Set ADC integration time
}

void StartSensorADC()
{
	uint8_t regContents = 0x03;

	if (SENSOR_GAIN == 16)
	{
		regContents |= 0x10;
	}

	//We are using manual timing. Need to set the bits in TIMING register. (Done one after other just in case there are problems with MANUAL being set to 1 before INTEG is set to 11)
	WriteSensorRegister(SENSOR_REG_TIMING, regContents | 0x08);		//set MANUAL bit to 1 to begin new conversion
}

void StopSensorADC()
{
	//Clear the timing register, but leave in manual mode
	WriteSensorRegister(SENSOR_REG_TIMING, 0x03);	//set MANUAL bit to 0
}

void SetADCManualMode()
{
	//set INTEG bits to 11, MANUAL bit to 0
	uint8_t regContents = 0x03;

	if (SENSOR_GAIN == 16)
	{
		regContents |= 0x10;
	}

	WriteSensorRegister(SENSOR_REG_TIMING, regContents);
}









