using System;
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
        private readonly static byte[] SetLEDCommandPacket = { 0x4C, 0x00, 0x00 };
        private readonly static byte[] RequestCH0CommandPacket = { 0x54 };
        private readonly static byte[] RequestCH1CommandPacket = { 0x55 };
        private readonly static byte[] RequestIrradiance770ValueCommandPacket = { 0x64 };
        private readonly static byte[] RequestIrradiance940ValueCommandPacket = { 0x65 };  
        private readonly static byte[] ReturnData = new byte[5];

        private SerialPort _serialPort = new SerialPort(Properties.Settings.Default.DeviceCOMPort, 2000000, Parity.Even, 8, StopBits.One);
        private ConcurrentQueue<SensorResult> _results = new ConcurrentQueue<SensorResult>();
        private Task _readerThread;
        private Stopwatch _stopwatch = new Stopwatch();
        private bool _stopping = false;
        private FnirsControllerState _state = FnirsControllerState.Stopped;

        private int timeOfLastReceive;

        private int _samplePeriod_ms = 100;

        private bool _serialPortBusy = false;

        float sensorValue770;
        float sensorValue940;
        float ch0;
        float ch1;

        public void Reset()
        {
            _stopwatch.Reset();
            _results = new ConcurrentQueue<SensorResult>();
        }

        public void SetSampleRate(double SampleRateHz)
        {
            _samplePeriod_ms = (int)(1000 / SampleRateHz);
            Console.WriteLine("Sample period set to " + _samplePeriod_ms.ToString() + " ms. (Sample rate = " + SampleRateHz.ToString() + " Hz)");
        }

        public double GetSampleRate()
        {
            return (double)(1000.0 / _samplePeriod_ms);
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

            if (!_serialPort.IsOpen)
            {
                Console.WriteLine("Opening serial port: " + Properties.Settings.Default.DeviceCOMPort);
                _serialPort.Open();
            }   

            if (_readerThread == null)
            {
                _readerThread = Task.Run(() =>
                {
                    _stopwatch.Start();  

                    while (!_stopping)
                    {
                        try
                        {                                                   
                            DoWork();                                                 
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
            _stopwatch.Stop();
          
            _stopping = false;
            _readerThread = null;

            SetSensorLEDState(0, LEDState.Off);
            SetSensorLEDState(1, LEDState.Off);  

            Console.WriteLine("Closing serial port: " + Properties.Settings.Default.DeviceCOMPort);
            _serialPort.Close();
            _state = FnirsControllerState.Stopped;
        }

        void Send(byte[] bytesToSend)
        {
            //Wait until serial port isn't being used (prevents conflicts with LED switching requests)
            while (_serialPortBusy) { }
            _serialPortBusy = true;
            _serialPort.DiscardInBuffer();
            _serialPort.Write(bytesToSend, 0, bytesToSend.Count());
            _serialPortBusy = false;
            Console.WriteLine("SENT:" + BitConverter.ToString(bytesToSend));
        }
        
        byte[] Receive(int count)
        {
            byte[] receiveData = new byte[count];

            for (int i = 0; i < count; i++)
            {
                receiveData[i] = (byte)_serialPort.ReadByte();
            }

            int timeSinceLastReceive = (int)_stopwatch.ElapsedMilliseconds - timeOfLastReceive;
            timeOfLastReceive = (int)_stopwatch.ElapsedMilliseconds;
            Console.WriteLine("REC :" + BitConverter.ToString(receiveData) + "\t\t" + timeSinceLastReceive.ToString());
            
            return receiveData;
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
            //Get sensor data
            ch0 = RequestSensorCH0Value();    
            ch1 = RequestSensorCH1Value();

            //Calculate irradiance values
            //sensorValue770 = ((953f * ch0) - (1258f * ch1)) / 2029184f;
            //sensorValue940 = ((6558f * ch1) - (3355f * ch0)) / 4058368f;

            sensorValue770 = ch0;
            sensorValue940 = ch1;

            Thread.Sleep(5);

            //Console.WriteLine("770: " + sensorValue770.ToString());
            //Console.WriteLine("940: " + sensorValue940.ToString());

            _results.Enqueue(new SensorResult { Read770 = sensorValue770, Read940 = sensorValue940, Milliseconds = _stopwatch.ElapsedMilliseconds });
        }

        public void SetSensorLEDState(ushort address, LEDState state)
        {
            return;
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
            Send(SetLEDCommandPacket);

            //Get acknowledgement
            byte[] ack = Receive(1);            

            if (ack[0] != SetLEDCommandPacket[0])
            {
                throw new LEDStateChangeUnacknowledgedException("LED State change was requested by computer, but not acknowledged by device.");
            }
        }

        int RequestSensorCH0Value()
        {    
            //Send the packet to device  
            Send(RequestCH0CommandPacket);

            //Read value out   
            byte[] data = Receive(3);

            if (data[0] != RequestCH0CommandPacket[0])
            {
                throw new Exception();
            }
            return (data[1] << 8) + (data[2]);
        }

        int RequestSensorCH1Value()
        {
            //Send the packet to device  
            Send(RequestCH1CommandPacket);

            //Read value out   
            byte[] data = Receive(3);

            if (data[0] != RequestCH1CommandPacket[0])
            {
                throw new Exception();
            }
            return (data[1] << 8) + (data[2]);
        }
        
        float RequestSensorIrradiance770()
        {            
            //Send the packet to device        
            Send(RequestIrradiance770ValueCommandPacket);

            //Read value out            
            byte[] data = Receive(5);            

            if (data[0] != RequestIrradiance770ValueCommandPacket[0])
            {
                throw new Exception();
            }
                        
            return BitConverter.ToSingle(data, 1);
        }

        float RequestSensorIrradiance940()
        {
            //Send the packet to device        
            Send(RequestIrradiance940ValueCommandPacket);

            //Read value out            
            byte[] data = Receive(5);

            if (data[0] != RequestIrradiance940ValueCommandPacket[0])
            {
                throw new Exception();
            }

            return BitConverter.ToSingle(data, 1);
        }
    }

#region Enums
    public enum FnirsControllerState
    {
        Running,
        Stopped
    }

    public enum LEDState
    {
        On,
        Off
    }
#endregion
}
