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


        int chartWidth = 10000;

        public FNIRS()
        {
            InitializeComponent();

            //Load user settings
            this.Size = Properties.Settings.Default.WindowSize;
            t_sampleRate.Text = Properties.Settings.Default.SampleRate.ToString();
            chart.ChartAreas["ChartArea_770"].AxisY.ScaleView.Zoom(Properties.Settings.Default.ChartScaleViewMinY770nm, Properties.Settings.Default.ChartScaleViewMaxY770nm);
            chart.ChartAreas["ChartArea_850"].AxisY.ScaleView.Zoom(Properties.Settings.Default.ChartScaleViewMinY850nm, Properties.Settings.Default.ChartScaleViewMaxY850nm);
      
            //Initialize chart
            chart.Series["S1_770_Raw"].Color = Color.DarkGreen;
            chart.Series["S1_770_MovAvg"].Color = Color.Green;
            chart.Series["S1_770_MovAvg"].BorderWidth = 2;
            chart.Series["S1_850_Raw"].Color = Color.DarkRed;
            chart.Series["S1_850_MovAvg"].Color = Color.Red;

            chart.ChartAreas["ChartArea_770"].AxisX.ScaleView.Zoomable = true;
            chart.ChartAreas["ChartArea_850"].AxisX.ScaleView.Zoomable = true;
            
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

        private void b_StartStop_Click(object sender, EventArgs e)
        {          
            if (_controller.GetState() == FnirsControllerState.Stopped)
            {
                //Check sample rate is valid
                double sampleRate;

                try
                {
                    double.TryParse(t_sampleRate.Text, out sampleRate);
                    if (sampleRate <= 0)
                    {
                        MessageBox.Show("Invalid sample rate. Must be a number greater than zero Hz.");
                        return;
                    }

                }
                catch
                {
                    MessageBox.Show("Invalid sample rate. Must be a number greater than zero Hz.");
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

                chart.Series["S1_770_Raw"].Points.Add(new DataPoint(result.Milliseconds, result.Read770));
                chart.Series["S1_850_Raw"].Points.Add(new DataPoint(result.Milliseconds, result.Read850));
                
                if (chart.Series["S1_850_Raw"].Points.Count > 100)
                {
                    chart.DataManipulator.FinancialFormula(FinancialFormula.MovingAverage, chart.Series["S1_770_Raw"], chart.Series["S1_770_MovAvg"]);
                }

                chart.ChartAreas["ChartArea_770"].AxisX.Minimum = result.Milliseconds - chartWidth;
                chart.ChartAreas["ChartArea_770"].AxisX.Maximum = result.Milliseconds;

                chart.ChartAreas["ChartArea_850"].AxisX.Minimum = result.Milliseconds - chartWidth;
                chart.ChartAreas["ChartArea_850"].AxisX.Maximum = result.Milliseconds;
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
            Properties.Settings.Default.ChartScaleViewMaxY770nm = chart.ChartAreas["ChartArea_770"].AxisY.ScaleView.ViewMaximum;
            Properties.Settings.Default.ChartScaleViewMaxY850nm = chart.ChartAreas["ChartArea_850"].AxisY.ScaleView.ViewMaximum;
            Properties.Settings.Default.ChartScaleViewMinY770nm = chart.ChartAreas["ChartArea_770"].AxisY.ScaleView.ViewMinimum;
            Properties.Settings.Default.ChartScaleViewMinY850nm = chart.ChartAreas["ChartArea_850"].AxisY.ScaleView.ViewMinimum;

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
    }
}
