using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private int sampleCount;
        private List<SensorResult> _trace = new List<SensorResult>();
        private DateTime _traceStart;

        public FNIRS()
        {
            InitializeComponent();

            chart.Series[0].Color = Color.Red;
            chart.Series[1].Color = Color.Green;

          
        }               
       
        private void b_Start_Click(object sender, EventArgs e)
        {
            _traceStart = DateTime.Now;
            _controller.Start();
        }

        private void b_Stop_Click(object sender, EventArgs e)
        {
            _controller.Stop();
        }

        private void sampleTimer_Tick(object sender, EventArgs e)
        {
            var numResults = _controller.ResultsInQueue;

            Text = numResults.ToString();            
                
            while(_controller.ResultsInQueue > 0)
            { 
                sampleCount++;
                var result = _controller.GetNextResult();
                _trace.Add(result);

                chart.Series[0].Points.Add(new DataPoint(result.Milliseconds, result.Read770));
                chart.Series[1].Points.Add(new DataPoint(result.Milliseconds, result.Read850));

                int chartWidth = 1000;
                chart.ChartAreas[0].AxisX.Minimum = sampleCount - chartWidth;
                chart.ChartAreas[0].AxisX.Maximum = sampleCount;

                chart.ChartAreas[1].AxisX.Minimum = sampleCount - chartWidth;
                chart.ChartAreas[1].AxisX.Maximum = sampleCount;
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
    }
}
