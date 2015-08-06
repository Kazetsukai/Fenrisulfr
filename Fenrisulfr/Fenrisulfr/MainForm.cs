using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Fenrisulfr.DSP;
using Fenrisulfr.FnirsControllerLogic;
using Fenrisulfr.Properties;

namespace Fenrisulfr
{ 
    
    public partial class FNIRS : Form
    {
        //private IFnirsController _controller = new FnirsController();
        private IFnirsController _controller = new FnirsController();
        
        
        private List<SensorResult> _trace = new List<SensorResult>();
        private DateTime _traceStart;
        
        int _chartWidth = 100000;
        bool _drawFFTHb = true;
        bool _drawFFTHbO2 = true;
        bool _fitPolyReg_Hb;
        bool _fitPolyReg_HbO2;
        int _polyRegOrder = 16;
        private int _samples = 10000;
        private int _runningAverageSamples = 1;

        public FNIRS()
        {
            InitializeComponent();

            //Load user settings
            //this.Size = Settings.Default.WindowSize;
            t_sampleRate.Text = Settings.Default.SampleRate.ToString();
            t_windowSize.Text = Settings.Default.FFTWindowSize.ToString();
            chartData.ChartAreas["ChartArea_Hb"].AxisY.ScaleView.Zoom(Settings.Default.ChartScaleViewMinYHb, 66000);//Properties.Settings.Default.ChartScaleViewMaxYHb);
            chartData.ChartAreas["ChartArea_HbO2"].AxisY.ScaleView.Zoom(Settings.Default.ChartScaleViewMinYHbO2, 66000);//Properties.Settings.Default.ChartScaleViewMaxYHbO2);
            
            //Initialize charts
            int borderWidth = 1;

            chartData.Series["S1_Hb"].Color = Color.DarkGreen;
            chartData.Series["S1_Hb"].BorderWidth = borderWidth;
            chartData.Series["S1_Hb"].BorderDashStyle = ChartDashStyle.Dot;

            chartData.Series["S1_HbO2"].Color = Color.DarkRed;
            chartData.Series["S1_HbO2"].BorderWidth = borderWidth;
            chartData.Series["S1_HbO2"].BorderDashStyle = ChartDashStyle.Dot;

            chartData.Series["S1_Hb_RunAvg"].Color = Color.Green;
            chartData.Series["S1_Hb_RunAvg"].BorderWidth = borderWidth;

            chartData.Series["S1_HbO2_RunAvg"].Color = Color.Red;            
            chartData.Series["S1_HbO2_RunAvg"].BorderWidth = borderWidth;

            chartData.ChartAreas["ChartArea_Hb"].AxisX.ScaleView.Zoomable = true;
            chartData.ChartAreas["ChartArea_HbO2"].AxisX.ScaleView.Zoomable = true;

            chartFFT.Series["S1_Hb_FFT"].Color = Color.DarkGreen;
            chartFFT.Series["S1_HbO2_FFT"].Color = Color.DarkRed;
            chartFFT.Series["S1_Hb_FFT"].BorderWidth = borderWidth;
            chartFFT.Series["S1_HbO2_FFT"].BorderWidth = borderWidth;
            chartFFT.ChartAreas["ChartArea"].AxisX.ScaleView.Zoomable = true;

            //Populate comport select combo box
            string[] availablePorts = SerialPort.GetPortNames();

            foreach (string port in availablePorts)
            {
                Console.WriteLine(port);
                c_comportSelect.Items.Add(port);
            }

            //Try set starting comport from last used comport
            if (c_comportSelect.Items.Contains(Settings.Default.DeviceCOMPort))
            {
                c_comportSelect.SelectedItem = Settings.Default.DeviceCOMPort;
            }
            else
            {
                if (c_comportSelect.Items.Count > 0)
                    c_comportSelect.SelectedIndex = 0;
            }         
        }    


