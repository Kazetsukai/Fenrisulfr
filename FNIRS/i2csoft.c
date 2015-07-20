/**********************************************************

Software I2C Library for AVR Devices.

Copyright 2008-2012
eXtreme Electronics, India
www.eXtremeElectronics.co.in
**********************************************************/

#include "i2csoft.h"

#define L_DELAY _delay_us(2);
#define Q_DELAY _delay_us(1);

void SoftI2CInit()
{
	SOFT_I2C_SDA_OUTPUT;	//Make sure master is in control
	SOFT_I2C_SCL_OUTPUT;

	//Make sure SDA and SCL idle at high level
	SOFT_I2C_SDA_HIGH;
	SOFT_I2C_SCL_HIGH;
}

void SoftI2CStart()
{
	//START: A HIGH to LOW transition on the SDA line while SCL is HIGH
	SOFT_I2C_SDA_HIGH;
	Q_DELAY;
	SOFT_I2C_SCL_HIGH;
	L_DELAY;
	SOFT_I2C_SDA_LOW;
	Q_DELAY;
}

void SoftI2CRepeatedStart()
{
	SOFT_I2C_SCL_HIGH;
	Q_DELAY;
	SOFT_I2C_SDA_LOW;
}

void SoftI2CStop()
{
	 SOFT_I2C_SDA_LOW;
	 Q_DELAY;
	 SOFT_I2C_SCL_HIGH;
	 L_DELAY;
	 SOFT_I2C_SDA_HIGH;
	 Q_DELAY;
}

uint8_t SoftI2CWriteByte(uint8_t data)
{
	//Bits are when SCL is high
	 uint8_t i;

	 for(i = 0; i < 8; i++)
	 {
		//Set SCL low during processing
		SOFT_I2C_SCL_LOW;

		//Then prepare SDA with desired data
		if(data & 0x80)
		{
			SOFT_I2C_SDA_HIGH;
		}
		else
		{
			SOFT_I2C_SDA_LOW;
		}

		L_DELAY;

		//Set SCL low to send the SDA bit
		SOFT_I2C_SCL_HIGH;
		Q_DELAY;

		data = data << 1;
	}

	SOFT_I2C_SCL_LOW;
	SOFT_I2C_SDA_INPUT;						//Release SDA to slave
	L_DELAY;

	SOFT_I2C_SCL_HIGH;						//Slave now detects SLC going high then low and releases SDA
	uint8_t ack = !(SDAPIN & _BV(SDA));		//Check for ACK
	Q_DELAY;

	SOFT_I2C_SCL_LOW;

	 SOFT_I2C_SDA_HIGH;						//Enable pullups
	 SOFT_I2C_SDA_OUTPUT;					//Return SDA control to master

	return ack;
}

uint8_t SoftI2CReadByte(uint8_t ack)
{
	uint8_t data = 0;
	uint8_t i;

	//Release SDA from master to slave
	SOFT_I2C_SDA_INPUT;

	for(i = 0; i < 8; i++)
	{
		SOFT_I2C_SCL_LOW;
		L_DELAY;
		SOFT_I2C_SCL_HIGH;

		if(SDAPIN & _BV(SDA))
		{
			data |= (0x80 >> i);
		}

		Q_DELAY;
	}

	//Master sends ack
	SOFT_I2C_SCL_LOW;
	L_DELAY;
	SOFT_I2C_SDA_OUTPUT;	//Return SDA control to master

	if(ack)
	{
		SOFT_I2C_SDA_LOW;
	}
	else
	{
		SOFT_I2C_SDA_HIGH;
	}

	SOFT_I2C_SCL_HIGH;	//Send ack by setting SCL HIGH
	Q_DELAY;

	return data;

}















