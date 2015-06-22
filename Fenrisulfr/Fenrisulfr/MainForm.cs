using MathNet.Numerics.Transformations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Fenrisulfr
{ 
    
    public partial class FNIRS : Form
    {
        private FnirsController _controller = new FnirsController();
        private List<SensorResult> _trace = new List<SensorResult>();
        private DateTime _traceStart;
        
        int _chartWidth = 10000;

        public FNIRS()
        {
            InitializeComponent();

            //Load user settings
            this.Size = Properties.Settings.Default.WindowSize;
            t_sampleRate.Text = Properties.Settings.Default.SampleRate.ToString();
            t_windowSize.Text = Properties.Settings.Default.FFTWindowSize.ToString();
            chartData.ChartAreas["ChartArea_770"].AxisY.ScaleView.Zoom(Properties.Settings.Default.ChartScaleViewMinY770nm, Properties.Settings.Default.ChartScaleViewMaxY770nm);
            chartData.ChartAreas["ChartArea_850"].AxisY.ScaleView.Zoom(Properties.Settings.Default.ChartScaleViewMinY850nm, Properties.Settings.Default.ChartScaleViewMaxY850nm);
            
            //Initialize charts
            int borderWidth = 2;
            chartData.Series["S1_770_Raw"].Color = Color.DarkGreen;
            chartData.Series["S1_850_Raw"].Color = Color.DarkRed;
            chartData.Series["S1_770_Raw"].BorderWidth = borderWidth;
            chartData.Series["S1_850_Raw"].BorderWidth = borderWidth;
            chartData.ChartAreas["ChartArea_770"].AxisX.ScaleView.Zoomable = true;
            chartData.ChartAreas["ChartArea_850"].AxisX.ScaleView.Zoomable = true;

            chartFFT.Series["S1_770_FFT"].Color = Color.DarkGreen;
            chartFFT.Series["S1_850_FFT"].Color = Color.DarkRed;
            chartFFT.Series["S1_770_FFT"].BorderWidth = borderWidth;
            chartFFT.Series["S1_850_FFT"].BorderWidth = borderWidth;
            chartFFT.ChartAreas["ChartArea_770"].AxisX.ScaleView.Zoomable = true;
            chartFFT.ChartAreas["ChartArea_850"].AxisX.ScaleView.Zoomable = true;

            //Populate comport select combo box
            string[] availablePorts = SerialPort.GetPortNames();

            foreach (string port in availablePorts)
            {
                Console.WriteLine(port);
                c_comportSelect.Items.Add(port);
            }

            //Try set starting comport from last used comport
            if (c_comportSelect.Items.Contains(Properties.Settings.Default.DeviceCOMPort))
            {
                c_comportSelect.SelectedItem = Properties.Settings.Default.DeviceCOMPort;
            }
            else
            {
                c_comportSelect.SelectedIndex = 0;
            }         
        }    

        //might be useful for debugging
        private void PrintFFT(double[] data, int windowSize)
        {        
            //Calculate real and imag parts of frequency. Output of FFT for real data is mirror imaged, so there will be (2 * windowSize) elements in freqReal[] and freqImag[]
            double[] freqReal, freqImag;
            RealFourierTransformation rft = new RealFourierTransformation();
            rft.TransformForward(data, out freqReal, out freqImag);

            double[] dataOut = new double[windowSize];

            //We only take first half of data (windowSize = half of freqReal[] size)
            for (int i = 0; i < windowSize; i++)
            {                
                Console.WriteLine(freqReal[i] + "\t" + freqImag[i]);
            }
        }

        private List<DataPoint> GetSeriesFFT(List<DataPoint> discreteData, int windowSize, double sampleRate)
        {
            //Populate data
            double[] dataIn = new double[windowSize];
            for(int i = 0; i < discreteData.Count; i++)
            {
                dataIn[i] = discreteData[i].YValues[0];
            }

            //Calculate distance between frequencies on FFT plot
            double freqBinWidth = sampleRate / windowSize;

            //Calculate real and imag parts of frequency. Output of FFT for real data is mirror imaged, so there will be (2 * windowSize) elements in freqReal[] and freqImag[]
            double[] freqReal, freqImag;
            RealFourierTransformation rft = new RealFourierTransformation();
            rft.TransformForward(dataIn, out freqReal, out freqImag);

            List<DataPoint> dataOut = new List<DataPoint>(windowSize / 2);      

            //We only take first half of data (windowSize = half of freqReal[] size)
            for (int i = 0; i < windowSize / 2; i++)
            {
                double magnitude = Math.Sqrt((freqReal[i] * freqReal[i]) + (freqImag[i] * freqImag[i]));                
                dataOut.Add(new DataPoint(i * freqBinWidth / 2, magnitude));
                //Console.WriteLine(freqReal[i] + "\t" + freqImag[i]);
            }           
            return dataOut;
        }

        private void b_StartStop_Click(object sender, EventArgs e)
        {            
            if (_controller.GetState() == FnirsControllerState.Stopped)
            {
                //Check sample rate is valid
                double sampleRate = Properties.Settings.Default.SampleRate;
               
                if (sampleRate <= 0)
                {
                    MessageBox.Show("Invalid sample rate. Must be a number greater than zero Hz.");
                    return;
                }   
    
                //Check FFT window size is valid
                int fftWindowSize = Properties.Settings.Default.FFTWindowSize;
                
                if((fftWindowSize & (fftWindowSize -1)) != 0) //Check fft window size is power of 2
                {
                    MessageBox.Show("FFT window size must be a power of 2! (e.g, 32, 64, 128, etc)");
                    return;
                }

                _traceStart = DateTime.Now;
                b_StartStop.Text = "Stop";
                c_comportSelect.Enabled = false;
                t_sampleRate.Enabled = false;

                _controller.SetSampleRate(sampleRate);
                _controller.Start();
                UI_UpdateTimer.Start();
            }
            else if (_controller.GetState() == FnirsControllerState.Running)
            {
                b_StartStop.Text = "Start";
                c_comportSelect.Enabled = true;
                t_sampleRate.Enabled = true;
                _controller.Stop();
                UI_UpdateTimer.Stop();                
            }
        }
        
        private void UI_UpdateTimer_Tick(object sender, EventArgs e)
        {
            var numResults = _controller.ResultsInQueue;

            Text = numResults.ToString();            
                
            while(_controller.ResultsInQueue > 0)
            { 
                var result = _controller.GetNextResult();
                _trace.Add(result);

                //Update data chart
                chartData.Series["S1_770_Raw"].Points.Add(new DataPoint(result.Milliseconds, result.Read770));
                chartData.Series["S1_850_Raw"].Points.Add(new DataPoint(result.Milliseconds, result.Read850));
                                
                chartData.ChartAreas["ChartArea_770"].AxisX.Minimum = result.Milliseconds - _chartWidth;
                chartData.ChartAreas["ChartArea_770"].AxisX.Maximum = result.Milliseconds;

                chartData.ChartAreas["ChartArea_850"].AxisX.Minimum = result.Milliseconds - _chartWidth;
                chartData.ChartAreas["ChartArea_850"].AxisX.Maximum = result.Milliseconds;
                                
                //Update FFT chart                
                if (chartData.Series["S1_770_Raw"].Points.Count > Properties.Settings.Default.FFTWindowSize)
                {
                    List<DataPoint> fftWindowData = new List<DataPoint>();
                
                    for (int i = 0; i < Properties.Settings.Default.FFTWindowSize; i++)
                    {
                        fftWindowData.Add(chartData.Series["S1_770_Raw"].Points[(chartData.Series["S1_770_Raw"].Points.Count - 1) - (Properties.Settings.Default.FFTWindowSize - i)]);
                    }

                    List<DataPoint> fftData770 = GetSeriesFFT(fftWindowData, Properties.Settings.Default.FFTWindowSize, _controller.GetSampleRate());
                    chartFFT.Series["S1_770_FFT"].Points.Clear();
                
                    foreach (DataPoint point in fftData770)
                    {
                        chartFFT.Series["S1_770_FFT"].Points.Add(point);
                    }
                }

                if (chartData.Series["S1_850_Raw"].Points.Count > Properties.Settings.Default.FFTWindowSize)
                {
                    List<DataPoint> fftWindowData = new List<DataPoint>();

                    for (int i = 0; i < Properties.Settings.Default.FFTWindowSize; i++)
                    {
                        fftWindowData.Add(chartData.Series["S1_850_Raw"].Points[(chartData.Series["S1_850_Raw"].Points.Count - 1) - (Properties.Settings.Default.FFTWindowSize - i)]);
                    }

                    List<DataPoint> fftData850 = GetSeriesFFT(fftWindowData, Properties.Settings.Default.FFTWindowSize, _controller.GetSampleRate());
                    chartFFT.Series["S1_850_FFT"].Points.Clear();

                    foreach (DataPoint point in fftData850)
                    {
                        chartFFT.Series["S1_850_FFT"].Points.Add(point);
                    }
                }
            }         
     
            // Only let people save if there are samples to save
            if (_trace.Count == 0)
            {
                btnSaveTrace.Enabled = false;
                lblSamples.Text = "(Waiting for samples)";
            }
            else
            {
                btnSaveTrace.Enabled = true;
                var milliseconds = (_trace[_trace.Count - 1].Milliseconds - _trace[0].Milliseconds);
                lblSamples.Text = _trace.Count + " samples collected - " + milliseconds + " milliseconds of data";
            }
        }

        private void chart_Click(object sender, EventArgs e)
        {

        }

        private void FNIRS_Load(object sender, EventArgs e)
        {

        }

        private void btnSaveTrace_Click(object sender, EventArgs e)
        {
            saveFileDialog.FileName = "FNIRS trace " + _traceStart.ToString("s").Replace(":", "") + ".csv";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var writer = new StreamWriter(saveFileDialog.OpenFile()))
                {
                    writer.WriteLine("{0},{1},{2}", "Milliseconds", "770nm", "850nm");

                    foreach (var result in _trace)
                        writer.WriteLine("{0},{1},{2}", result.Milliseconds, result.Read770, result.Read850);
                }
            }
        }

        private void c_comportSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Save comport selection to appdata settings config file
            Properties.Settings.Default.DeviceCOMPort = c_comportSelect.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }

        private void FNIRS_ResizeEnd(object sender, EventArgs e)
        {
            Properties.Settings.Default.WindowSize = this.Size;
            Properties.Settings.Default.Save();          
        }

        private void chart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            Properties.Settings.Default.ChartScaleViewMaxY770nm = chartData.ChartAreas["ChartArea_770"].AxisY.ScaleView.ViewMaximum;
            Properties.Settings.Default.ChartScaleViewMaxY850nm = chartData.ChartAreas["ChartArea_850"].AxisY.ScaleView.ViewMaximum;
            Properties.Settings.Default.ChartScaleViewMinY770nm = chartData.ChartAreas["ChartArea_770"].AxisY.ScaleView.ViewMinimum;
            Properties.Settings.Default.ChartScaleViewMinY850nm = chartData.ChartAreas["ChartArea_850"].AxisY.ScaleView.ViewMinimum;

            Properties.Settings.Default.Save(); 
        }

        private void t_sampleRate_TextChanged(object sender, EventArgs e)
        {
            double sampleRate;

            try
            {
                double.TryParse(t_sampleRate.Text, out sampleRate);
                Properties.Settings.Default.SampleRate = sampleRate;
                Properties.Settings.Default.Save();
              
            }
            catch
            {
                t_sampleRate.Text = Properties.Settings.Default.SampleRate.ToString();
            }
        }

        private void t_windowSize_TextChanged(object sender, EventArgs e)
        {
            int fftWindowSize;

            try
            {
                int.TryParse(t_windowSize.Text, out fftWindowSize);
                Properties.Settings.Default.FFTWindowSize = fftWindowSize;
                Properties.Settings.Default.Save();

            }
            catch
            {
                t_windowSize.Text = Properties.Settings.Default.FFTWindowSize.ToString();
            }
        }
    }
}