        private void b_StartStop_Click(object sender, EventArgs e)
        {            
            if (_controller.GetState() == FnirsControllerState.Stopped)
            {
                //Check sample rate is valid
                double sampleRate = Settings.Default.SampleRate;
               
                if (sampleRate <= 0)
                {
                    MessageBox.Show("Invalid sample rate. Must be a number greater than zero Hz.");
                    return;
                }   
    
                //Check FFT window size is valid
                int fftWindowSize = Settings.Default.FFTWindowSize;
                
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

                    if (order >= Settings.Default.FFTWindowSize / 2)
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
            }
        }


        
        private void UI_UpdateTimer_Tick(object sender, EventArgs e)
        {
            var numResults = _controller.ResultsInQueue;

            Text = numResults.ToString();

            while (_controller.ResultsInQueue > 0)
            {
                var result = _controller.GetNextResult();
                _trace.Add(result);
            }

            UpdateChartData();

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

        private void UpdateChartData()
        {
            var start = _trace.Count - _samples;
            var end = start + _samples;
            if (start < 0) start = 0;
            var data = _trace.GetRange(start, end - start).ToArray();

            if (data.Length == 0) return;

            var result = _trace[_trace.Count - 1];

            var Hb = SignalUtil.Hb(data).ToArray();
            var HbO2 = SignalUtil.HbO2(data).ToArray();
                        
            var avgHb = SignalUtil.RunningAverage(Hb, _runningAverageSamples).ToArray();
            var avgHbO2 = SignalUtil.RunningAverage(HbO2, _runningAverageSamples).ToArray();

            //Clear the chart
            chartData.Series["S1_Hb"].Points.Clear();
            chartData.Series["S1_HbO2"].Points.Clear();
            chartData.Series["S1_Hb_RunAvg"].Points.Clear();
            chartData.Series["S1_HbO2_RunAvg"].Points.Clear();

            //Plot the latest data on the chart
            for (int i = 0; i < data.Length; i++)
            {
                chartData.Series["S1_Hb"].Points.AddXY(data[i].Milliseconds, Hb[i]);
                chartData.Series["S1_HbO2"].Points.AddXY(data[i].Milliseconds, HbO2[i]);
                chartData.Series["S1_Hb_RunAvg"].Points.AddXY(data[i].Milliseconds, avgHb[i]);
                chartData.Series["S1_HbO2_RunAvg"].Points.AddXY(data[i].Milliseconds, avgHbO2[i]);
            }
            
            //Rescale chart view (scrolls along with incoming data)
            chartData.ChartAreas["ChartArea_Hb"].AxisX.Minimum = result.Milliseconds - _chartWidth;
            chartData.ChartAreas["ChartArea_Hb"].AxisX.Maximum = result.Milliseconds;
            chartData.ChartAreas["ChartArea_HbO2"].AxisX.Minimum = result.Milliseconds - _chartWidth;
            chartData.ChartAreas["ChartArea_HbO2"].AxisX.Maximum = result.Milliseconds;
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
                    writer.WriteLine("{0},{1},{2}", "Milliseconds", "Hb", "HbO2");

                    foreach (var result in _trace)
                        writer.WriteLine("{0},{1},{2}", result.Milliseconds, result.CH0, result.CH1);
                }
            }
        }

        private void c_comportSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Save comport selection to appdata settings config file
            Settings.Default.DeviceCOMPort = c_comportSelect.SelectedItem.ToString();
            Settings.Default.Save();
        }

        private void FNIRS_ResizeEnd(object sender, EventArgs e)
        {
            Settings.Default.WindowSize = this.Size;
            Settings.Default.Save();          
        }

        private void chart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            Settings.Default.ChartScaleViewMaxYHb = chartData.ChartAreas["ChartArea_Hb"].AxisY.ScaleView.ViewMaximum;
            Settings.Default.ChartScaleViewMaxYHbO2 = chartData.ChartAreas["ChartArea_HbO2"].AxisY.ScaleView.ViewMaximum;
            Settings.Default.ChartScaleViewMinYHb = chartData.ChartAreas["ChartArea_Hb"].AxisY.ScaleView.ViewMinimum;
            Settings.Default.ChartScaleViewMinYHbO2 = chartData.ChartAreas["ChartArea_HbO2"].AxisY.ScaleView.ViewMinimum;

            Settings.Default.Save(); 
        }

        private void t_sampleRate_TextChanged(object sender, EventArgs e)
        {
            double sampleRate;

            try
            {
                double.TryParse(t_sampleRate.Text, out sampleRate);
                Settings.Default.SampleRate = sampleRate;
                Settings.Default.Save();
              
            }
            catch
            {
                t_sampleRate.Text = Settings.Default.SampleRate.ToString();
            }
        }

        private void t_windowSize_TextChanged(object sender, EventArgs e)
        {
            int windowSize;

            try
            {
                int.TryParse(t_windowSize.Text, out windowSize);
                _runningAverageSamples = windowSize;

            }
            catch
            {
                t_windowSize.Text = "1";
                _runningAverageSamples = 1;
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

        private void ch_drawFFTHb_CheckedChanged(object sender, EventArgs e)
        {
            _drawFFTHb = ch_drawFFTHb.Checked;
        }

        private void ch_drawFFTHbO2_CheckedChanged(object sender, EventArgs e)
        {
            _drawFFTHbO2 = ch_drawFFTHbO2.Checked;
        }  

        private void ch_bestFitLineHb_CheckedChanged(object sender, EventArgs e)
        {
            _fitPolyReg_Hb = ch_FitPolyRegHb.Checked;
        }

        private void ch_FitPolyRegHbO2_CheckedChanged(object sender, EventArgs e)
        {
            _fitPolyReg_HbO2 = ch_FitPolyRegHbO2.Checked;
        }

        private void t_polyRegOrder_TextChanged(object sender, EventArgs e)
        {
            //Check polynomial order is valid
            try
            {
                int order;
                int.TryParse(t_polyRegOrder.Text, out order);

                if (order >= Settings.Default.FFTWindowSize / 2)
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

        private void FNIRS_FormClosing(object sender, FormClosingEventArgs e)
        {
            _controller.Stop(); 
        }        
    }
}
