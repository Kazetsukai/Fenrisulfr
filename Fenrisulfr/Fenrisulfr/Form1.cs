using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using WindowsFormsApplication1.Exceptions;

namespace Fenrisulfr
{
    enum LEDState
    {
        On,
        Off
    }   
    
    public partial class FNIRS : Form
    {
        int timeElapsed;

        public FNIRS()
        {
            InitializeComponent();

            chart.Series[0].Color = Color.Red;
            chart.Series[1].Color = Color.Green;

            serialPort1.Open();
        }
          
        void SetLEDState(ushort address, LEDState state)
        {
            //Clear serial port in buffer
            serialPort1.DiscardInBuffer();

            //Prepare command packet with empty address and state bytes
            byte[] data = { 0x4C, 0x00, 0x00 };

            //Inject state data to packet
            if (state == LEDState.On)
            {
                data[1] = 0x80;
            }
                        
            //Inject address of specified LED
            data[1] |= (byte)(address >> 7);
            data[2] |= (byte)address;

            //Send the packet to device
            serialPort1.Write(data, 0, data.Length);

            //Get acknowledgement
            byte ack = (byte)serialPort1.ReadByte();

            if (ack != 0x4C)
            {
                throw new LEDStateChangeUnacknowledgedException("LED State change was requested by computer, but not acknowledged by device.");
            }
        }

        void SetLED(ushort address)
        {
            SetLEDState(address, LEDState.On);
        }

        void ClearLED(ushort address)
        {
            SetLEDState(address, LEDState.Off);
        }

        int RequestSensorValue(ushort address)
        {
            //Clear serial port in buffer
            serialPort1.DiscardInBuffer();

            //Prepare command packet with address 
            byte[] data = { 0x53, (byte)(address >> 7), (byte)address };
                                  
            //Send the packet to device
            serialPort1.DiscardInBuffer();
            serialPort1.Write(data, 0, data.Length);
                       
            //Wait for data to be returned            
            byte[] returnData = new byte[3];
           
            serialPort1.Read(returnData, 0, returnData.Length);
            Console.WriteLine(BitConverter.ToString(returnData));          
            
            return ((int)returnData[1] << 8) + (int)(returnData[2]);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
        }      
       
        private void b_Start_Click(object sender, EventArgs e)
        {
            sampleTimer.Enabled = true;
        }

        private void b_Stop_Click(object sender, EventArgs e)
        {
            sampleTimer.Enabled = false;
        }

        private void sampleTimer_Tick(object sender, EventArgs e)
        {
            timeElapsed += sampleTimer.Interval;

            //Update sample rate from GUI
            float sampleRate = 1000f;
            try
            {
                float.TryParse(t_sampleRate.Text, out sampleRate);
            }
            catch { }

            float samplePeriod = (int)(1000f / sampleRate);
   
            if (samplePeriod <= 0)
            {
                samplePeriod = 1000;
            }

            sampleTimer.Interval = (int)samplePeriod;

            //Flash leds and get data
            SetLED(1);
            int sensorValue770 = RequestSensorValue(1);
            ClearLED(1);  

            SetLED(2);
            int sensorValue850 = RequestSensorValue(1);
            ClearLED(2);                    

            chart.Series[0].Points.Add(new DataPoint((double)(timeElapsed) / 1000, (double)sensorValue770));
            chart.Series[1].Points.Add(new DataPoint((double)(timeElapsed) / 1000, (double)sensorValue850));

            l_sensorValue_770.Text = "Sensor Value 770nm: " + sensorValue770.ToString();
            l_sensorValue_850.Text = "Sensor Value 850nm: " + sensorValue850.ToString();
        }
    }
}
