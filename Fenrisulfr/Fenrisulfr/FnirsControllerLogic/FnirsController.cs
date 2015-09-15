using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fenrisulfr.Exceptions;
using Fenrisulfr;
using Fenrisulfr.Properties;

namespace Fenrisulfr.FnirsControllerLogic
{
    public class FnirsController : IFnirsController
    {
        private readonly static byte[] SetLEDCommandPacket = { 0x4C, 0x00, 0x00 };
        private readonly static byte[] SetADCConfigPacket = { 0x41, 0x00 };
        private readonly static byte[] RequestCH0CommandPacket = { 0x54 };
        private readonly static byte[] RequestCH1CommandPacket = { 0x55 };
        private readonly static byte[] RequestIrradianceHbValueCommandPacket = { 0x64 };
        private readonly static byte[] RequestIrradianceHbO2ValueCommandPacket = { 0x65 };
        private readonly static byte[] RequestIrradianceValuesCommandPacket =   { 0x66 };        
        private readonly static byte[] ReturnData = new byte[5];
                
        private SerialPort _serialPort = new SerialPort(Settings.Default.DeviceCOMPort, 2000000, Parity.Even, 8, StopBits.One);
        private ConcurrentQueue<SensorResult> _results = new ConcurrentQueue<SensorResult>();
        private Task _readerThread;
        private Stopwatch _stopwatch = new Stopwatch();
        private bool _stopping = false;
        private FnirsControllerState _state = FnirsControllerState.Stopped;

        private int timeOfLastReceive;
        private bool _serialPortBusy = false;

        byte[] floatHb;
        byte[] floatHbO2;

        ADCGain currentGain = ADCGain.x9876;
        ADCIntegrationTime currentIntegTime = ADCIntegrationTime.IntegTime600ms;
               
        public void Reset()
        {
            _stopwatch.Reset();
            _results = new ConcurrentQueue<SensorResult>();
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
                Console.WriteLine("Opening serial port: " + Settings.Default.DeviceCOMPort);
                _serialPort.Open();
            }   

            if (_readerThread == null)
            {
                _readerThread = Task.Run(() =>
                {
                    _stopwatch.Start();

                    //Try 3 times to make sure leds turn on
                    SetSensorLEDState(0, LEDState.On);
                    SetSensorLEDState(0, LEDState.On);
                    SetSensorLEDState(0, LEDState.On);
                    SetSensorLEDState(1, LEDState.On);
                    SetSensorLEDState(1, LEDState.On);
                    SetSensorLEDState(1, LEDState.On);

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
            if (_readerThread != null) _readerThread.Wait();
            _stopwatch.Stop();
          
            _stopping = false;
            _readerThread = null;

            //SetSensorLEDState(0, LEDState.Off);
            //SetSensorLEDState(1, LEDState.Off);  

            Console.WriteLine("Closing serial port: " + Settings.Default.DeviceCOMPort);
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
            Thread.Sleep(((int)currentIntegTime + 1) * 50); 
            SensorResult result = new SensorResult { CH0 = RequestSensorCH0Value(), CH1 = RequestSensorCH1Value(), Milliseconds = (int)_stopwatch.ElapsedMilliseconds };
            
            /*
            Console.WriteLine("CH0: " + result.CH0.ToString());
            Console.WriteLine("CH1: " + result.CH1.ToString());
            Console.WriteLine();*/

            _results.Enqueue(result);
        }

        public void SetSensorLEDState(ushort address, LEDState state)
        {
            if (GetState() == FnirsControllerState.Stopped)
            {
                _serialPort.Open();
            } 

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

            if (GetState() == FnirsControllerState.Stopped)
            {
                _serialPort.Close();
            }           
        }

        public void SetSensorADCConfig(ADCGain newGain, ADCIntegrationTime newIntegTime)
        {
            if (GetState() == FnirsControllerState.Stopped)
            {
                _serialPort.Open();
            } 

            //Determine register value from desired gain and ATime value.
            byte registerValue = (byte)(((byte)newGain << 4) | (byte)newIntegTime);

            SetADCConfigPacket[1] = registerValue;

            Send(SetADCConfigPacket);

            //Get acknowledgement
            byte[] ack = Receive(1);

            if (ack[0] != SetADCConfigPacket[0])
            {
                throw new LEDStateChangeUnacknowledgedException("ADC config change was requested by computer, but not acknowledged by device.");
            }

            if (GetState() == FnirsControllerState.Stopped)
            {
                _serialPort.Close();
            }           

            //Record new settings locally
            currentGain = newGain;
            currentIntegTime = newIntegTime;
        }
        
        private float[] RequestSensorIrradianceValues()
        {
            //Send the packet to device        
            Send(RequestIrradianceValuesCommandPacket);

            //Read value out            
            byte[] data = Receive(9);

            if (data[0] != RequestIrradianceValuesCommandPacket[0])
            {
                throw new Exception();
            }

            floatHb = data.Skip(1).Take(4).ToArray();
            floatHbO2 = data.Skip(5).Take(4).ToArray();

            float[] output = {BitConverter.ToSingle(floatHb, 0), BitConverter.ToSingle(floatHbO2, 0) };
            return output;
        }

        private int RequestSensorCH0Value()
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

        private int RequestSensorCH1Value()
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
        
        private float RequestSensorIrradianceHb()
        {            
            //Send the packet to device        
            Send(RequestIrradianceHbValueCommandPacket);

            //Read value out            
            byte[] data = Receive(5);            

            if (data[0] != RequestIrradianceHbValueCommandPacket[0])
            {
                throw new Exception();
            }
                        
            return BitConverter.ToSingle(data, 1);
        }

        private float RequestSensorIrradianceHbO2()
        {
            //Send the packet to device        
            Send(RequestIrradianceHbO2ValueCommandPacket);

            //Read value out            
            byte[] data = Receive(5);

            if (data[0] != RequestIrradianceHbO2ValueCommandPacket[0])
            {
                throw new Exception();
            }

            return BitConverter.ToSingle(data, 1);
        }
    }
}
