/**********************************************************

Software I2C Library for AVR Devices.

Copyright 2008-2012
eXtreme Electronics, India
www.eXtremeElectronics.co.in
**********************************************************/


#ifndef _I2CSOFT_H
#define _I2CSOFT_H

#include <avr/io.h>
#include <util/delay.h>

/*
I/O Configuration
*/

#define SCLPORT	PORTC	//TAKE PORTC as SCL OUTPUT WRITE
#define SCLDDR	DDRC	//TAKE DDRC as SCL INPUT/OUTPUT configure

#define SDAPORT	PORTC	//TAKE PORTC as SDA OUTPUT WRITE
#define SDADDR	DDRC	//TAKE PORTC as SDA INPUT configure

#define SDAPIN	PINC	//TAKE PORTC TO READ DATA
#define SCLPIN	PINC	//TAKE PORTC TO READ DATA

#define SCL	PC3			//PORTC.3 PIN AS SCL PIN
#define SDA	PC2			//PORTC.2 PIN AS SDA PIN

/*
#define SOFT_I2C_SDA_LOW	SDADDR |= ((1<<SDA))
#define SOFT_I2C_SDA_HIGH	SDADDR &= (~(1<<SDA))

#define SOFT_I2C_SCL_LOW	SCLDDR |= ((1<<SCL))
#define SOFT_I2C_SCL_HIGH	SCLDDR &= (~(1<<SCL))
*/

#define SOFT_I2C_SDA_OUTPUT	SDADDR |= ((1<<SDA))
#define SOFT_I2C_SDA_INPUT	SDADDR &= (~(1<<SDA))

#define SOFT_I2C_SCL_OUTPUT	SCLDDR |= ((1<<SCL))
#define SOFT_I2C_SCL_INPUT	SCLDDR &= (~(1<<SCL))

#define SOFT_I2C_SDA_LOW	SDAPORT &= ~_BV(SDA)
#define SOFT_I2C_SDA_HIGH	SDAPORT |= _BV(SDA)

#define SOFT_I2C_SCL_LOW	SCLPORT &= ~_BV(SCL)
#define SOFT_I2C_SCL_HIGH	SCLPORT |= _BV(SCL)


void SoftI2CInit();
void SoftI2CStart();
void SoftI2CRepeatedStart();
void SoftI2CStop();
uint8_t SoftI2CWriteByte(uint8_t data);
uint8_t SoftI2CReadByte(uint8_t ack);


#endif
