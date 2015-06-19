#include <avr/io.h>
#include <avr/interrupt.h>
#include <avr/pgmspace.h>
#include <util/delay.h>
#include <stdlib.h>
#include "usart.h"

//Define macros
#define set(port, pin) (port |= _BV(pin))
#define clr(port, pin) (port &= ~_BV(pin))
#define toggle(port, pin) (port ^= _BV(pin))

//Define ports and pins
#define LED_PORT			PORTB
#define LED_PCB				PB5
#define LED850				PB4
#define LED770				PB3

#define SENSOR_PORT			PORTC
#define SENSOR				PC0

//Define constants
#define UART_BAUD_RATE		2000000

uint16_t sensorValue850;
uint16_t sensorValue770;

uint16_t sensor_read()
{
	ADCSRA |= _BV(ADSC);				//Start conversion
	while ( (ADCSRA & _BV(ADSC)) );		//Wait for conversion to complete
	return ADC;
}

void Setup()
{
	//Set inputs
	DDRC &= ~_BV(SENSOR);

	//Set outputs
	DDRB |= _BV(LED770) | _BV(LED850) | _BV(LED_PCB);

	//Initialize ADC
	ADMUX |=  _BV(REFS1) | _BV(REFS0);									//Select Vref=Vcc (1.1V), channel = sensor pin (PC0)
	ADCSRA |= _BV(ADPS2) | _BV(ADPS0) | _BV(ADEN); 						//Set prescaler to 32 and enable ADC (allows sample to be taken in < 1ms)

	//Initialize UART
	usart_init(USART_BAUD_SELECT_DOUBLE_SPEED(UART_BAUD_RATE,F_CPU));

	sei();
}

#define CMD_LEDSTATE		0x4C
#define CMD_SENSORREAD		0x53
#define CMD_ADCCONFIG		0x41

void SetLEDState(uint8_t state, uint16_t address)
{
	if (address == 1)
	{
		if (state)
		{
			set(LED_PORT, LED770);
		}
		else
		{
			clr(LED_PORT, LED770);
		}
	}
	if (address == 2)
	{
		if (state)
		{
			set(LED_PORT, LED850);
		}
		else
		{
			clr(LED_PORT, LED850);
		}
	}
}

uint16_t GetSensorValue(uint16_t address)
{
	if (address == 1)
	{
		return sensor_read();
	}
	return 0;
}

void SetADCConfig(uint8_t prescaler, uint8_t vref)
{
	//Set prescaler
	prescaler &= 0x07;		//Prevent 'prescaler' overwriting other bits in register
	ADCSRA &= (0x18);		//Clear prescaler bits
	ADCSRA |= prescaler;	//Apply prescaler bits

	//Set Vref
	if (vref == 1)
	{
		//Set ADC Vref to 5.0V
		ADMUX &= ~(_BV(REFS1) | _BV(REFS0));
	}
	else
	{
		//Set ADC Vref to 1.1V
		ADMUX |= _BV(REFS1) | _BV(REFS0);
	}
}

int main(void)
{
	//Initialize MCU
	Setup();

	while(1)
	{
		//Wait for instruction from PC
		uint8_t nextByte = usart_receive();

		//Check for commands as per protocol of device
		if (nextByte == CMD_LEDSTATE)
		{
			//Extract LED address and STATE from next bytes
			uint8_t byteHigh = usart_receive();
			uint8_t byteLow = usart_receive();

			uint8_t LEDState = (byteHigh >> 7);
			uint16_t LEDAddress = ((byteHigh & 0x7F) << 8);
			LEDAddress |= byteLow;

			//Apply the new state to the specified LED
			SetLEDState(LEDState, LEDAddress);

			//Send acknowledgement to PC
			usart_send(CMD_LEDSTATE);
		}

		else if (nextByte == CMD_SENSORREAD)
		{
			//Extract sensor address from next bytes
			uint16_t sensorAddress = (usart_receive() << 8);
			sensorAddress |= usart_receive();

			uint16_t sensorValue = GetSensorValue(sensorAddress);

			//Send the data to PC
			usart_send(CMD_SENSORREAD);
			usart_send(sensorValue >> 8);
			usart_send((uint8_t)sensorValue);
		}

		else if (nextByte == CMD_ADCCONFIG)
		{
			//Extract ADC config from next byte
			uint8_t adcConfig = usart_receive();
			uint8_t vref = (adcConfig >> 7);
			uint8_t prescaler = adcConfig & 0x07;

			//Set the new ADC configurations
			SetADCConfig(prescaler, vref);

			//Send acknowledgement to PC
			usart_send(CMD_ADCCONFIG);
		}
	}
}

