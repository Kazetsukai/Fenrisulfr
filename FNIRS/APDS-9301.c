/*
 * APDS-9301.c
 *
 *  Created on: 16/07/2015
 *      Author: Joseph
 */

#include "APDS-9301.h"

void SensorInit()
{
	SoftI2CMasterInit();
}

void SensorPowerUp()
{
	WriteSensorRegister(SENSOR_REG_CONTROL, 0x03);
}

uint8_t SensorCommsAreWorking()
{
	//Power up the sensor. Can be used to confirm device is communicating properly if READ is done after power up and the value is 0x03.
	SensorPowerUp();

	if ((ReadSensorRegister(SENSOR_REG_CONTROL) & 0x03) == 0x03)
	{
		return 1;
	}
	else
	{
		return 0;
	}
}

void WriteSensorRegister(uint8_t address, uint8_t data)
{
	SoftI2CMasterInit();
	SoftI2CMasterStart(SENSOR_ADDR | I2C_WRITE);				//Start condition with WRITE
	SoftI2CMasterWrite(SENSOR_REG_COMMAND |  address);			//Command code with address of register
	SoftI2CMasterWrite(data);									//Write data to specified register
	SoftI2cMasterStop();										//Stop condition
	SoftI2CMasterDeInit();
}

uint8_t ReadSensorRegister(uint8_t address)
{
	uint8_t value = 0;

	SoftI2CMasterInit();
	SoftI2CMasterStart(SENSOR_ADDR | I2C_WRITE);				//Start condition with WRITE
	SoftI2CMasterWrite(SENSOR_REG_COMMAND |  address);			//Command code with address of register
	SoftI2CMasterRestart(SENSOR_ADDR | I2C_READ);				//Restart condition with READ
	value = SoftI2cMasterRead(1);								//Read 1 byte to get data. Master will reply with ACK
	SoftI2cMasterStop();										//Stop condition
	SoftI2CMasterDeInit();

	return value;
}

uint16_t GetSensorValue(uint8_t channel)
{
	if (channel == 0)
	{
		uint16_t data = ReadSensorRegister(SENSOR_REG_DATA0LOW);
		data |= (ReadSensorRegister(SENSOR_REG_DATA0HIGH) << 8);
		return data;
	}

	else if (channel == 1)
	{
		uint16_t data = ReadSensorRegister(SENSOR_REG_DATA1LOW);
		data |= (ReadSensorRegister(SENSOR_REG_DATA1HIGH) << 8);
		return data;
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
}

void SetSensorGain_16()
{
	//Get register contents to preserve other values
	uint8_t regContents = ReadSensorRegister(SENSOR_REG_TIMING);

	//Set sensor gain to 16x
	WriteSensorRegister(SENSOR_REG_TIMING, regContents | _BV(4));	//Set GAIN bit to 1
}

void SetSensorADCIntegTime(uint8_t SENSOR_ADC_INTEG_TIME)
{
	//Get register contents to preserve other values
	uint8_t regContents = ReadSensorRegister(SENSOR_REG_TIMING);

	//Set sensor gain to 16x
	WriteSensorRegister(SENSOR_REG_TIMING, regContents & SENSOR_ADC_INTEG_TIME);	//Set ADC integration time
}


