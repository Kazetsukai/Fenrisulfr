/*
 * APDS-9301.h
 *
 *  Created on: 16/07/2015
 *      Author: Joseph
 */

#ifndef APDS_9301_H_
#define APDS_9301_H_

#include "i2csoft.h"

#define SENSOR_ADDR 				0x52		//Shifted 1 bit to account for R/W bit
#define SENSOR_REG_COMMAND			0x80		//Specifies register address
#define SENSOR_REG_CONTROL			0x00		//Control of basic functions
#define SENSOR_REG_TIMING			0x01		//Integration time/gain control
#define SENSOR_REG_THRESHLOWLOW		0x02		//Low byte of low interrupt threshold
#define SENSOR_REG_THRESHLOWHIGH	0x03		//High byte of low interrupt threshold
#define SENSOR_REG_THRESHHIGHLOW	0x04		//Low byte of high interrupt threshold
#define SENSOR_REG_THRESHHIGHHIGH	0x05		//High byte of high interrupt threshold
#define SENSOR_REG_INTERRUPT		0x06		//Interrupt control
#define SENSOR_REG_ID 				0x0A		//Part number/ Rev ID
#define SENSOR_REG_DATA0LOW			0x0C		//Low byte of ADC channel 0
#define SENSOR_REG_DATA0HIGH		0x0D		//High byte of ADC channel 0
#define SENSOR_REG_DATA1LOW			0x0E		//Low byte of ADC channel 1
#define SENSOR_REG_DATA1HIGH		0x0F		//High byte of ADC channel 1

#define SENSOR_WORD					0x20

#define SENSOR_ADC_INTEG_TIME_14ms	0x00		//ADC scales to 0.034
#define SENSOR_ADC_INTEG_TIME_101ms	0x01		//ADC scales to 0.252
#define SENSOR_ADC_INTEG_TIME_402ms	0x10		//ADC scales to 1


extern void SensorInit();

extern void SensorPowerUp();
extern uint8_t SensorCommsAreWorking();

extern void WriteSensorRegister(uint8_t address, uint8_t data);
extern uint8_t ReadSensorRegister(uint8_t address);

extern uint16_t GetSensorData(uint8_t channel);

extern void SetSensorGain_1();
extern void SetSensorGain_16();

extern void SetSensorADCIntegTime(uint8_t SENSOR_ADC_INTEG_TIME);

extern void StartSensorADC();
extern void StopSensorADC();

extern void RestartADC();

extern uint16_t ReadSensorDATA0();
extern void SetADCManualMode();
#endif /* APDS_9301_H_ */
