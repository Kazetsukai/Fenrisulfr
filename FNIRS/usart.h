/*
 * usart.h
 *
 *  Created on: 19/06/2015
 *      Author: Joseph
 */

#ifndef USART_H_
#define USART_H_

#define USART_BAUD_SELECT(baudRate,xtalCpu) ((xtalCpu)/((baudRate)*16l)-1)
#define USART_BAUD_SELECT_DOUBLE_SPEED(baudRate,xtalCpu) (((xtalCpu)/((baudRate)*8l)-1)|0x8000)

extern void usart_init(unsigned int baudrate);
extern uint8_t usart_receive();
extern void usart_send(uint8_t data);
extern void usart_puts(char* StringPtr);

#endif /* USART_H_ */
