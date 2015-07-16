
#include "softi2c.h"

// Initialize SCL/SDA pins and set the bus high
void SoftI2CMasterInit(void) {
	I2C_DDR |= _BV(SDA);
	I2C_PORT |= _BV(SDA);
	I2C_DDR |= _BV(SCL);
	I2C_PORT |= _BV(SCL);
}

// De-initialize SCL/SDA pins and set the bus low
void SoftI2CMasterDeInit(void) {
	I2C_PORT &= ~_BV(SDA);
	I2C_DDR &= ~_BV(SDA);
	I2C_PORT &= ~_BV(SCL);
	I2C_DDR &= ~_BV(SCL);
}

// Read a uint8_t from I2C and send Ack if more reads follow else Nak to terminate read
uint8_t SoftI2cMasterRead(uint8_t isFinalReadByte) {
	uint8_t b = 0;
	// Make sure pull-up enabled
	I2C_PORT |= _BV(SDA);
	I2C_DDR &= ~_BV(SDA);
	// Read uint8_t
	for (uint8_t i = 0; i < 8; i++) {
		// Don't change this loop unless you verify the change with a scope
		b <<= 1;
		I2C_DELAY();
		I2C_PORT |= _BV(SCL);
		if (bit_is_set(I2C_PIN, SDA)) b |= 1;
		I2C_PORT &= ~_BV(SCL);
	}
	// Send Ack or Nak
	I2C_DDR |= _BV(SDA);
	if (isFinalReadByte) {
		I2C_PORT |= _BV(SDA);
	}
	else {
		I2C_PORT &= ~_BV(SDA);
	}
	I2C_PORT |= _BV(SCL);
	I2C_DELAY();
	I2C_PORT &= ~_BV(SCL);
	I2C_PORT &= ~_BV(SDA);
	return b;
}

// Write a uint8_t to I2C
uint8_t SoftI2CMasterWrite(uint8_t data)
{
	for (uint8_t m = 0x80; m != 0; m >>= 1)
	{
		if (m & data)
		{
			I2C_PORT |= _BV(SDA);
		}
		else
		{
			I2C_PORT &= ~_BV(SDA);
		}

		I2C_PORT |= _BV(SCL);
		I2C_DELAY();
		I2C_PORT &= ~_BV(SCL);
	}

	I2C_DDR &= ~_BV(SDA);	// Set SDA as input (free it to slave)
	I2C_PORT |= _BV(SDA); 	// Enable pullup for slave SDA

	I2C_PORT |= _BV(SCL);	// Master clocks one cycle to retrieve the ACK or NACK
	uint8_t rtn = bit_is_set(I2C_PIN, SDA);	//Get ACK/NACK
	I2C_PORT &= ~_BV(SCL);	// end of ACK/NACK clock

	I2C_DDR |= _BV(SDA);	//Return data bus control to master. Set SDA as output
	I2C_PORT &= ~_BV(SDA);	//Return data bus to idle
	return rtn == 0;
}

// Issue a start condition
uint8_t SoftI2CMasterStart(uint8_t addressRW)
{
	I2C_PORT &= ~_BV(SDA);
	I2C_DELAY();
	I2C_PORT &= ~_BV(SCL);
	return SoftI2CMasterWrite(addressRW);
}

// Issue a restart condition
uint8_t SoftI2CMasterRestart(uint8_t addressRW) {
	I2C_PORT |= _BV(SDA);
	I2C_PORT |= _BV(SCL);
	I2C_DELAY();
	return SoftI2CMasterStart(addressRW);
}

// Issue a stop condition
void SoftI2cMasterStop(void) {
	I2C_PORT &= ~_BV(SDA);
	I2C_DELAY();
	I2C_PORT |= _BV(SCL);
	I2C_DELAY();
	I2C_PORT |= _BV(SDA);
	I2C_DELAY();
}

// Read 1 uint8_t from the EEPROM device and return it
uint8_t soft_i2c_eeprom_read_uint8_t(uint8_t deviceAddr, uint16_t readAddress) {
	uint8_t uint8_tRead = 0;

	// Issue a start condition, send device address and write direction bit
	if (!SoftI2CMasterStart((deviceAddr<<1) | I2C_WRITE)) return 0;

	// Send the address to read, 8 bit or 16 bit
	if (readAddress > 255) {
		if (!SoftI2CMasterWrite((readAddress >> 8))) return 0; // MSB
		if (!SoftI2CMasterWrite((readAddress & 0xFF))) return 0; // LSB
	}
	else {
		if (!SoftI2CMasterWrite(readAddress)) return 0; // 8 bit
	}

	// Issue a repeated start condition, send device address and read direction bit
	if (!SoftI2CMasterRestart((deviceAddr<<1) | I2C_READ)) return 0;

	// Read the uint8_t
	uint8_tRead = SoftI2cMasterRead(1);

	// Issue a stop condition
	SoftI2cMasterStop();

	return uint8_tRead;
}

// Write 1 uint8_t to the EEPROM
uint8_t soft_i2c_eeprom_write_uint8_t(uint8_t deviceAddr, uint16_t writeAddress, uint8_t writeuint8_t) {

	// Issue a start condition, send device address and write direction bit
	if (!SoftI2CMasterStart((deviceAddr<<1) | I2C_WRITE)) return 0;

	// Send the address to write, 8 bit or 16 bit
	if ( writeAddress > 255) {
		if (!SoftI2CMasterWrite((writeAddress >> 8))) return 0; // MSB
		if (!SoftI2CMasterWrite((writeAddress & 0xFF))) return 0; // LSB
	}
	else {
		if (!SoftI2CMasterWrite(writeAddress)) return 0; // 8 bit
	}

	// Write the uint8_t
	if (!SoftI2CMasterWrite(writeuint8_t)) return 0;

	// Issue a stop condition
	SoftI2cMasterStop();

	// Delay 10ms for the write to complete, depends on the EEPROM you use
	_delay_ms(10);

	return 1;
}

// Read more than 1 uint8_t from a device
// (Optional)
/*uint8_t I2cReaduint8_ts(uint8_t deviceAddr, uint8_t readAddress, uint8_t *readBuffer, uint8_t uint8_tstoRead) {
	// Issue a start condition, send device address and write direction bit
	if (!SoftI2cMasterStart((deviceAddr<<1) | I2C_WRITE)) return 0;

	// Send the address to read
	if (!SoftI2cMasterWrite(readAddress)) return 0;

	// Issue a repeated start condition, send device address and read direction bit
	if (!SoftI2cMasterRestart((deviceAddr<<1) | I2C_READ)) return 0;

	// Read data from the device
	for (uint8_t i = 0; i < uint8_tstoRead; i++) {
		// Send Ack until last uint8_t then send Ack
		readBuffer[i] = SoftI2cMasterRead(i == (bytestoRead-1));
	}

	// Issue a stop condition
	SoftI2cMasterStop();

	return 1;
}*/
