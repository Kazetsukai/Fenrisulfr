#include <avr/io.h>
#include <avr/interrupt.h>
#include <avr/pgmspace.h>

#include <util/delay.h>
#include <stdlib.h>
#include "usart.h"
#include "APDS-9301.h"

//Define macros
#define set(port, pin) (port |= _BV(pin))
#define clr(port, pin) (port &= ~_BV(pin))
#define toggle(port, pin) (port ^= _BV(pin))

//Define ports and pins
#define LED_PCB				PB5
#define LED940				PC5
#define LED770				PC4
#define DEBUG0				PC0
#define DEBUG1				PC1

//Define constants
#define UART_BAUD_RATE		2000000

volatile uint16_t timerElapsed_ms;
volatile uint16_t integTime_ms;

uint16_t sensorValue940;
uint16_t sensorValue770;
volatile uint16_t sensorValue;
volatile float irrad770;
volatile float irrad940;

uint16_t sensor_read()
{
	ADCSRA |= _BV(ADSC);				//Start conversion
	while ( (ADCSRA & _BV(ADSC)) );		//Wait for conversion to complete
	return ADC;
}

void Setup()
{
	//Set outputs
	DDRB |= _BV(LED_PCB);
	DDRC |= _BV(LED770) | _BV(LED940) | _BV(DEBUG0)| _BV(DEBUG1);

	//Initialize timer for counting ADC integration time
	TCNT1 = 0;									//Set initial timer value
	TCCR1B |= _BV(CS10);						//Start timer with prescale clkI/O / 32
	OCR1A = 15960;								//Tuned to 1ms compmatch interrupt
	TIMSK1 |= _BV(OCIE1A);						//Start timer1 interrupt on compmatch with OCR1A (counts every ms)

	//Initialize UART
	usart_init(USART_BAUD_SELECT_DOUBLE_SPEED(UART_BAUD_RATE,F_CPU));

	SensorInit();

	sei();
}

void SetLEDState(uint8_t LEDState, uint8_t LEDAddress)
{
	if (LEDState == 1)
	{
		if (LEDAddress == 0)
		{
			set(PORTC, LED770);
		}
		if (LEDAddress == 1)
		{
			set(PORTC, LED940);
		}
	}
	if (LEDState == 0)
	{
		if (LEDAddress == 0)
		{
			clr(PORTC, LED770);
		}
		if (LEDAddress == 1)
		{
			clr(PORTC, LED940);
		}
	}
}

#define CMD_LEDSTATE		0x4C
#define CMD_READ_CH			0x53
#define CMD_READ_Ee770		0x64
#define CMD_READ_Ee940		0x65
#define CMD_ADCCONFIG		0x41

void RestartIntegTimer()
{
	StopSensorADC();
	StartSensorADC();
	timerElapsed_ms = 0;
}

//This code will be run on the master mcu
float GetIrradiance770()
{
	uint16_t ch0 = GetSensorData(0);
	uint16_t ch1 = GetSensorData(1);

	return (0.06879 * ch0) + (0.09426 * ch1);
}

float GetIrradiance940()
{
	uint16_t ch0 = GetSensorData(0);
	uint16_t ch1 = GetSensorData(1);

	return (0.11464 * ch0) - (0.02242 * ch1);
}

int main(void)
{
	//Initialize MCU
	Setup();

	if (SensorCommsAreWorking())
	{
		for (int i = 0; i < 3; i++)
		{
			set(PORTB, LED_PCB);
			_delay_ms(100);
			clr(PORTB, LED_PCB);
			_delay_ms(100);
		}
	}

	SetSensorGain_16();
	SetSensorADCIntegTime(SENSOR_ADC_INTEG_TIME_402ms);

	//SetADCManualMode();
	//integTime_ms = 2;
	SetLEDState(1, 0);
	SetLEDState(1, 1);

	while(1)
	{
		/*
		while (1)
		{
			//baud rate is changed remember!!!!!!!!
			usart_puts("0,");

			sensorValue770 = GetSensorData(0);
			usart_puts(utoa(sensorValue770, 6, 10));
			usart_puts(",");

			sensorValue940 = GetSensorData(1);
			usart_puts(utoa(sensorValue940, 6, 10));
			usart_puts("\n\r");
		}*/

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
			RestartIntegTimer();	//Restart ADC

			//Send acknowledgement to PC
			usart_send(CMD_LEDSTATE);
		}

		else if (nextByte == CMD_READ_CH)
		{
			//Extract sensor address from next bytes
			uint16_t channel = (usart_receive() << 8);
			channel |= usart_receive();

			uint16_t data = GetSensorData(channel);

			usart_send(data >> 8);
			usart_send(data);

		}

		else if (nextByte == CMD_READ_Ee770)
		{
			irrad770 = GetIrradiance770();

			//Send the data to PC
			usart_send(CMD_READ_Ee770);		//Send command back
			usart_send_f((char*)&irrad770);

		}
		else if (nextByte == CMD_READ_Ee940)
		{
			irrad940 = GetIrradiance940();

			//Send the data to PC
			usart_send(CMD_READ_Ee940);		//Send command back
			usart_send_f((char*)&irrad940);
		}

		/*
		//Not working yet
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
		}*/
	}
}


ISR(TIMER1_COMPA_vect)
{
	/*
	TCNT1 = 0;
	timerElapsed_ms++;

	//Restart ADC when conversion time is reached
	if (timerElapsed_ms >= integTime_ms)
	{
		RestartIntegTimer();
	}*/
}


