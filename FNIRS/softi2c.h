/*
 * softi2c.h
 *
 *  Created on: 16/07/2015
 *      Author: Joseph
 */

#ifndef SOFTI2C_H_
#define SOFTI2C_H_

#include <avr/io.h>
#include <util/delay.h>

#define NOP() do { __asm__ __volatile__ ("nop"); } while (0)

#define I2C_READ	1
#define I2C_WRITE	0

#define I2C_PORT 	PORTC
#define I2C_PIN		PINC
#define I2C_DDR 	DDRC
#define SDA 		PC2
#define SCL 		PC3

#define I2C_DELAY()  {_delay_us(1.4); NOP();}	//Gives perfect 400 KHz clock signal on atmega328p
#define I2C_READ 		1
#define I2C_WRITE 		0

extern void SoftI2CMasterInit(void);
extern void SoftI2CMasterDeInit(void);
extern void SoftI2cMasterStop(void);

extern uint8_t SoftI2cMasterRead(uint8_t last);
extern uint8_t SoftI2CMasterWrite(uint8_t data);
extern uint8_t SoftI2CMasterWrite(uint8_t data);
extern uint8_t SoftI2CMasterStart(uint8_t addressRW);
extern uint8_t SoftI2CMasterRestart(uint8_t addressRW);
extern uint8_t soft_i2c_eeprom_read_byte(uint8_t deviceAddr, uint16_t readAddress);
extern uint8_t soft_i2c_eeprom_write_byte(uint8_t deviceAddr, uint16_t writeAddress, uint8_t writeByte);

#endif /* SOFTI2C_H_ */
