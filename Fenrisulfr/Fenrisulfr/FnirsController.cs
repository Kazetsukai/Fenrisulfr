﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fenrisulfr.Exceptions;

namespace Fenrisulfr
{
    public class FnirsController
    {
        private readonly static byte[] RequestValueCommandPacket = { 0x53, 0x00, 0x00 };
        private readonly static byte[] SetLEDCommandPacket = { 0x4C, 0x00, 0x00 };
        private readonly static byte[] ReturnData = new byte[3];

        private SerialPort _serialPort = new SerialPort(Properties.Settings.Default.DeviceCOMPort, 2000000, Parity.Even, 8, StopBits.One);
        private ConcurrentQueue<SensorResult> _results = new ConcurrentQueue<SensorResult>();
        private Task _readerThread;
        private Stopwatch _stopwatch = new Stopwatch();
        private bool _stopping = false;
        private FnirsControllerState _state = FnirsControllerState.Stopped;

        private int _samplePeriod_ms = 100;

        int sensorValue770;
        int sensorValue850;

        public void SetSampleRate(double SampleRateHz)
        {
            _samplePeriod_ms = (int)(1000 / SampleRateHz);
            Console.WriteLine("Sample period set to " + _samplePeriod_ms.ToString() + " ms. (Sample rate = " + SampleRateHz.ToString() + " Hz)");
        }

        public FnirsControllerState GetState()
        {
            return _state;
        }

        public FnirsController()
        {
        }

        public void Start()
        {
            _state = FnirsControllerState.Running;
            _results = new ConcurrentQueue<SensorResult>();

            if (_readerThread == null)
            {
                _readerThread = Task.Run(() =>
                {
                    _stopwatch.Start();

                    while (!_stopping)
                    {
                        try
                        {
                            if (!_serialPort.IsOpen)
                            {
                                Console.WriteLine("Opening serial port: " + Properties.Settings.Default.DeviceCOMPort);
                                _serialPort.Open();

                                //Clear LEDs
                                SetLEDState(1, LEDState.Off);
                                SetLEDState(2, LEDState.Off);
                            }

                            while (true)
                            {
                                DoWork();
                                Thread.Sleep(_samplePeriod_ms);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Exception on reader thread: " + ex.Message);
                        }
                    }
                });
            }
            else
            {
                Console.WriteLine("Already started!");
            }
        }

        public void Stop()
        {
            _stopping = true;
            
            _readerThread.Wait();

            _stopping = false;
            _readerThread = null;

            Console.WriteLine("Closing serial port: " + Properties.Settings.Default.DeviceCOMPort);
            _serialPort.Close();
            _state = FnirsControllerState.Stopped;
        }

        public int ResultsInQueue { get { return _results.Count; } }

        /// <summary>
        /// This special method throws an exception if you call it without starting the controller. That is its main purpose.
        /// It might also return you sensor values if you ask nicely.
        /// </summary>
        /// <returns></returns>
        public SensorResult GetNextResult()
        {
            SensorResult result;

            // Try 200 times, because why not?
            // If it fails 200 times in a row, tough luck.
            for (int i = 0; i < 200; i++)
            {
                if (_results.TryDequeue(out result))
                    return result;

                Thread.Sleep(1);
            }
            
            throw new ComputerSaysNoException("This is ridiculous, if you aren't going to give me a value then I am outta here. Screw you.");
        }

        void DoWork()
        {
            //Flash leds and get data
            SetLEDState(1, LEDState.On);
            sensorValue770 = RequestSensorValue(1);
            SetLEDState(1, LEDState.Off);

            SetLEDState(2, LEDState.On);
            sensorValue850 = RequestSensorValue(1);
            SetLEDState(2, LEDState.Off);

            _results.Enqueue(new SensorResult { Read770 = sensorValue770, Read850 = sensorValue850, Milliseconds = _stopwatch.ElapsedMilliseconds });
        }

        int RequestSensorValue(ushort address)
        {
            //Clear serial port in buffer
            _serialPort.DiscardInBuffer();

            //Prepare command packet
            RequestValueCommandPacket[1] = (byte)(address >> 7);
            RequestValueCommandPacket[2] = (byte) address;

            //Send the packet to device
            _serialPort.DiscardInBuffer();
            _serialPort.Write(RequestValueCommandPacket, 0, 3);

            //Read value out
            _serialPort.Read(ReturnData, 0, 3);

            return (ReturnData[1] << 8) + (ReturnData[2]);
        }

        void SetLEDState(ushort address, LEDState state)
        {
            //Clear serial port in buffer
            _serialPort.DiscardInBuffer();

            SetLEDCommandPacket[0] = 0x4C;
            SetLEDCommandPacket[1] = 0;
            SetLEDCommandPacket[2] = 0; 

            //Inject state data to packet
            if (state == LEDState.On)
            {
                SetLEDCommandPacket[1] = 0x80;
            }

            //Inject address of specified LED
            SetLEDCommandPacket[1] |= (byte)(address >> 7);
            SetLEDCommandPacket[2] |= (byte)address;

            //Send the packet to device
            _serialPort.Write(SetLEDCommandPacket, 0, 3);

            //Get acknowledgement
            byte ack = (byte)_serialPort.ReadByte();
            
            if (ack != 0x4C)
            {
                throw new LEDStateChangeUnacknowledgedException("LED State change was requested by computer, but not acknowledged by device.");
            }
        }
    }
}
