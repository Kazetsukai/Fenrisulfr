/*
 * APDS-9301.h
 *
 *  Created on: 16/07/2015
 *      Author: Joseph
 */

#ifndef TLS25911_H_
#define TLS25911_H_

#include <math.h>
#include "i2csoft.h"

#define SENSOR_ADDR 				0x52	//Address is 0x29. This is shifted left 1 bit in preparation for R\W.
#define SENSOR_REG_ENABLE			0x00
#define SENSOR_REG_CONTROL			0x01
#define SENSOR_REG_PID				0x11
#define SENSOR_REG_ID				0x12
#define SENSOR_REG_STATUS			0x13
#define SENSOR_REG_C0DATAL			0x14
#define SENSOR_REG_C0DATAH			0x15
#define SENSOR_REG_C1DATAL			0x16
#define SENSOR_REG_C1DATAH			0x17

#define SENSOR_COMMAND_BIT			0xA0	//bits 7 and 5 for 'command normal'
#define SENSOR_CLEAR_BIT         	0x40    // Clears any pending interrupt (write 1 to clear)
#define SENSOR_WORD_BIT          	0x20    // 1 = read/write word (rather than byte)
#define SENSOR_BLOCK_BIT         	0x10    // 1 = using block read/write

#define SENSOR_ENABLE_POWERON    	0x01
#define SENSOR_ENABLE_POWEROFF   	0x00
#define SENSOR_ENABLE_AEN        	0x02
#define SENSOR_ENABLE_AIEN       	0x10

#define SENSOR_GAIN_LOW				0x00	//1x
#define SENSOR_GAIN_MED				0x10	//25x
#define SENSOR_GAIN_HIGH			0x20	//428x
#define SENSOR_GAIN_MAX				0x30	//9876x

#define SENSOR_ATIME_100ms			0
#define SENSOR_ATIME_200ms			1
#define SENSOR_ATIME_300ms			2
#define SENSOR_ATIME_400ms			3
#define SENSOR_ATIME_500ms			4
#define SENSOR_ATIME_600ms			5

extern void SensorInit();
extern void SensorEnable();
extern void SensorDisable();

extern uint8_t SensorCommsAreWorking();

extern uint8_t WriteSensorRegister(uint8_t address, uint8_t data);
extern uint8_t ReadSensorRegister(uint8_t address);

extern uint8_t SetSensorControl(uint8_t GAIN_OR_ATIME);

extern uint16_t ReadSensorCH0();
extern uint16_t ReadSensorCH1();

#endif /* TLS25911_H_ */
