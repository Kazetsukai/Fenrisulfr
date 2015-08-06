/*
 * TLS25911.c
 *
 *  Created on: 16/07/2015
 *      Author: Joseph
 */

#include "TLS25911.h"

void SensorEnable()
{
	WriteSensorRegister(SENSOR_COMMAND_BIT | SENSOR_REG_ENABLE,  SENSOR_ENABLE_POWERON | SENSOR_ENABLE_AEN);	//Enable ALS and power on without interrupts
}

void SensorDisable()
{
	WriteSensorRegister(SENSOR_COMMAND_BIT | SENSOR_REG_ENABLE,  SENSOR_ENABLE_POWEROFF);
}

void SensorInit()
{
	SoftI2CInit();
	SensorEnable();
}

uint8_t WriteSensorRegister(uint8_t address, uint8_t data)
{
	//Returns 1 if succesful, or 0 if no ACKs were received
	uint8_t ack = 0;

	SoftI2CStart();											//Start
	ack = SoftI2CWriteByte(SENSOR_ADDR);					//Slave address with WRITE
	if (!ack) {return 0;}
	ack = SoftI2CWriteByte(address);						//Send address of register
	if (!ack) {return 0;}
	ack = SoftI2CWriteByte(data);							//Write data to specified register
	if (!ack) {return 0;}
	SoftI2CStop();											//Stop condition

	return 1;
}

uint8_t ReadSensorRegister(uint8_t address)
{
	//Returns value if succesful, or 0 if no ACKs were received
	uint8_t ack = 0;

	SoftI2CStart();											//Start
	ack = SoftI2CWriteByte(SENSOR_ADDR);					//Slave address with WRITE
	if (!ack) {return 0;}
	ack = SoftI2CWriteByte(0x80 | 0x20 | address);		 	//Send address of register with command bit, normal mode
	if (!ack) {return 0;}
	SoftI2CRepeatedStart();									//Restart
	SoftI2CWriteByte(SENSOR_ADDR | 0x01);					//Slave address with READ
	if (!ack) {return 0;}
	uint8_t value = SoftI2CReadByte(1);						//Read 1 byte to get data. Master will reply with ACK
	SoftI2CStop();											//Stop condition
	return value;
}

uint8_t SensorCommsAreWorking()
{
	//Read id register to confirm sensor is communicating properly. Value should be 0x50
	uint8_t id = ReadSensorRegister(SENSOR_REG_ID);

	if (id == 0x50)
	{
		return 1;
	}
	else
	{
		return 0;
	}
}

uint16_t ReadSensorCH0()
{
	uint16_t value = ReadSensorRegister(SENSOR_REG_C0DATAL);
	value |= ReadSensorRegister(SENSOR_REG_C0DATAH) << 8;
	return value;
}

uint16_t ReadSensorCH1()
{
	uint16_t value = ReadSensorRegister(SENSOR_REG_C1DATAL);
	value |= ReadSensorRegister(SENSOR_REG_C1DATAH) << 8;
	return value;
}

uint8_t SetSensorControl(uint8_t GAIN_OR_ATIME)
{
	//Set sensor gain
	SensorEnable();
	uint8_t status = WriteSensorRegister(SENSOR_COMMAND_BIT | SENSOR_REG_CONTROL, GAIN_OR_ATIME);
	SensorDisable();
	return status;
}

/*
double GetLuxReading()
{
	double lux = 0;

	//Info taken from datasheet (page 4) to calculate lux for this sensor. Relies on data from both ADC channels
	uint16_t channel0Value = ReadSensorCH0();
	uint16_t channel1Value = ReadSensorDATA1();

	double channelRatio = channel1Value / channel0Value;

	if (channelRatio <= 0.50)
	{
		lux = (0.0304 * channel0Value) - (0.062 *  channel0Value * pow(channelRatio, 1.4));
	}

	else if  (channelRatio <= 0.61)
	{
		lux = (0.0224 * channel0Value) - (0.031 *  channel1Value);
	}

	else if  (channelRatio <= 0.80)
	{
		lux = (0.0128 * channel0Value) - (0.0153 *  channel1Value);
	}
	else if  (channelRatio <= 1.30)
	{
		lux = (0.00146 * channel0Value) - (0.00112 *  channel1Value);
	}
	else if  (channelRatio > 1.30)
	{
		lux = 0;
	}

	return lux;
}
*/







