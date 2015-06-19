/*
 * usart.c
 *
 *  Created on: 19/06/2015
 *      Author: Joseph
 */

#include <avr/io.h>
#include <avr/interrupt.h>
#include <avr/pgmspace.h>
#include "usart.h"

void usart_init(unsigned int baudrate)
{
    //Set baud rate
    if (baudrate & 0x8000)
    {
    	UCSR0A = _BV(U2X0);  //Enable 2x speed
   		baudrate &= ~0x8000;
   	}
    UBRR0H = (unsigned char)(baudrate>>8);
    UBRR0L = (unsigned char) baudrate;

    //Enable USART receiver and transmitter
    UCSR0B = _BV(RXEN0) | _BV(TXEN0);

    //Set frame format: asynchronous, Even parity, 1 stop bit,
    UCSR0C = _BV(UPM01) | _BV(UCSZ00) | _BV(UCSZ01);
}

uint8_t usart_receive()
{
	 while(!(UCSR0A & (1<<RXC0)));
	 return UDR0;
}

void usart_send(uint8_t data)
{
	//Wait until there is space in the tx buffer, then send
	 while(!(UCSR0A & _BV(UDRE0)));
	 UDR0 = data;
}

void usart_puts(char* StringPtr)
{
   //Here we check if there is still more chars to send, this is done checking the actual char and see if it is different from the null char
	while(*StringPtr != 0x00)
	{
		usart_send(*StringPtr);    //Using the simple send function we send one char at a time
		StringPtr++;			   //We increment the pointer so we can read the next char
	}
}
