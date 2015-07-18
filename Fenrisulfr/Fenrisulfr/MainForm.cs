using MathNet.Numerics;
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
        bool _drawFFT770 = true;
        bool _drawFFT940 = true;
        bool _fitPolyReg_770;
        bool _fitPolyReg_940;
        int _polyRegOrder = 16;

        public FNIRS()
        {
            InitializeComponent();

            //Load user settings
            this.Size = Properties.Settings.Default.WindowSize;
            t_sampleRate.Text = Properties.Settings.Default.SampleRate.ToString();
            t_windowSize.Text = Properties.Settings.Default.FFTWindowSize.ToString();
            chartData.ChartAreas["ChartArea_770"].AxisY.ScaleView.Zoom(Properties.Settings.Default.ChartScaleViewMinY770nm, 10000);//Properties.Settings.Default.ChartScaleViewMaxY770nm);
            chartData.ChartAreas["ChartArea_940"].AxisY.ScaleView.Zoom(Properties.Settings.Default.ChartScaleViewMinY940nm, 10000);//Properties.Settings.Default.ChartScaleViewMaxY940nm);
            
            //Initialize charts
            int borderWidth = 2;
            chartData.Series["S1_770_Raw"].Color = Color.DarkGreen;
            chartData.Series["S1_940_Raw"].Color = Color.DarkRed;
            chartData.Series["S1_770_Raw"].BorderWidth = borderWidth;
            chartData.Series["S1_940_Raw"].BorderWidth = borderWidth;
            chartData.ChartAreas["ChartArea_770"].AxisX.ScaleView.Zoomable = true;
            chartData.ChartAreas["ChartArea_940"].AxisX.ScaleView.Zoomable = true;

            chartFFT.Series["S1_770_FFT"].Color = Color.DarkGreen;
            chartFFT.Series["S1_940_FFT"].Color = Color.DarkRed;
            chartFFT.Series["S1_770_FFT"].BorderWidth = borderWidth;
            chartFFT.Series["S1_940_FFT"].BorderWidth = borderWidth;
            chartFFT.ChartAreas["ChartArea"].AxisX.ScaleView.Zoomable = true;

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

        private void GetSeriesFFT(List<DataPoint> discreteData, int windowSize, double sampleRate, out double[] xValues, out double[] yValues)
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

            xValues = new double[windowSize / 2];
            yValues = new double[windowSize / 2];

            //We only take first half of data (windowSize = half of freqReal[] size)
            for (int i = 0; i < windowSize / 2; i++)
            {
                double magnitude = Math.Sqrt((freqReal[i] * freqReal[i]) + (freqImag[i] * freqImag[i]));  
                double freq = i * freqBinWidth / 2;
                
                xValues[i] = freq;
                yValues[i] = magnitude;

                //Console.WriteLine(freqReal[i] + "\t" + freqImag[i]);
            }           
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

                //Check polynomial order is valid
                try
                {
                    int order;
                    int.TryParse(t_polyRegOrder.Text, out order);

                    if (order >= Properties.Settings.Default.FFTWindowSize / 2)
                    {
                        MessageBox.Show("Polynomial regression order must be a number less than half of the FFT window size.");                        
                        return;
                    }
                    _polyRegOrder = order;
                }
                catch
                {
                    MessageBox.Show("Polynomial regression order must be a number less than half of the FFT window size.");                  
                    return;
                }                    

                _traceStart = DateTime.Now;
                b_StartStop.Text = "Stop";
                c_comportSelect.Enabled = false;
                t_sampleRate.Enabled = false;
                t_windowSize.Enabled = false;

                _controller.SetSampleRate(sampleRate);
                _controller.Start();
                UI_UpdateTimer.Start();
            }
            else if (_controller.GetState() == FnirsControllerState.Running)
            {
                _controller.Stop();
                UI_UpdateTimer.Stop();   
                b_StartStop.Text = "Start";
                c_comportSelect.Enabled = true;
                t_sampleRate.Enabled = true;
                t_windowSize.Enabled = true;                             
            }
        }

        private void ButterworthFilter(List<DataPoint> fftSignal, double sampleFrequency, int order, double f0, double DCGain)
        {
            //Assumes input fftSignal has been halved to remove negative mirror image 
            if (f0 > 0)
            {
                var N = fftSignal.Count;
                var numBins = N;  
                var binWidth = sampleFrequency / N; // Hz

                // Filter
                Parallel.For(1, N, i =>
                {
                    var binFreq = binWidth * i;
                    var gain = DCGain / (Math.Sqrt((1 + Math.Pow(binFreq / f0, 2.0 * order))));                   
                    fftSignal[i].YValues[0] *= gain;
                    fftSignal[N - i].YValues[0] *= gain;
                });
            }
        }

        /// <summary>
        /// Generates line of best fit from data list using linear least squares algorithm.
        /// </summary>
        /// <param name="points"></param>
        /// <returns>List of DataPoint objects.</returns>
        public static List<DataPoint> GenerateLinearBestFit(List<DataPoint> points)
        {
            int numPoints = points.Count;
            double meanX = points.Average(point => point.XValue);
            double meanY = points.Average(point => point.YValues[0]);

            double sumXSquared = points.Sum(point => point.XValue * point.XValue);
            double sumXY = points.Sum(point => point.XValue * point.YValues[0]);

            double a = (sumXY / numPoints - meanX * meanY) / (sumXSquared / numPoints - meanX * meanX);
            double b = (a * meanX - meanY);

            return points.Select(point => new DataPoint(point.XValue, a * point.XValue - b)).ToList();
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
                chartData.Series["S1_940_Raw"].Points.Add(new DataPoint(result.Milliseconds, result.Read940));
                                
                chartData.ChartAreas["ChartArea_770"].AxisX.Minimum = result.Milliseconds - _chartWidth;
                chartData.ChartAreas["ChartArea_770"].AxisX.Maximum = result.Milliseconds;

                chartData.ChartAreas["ChartArea_940"].AxisX.Minimum = result.Milliseconds - _chartWidth;
                chartData.ChartAreas["ChartArea_940"].AxisX.Maximum = result.Milliseconds;
                
                //Update FFT chart                
                if (chartData.Series["S1_770_Raw"].Points.Count > Properties.Settings.Default.FFTWindowSize)
                {              
                    List<DataPoint> fftWindowData = new List<DataPoint>();
                
                    //Populate list of data to perform FFT on
                    for (int i = 0; i < Properties.Settings.Default.FFTWindowSize; i++)
                    {
                        fftWindowData.Add(chartData.Series["S1_770_Raw"].Points[(chartData.Series["S1_770_Raw"].Points.Count - 1) - (Properties.Settings.Default.FFTWindowSize - i)]);
                    }                 

                    //Apply FFT to the data
                    double[] fftData770X = new double[Properties.Settings.Default.FFTWindowSize / 2];
                    double[] fftData770Y = new double[Properties.Settings.Default.FFTWindowSize / 2];

                    GetSeriesFFT(fftWindowData, Properties.Settings.Default.FFTWindowSize, _controller.GetSampleRate(), out fftData770X, out fftData770Y);

                    //Clear the chart
                    chartFFT.Series["S1_770_FFT"].Points.Clear();
                    chartFFT.Series["S1_770_BestFitLine"].Points.Clear();

                    //Draw the data on the chart
                    if (_drawFFT770)
                    {
                        for (int i = 0; i < Properties.Settings.Default.FFTWindowSize / 2; i++)
                        {
                            chartFFT.Series["S1_770_FFT"].Points.Add(new DataPoint(fftData770X[i], fftData770Y[i]));
                        }
                    }

                    //Draw best fit line
                    if (_fitPolyReg_770)
                    {
                        //Calculate polynomial constants for regression line
                        double[] polynomialConstants = Fit.Polynomial(fftData770X, fftData770Y, _polyRegOrder);
                        
                        //Draw the line of best fit on chart
                        for (int i = 0; i < Properties.Settings.Default.FFTWindowSize / 2; i++)
                        {
                            double y = 0;

                            for (int j = 0; j < polynomialConstants.Length; j++)
                            {
                                y += polynomialConstants[j] * Math.Pow(fftData770X[i], j);
                            }                          

                            chartFFT.Series["S1_770_BestFitLine"].Points.Add(new DataPoint(fftData770X[i], y));
                        }
                    }
                }

                if (chartData.Series["S1_940_Raw"].Points.Count > Properties.Settings.Default.FFTWindowSize)
                {
                    List<DataPoint> fftWindowData = new List<DataPoint>();

                    for (int i = 0; i < Properties.Settings.Default.FFTWindowSize; i++)
                    {
                        fftWindowData.Add(chartData.Series["S1_940_Raw"].Points[(chartData.Series["S1_940_Raw"].Points.Count - 1) - (Properties.Settings.Default.FFTWindowSize - i)]);
                    }
                    
                    //Apply FFT to the data
                    double[] fftData940X = new double[Properties.Settings.Default.FFTWindowSize / 2];
                    double[] fftData940Y = new double[Properties.Settings.Default.FFTWindowSize / 2];

                    GetSeriesFFT(fftWindowData, Properties.Settings.Default.FFTWindowSize, _controller.GetSampleRate(), out fftData940X, out fftData940Y);
                    
                    //Clear the chart
                    chartFFT.Series["S1_940_FFT"].Points.Clear();
                    chartFFT.Series["S1_940_BestFitLine"].Points.Clear();

                    //Draw the data on the chart
                    if (_drawFFT940)
                    {
                        for (int i = 0; i < Properties.Settings.Default.FFTWindowSize / 2; i++)
                        {
                            chartFFT.Series["S1_940_FFT"].Points.Add(new DataPoint(fftData940X[i], fftData940Y[i]));
                        }
                    }
                    
                    //Draw best fit line
                    if (_fitPolyReg_940)
                    {
                        //Calculate polynomial constants for regression line
                        double[] polynomialConstants = Fit.Polynomial(fftData940X, fftData940Y, _polyRegOrder);
                        
                        //Draw the line of best fit on chart
                        for (int i = 0; i < Properties.Settings.Default.FFTWindowSize / 2; i++)
                        {
                            double y = 0;

                            for (int j = 0; j < polynomialConstants.Length; j++)
                            {
                                y += polynomialConstants[j] * Math.Pow(fftData940X[i], j);
                            }                          

                            chartFFT.Series["S1_940_BestFitLine"].Points.Add(new DataPoint(fftData940X[i], y));
                        }
                    }
                }
            }         
     
            // Only let people save if there are samples to save
            if (_trace.Count == 0)
            {
                b_SaveTrace.Enabled = false;
                l_samples.Text = "(Waiting for samples)";
            }
            else
            {
                b_SaveTrace.Enabled = true;
                var milliseconds = (_trace[_trace.Count - 1].Milliseconds - _trace[0].Milliseconds);
                l_samples.Text = _trace.Count + " samples collected - " + milliseconds + " milliseconds of data";
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
                    writer.WriteLine("{0},{1},{2}", "Milliseconds", "770nm", "940nm");

                    foreach (var result in _trace)
                        writer.WriteLine("{0},{1},{2}", result.Milliseconds, result.Read770, result.Read940);
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
            Properties.Settings.Default.ChartScaleViewMaxY940nm = chartData.ChartAreas["ChartArea_940"].AxisY.ScaleView.ViewMaximum;
            Properties.Settings.Default.ChartScaleViewMinY770nm = chartData.ChartAreas["ChartArea_770"].AxisY.ScaleView.ViewMinimum;
            Properties.Settings.Default.ChartScaleViewMinY940nm = chartData.ChartAreas["ChartArea_940"].AxisY.ScaleView.ViewMinimum;

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

     
        private void b_reset_Click(object sender, EventArgs e)
        {
            //Reset timers and counters
            _trace = new List<SensorResult>();
            _controller.Reset();
            b_SaveTrace.Enabled = false;

            //Clear the charts
            ClearChartData();
            ClearChartFFT();
        }

        void ClearChartData()
        {
            for (int i = 0; i < chartData.Series.Count; i++)
            {
                chartData.Series[i].Points.Clear();
                chartData.Series[i].Points.Add(new DataPoint(0, 0));
            }
        }

        void ClearChartFFT()
        {
            for (int i = 0; i < chartFFT.Series.Count; i++)
            {
                chartFFT.Series[i].Points.Clear();
                chartFFT.Series[i].Points.Add(new DataPoint(0, 0));
            }
        }

        private void ch_drawFFT770_CheckedChanged(object sender, EventArgs e)
        {
            _drawFFT770 = ch_drawFFT770.Checked;
        }

        private void ch_drawFFT940_CheckedChanged(object sender, EventArgs e)
        {
            _drawFFT940 = ch_drawFFT940.Checked;
        }  

        private void ch_bestFitLine770_CheckedChanged(object sender, EventArgs e)
        {
            _fitPolyReg_770 = ch_FitPolyReg770.Checked;
        }

        private void ch_FitPolyReg940_CheckedChanged(object sender, EventArgs e)
        {
            _fitPolyReg_940 = ch_FitPolyReg940.Checked;
        }

        private void t_polyRegOrder_TextChanged(object sender, EventArgs e)
        {
            //Check polynomial order is valid
            try
            {
                int order;
                int.TryParse(t_polyRegOrder.Text, out order);

                if (order >= Properties.Settings.Default.FFTWindowSize / 2)
                {
                    MessageBox.Show("Polynomial regression order must be a number less than half of the FFT window size.");
                    t_polyRegOrder.Text = "16";
                    _polyRegOrder = 16;
                    return;
                }
                _polyRegOrder = order;
            }
            catch
            {
                MessageBox.Show("Polynomial regression order must be a number less than half of the FFT window size.");
                t_polyRegOrder.Text = "16";
                _polyRegOrder = 16;
                return;
            }       
        }                  
    }
}
