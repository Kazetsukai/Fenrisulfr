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

namespace Fenrisulfr
{ 
    
    public partial class FNIRS : Form
    {
        private FnirsController _controller = new FnirsController();
        int sampleCount;

        public FNIRS()
        {
            InitializeComponent();

            chart.Series[0].Color = Color.Red;
            chart.Series[1].Color = Color.Green;
        }               
       
        private void b_Start_Click(object sender, EventArgs e)
        {
            _controller.Start();
            sampleTimer.Start();
        }

        private void b_Stop_Click(object sender, EventArgs e)
        {
            sampleTimer.Stop();
            throw new ComputerSaysNoException("I didn't ever really expect anyone to want to stop...");
        }

        private void sampleTimer_Tick(object sender, EventArgs e)
        {
            var numResults = _controller.ResultsInQueue;

            Text = numResults.ToString();            
                
            while(_controller.ResultsInQueue > 0)
            { 
                sampleCount++;
                var result = _controller.GetNextResult();

                chart.Series[0].Points.Add(new DataPoint(sampleCount, result.Read770));
                chart.Series[1].Points.Add(new DataPoint(sampleCount, result.Read850));
            }              
        }

        private void chart_Click(object sender, EventArgs e)
        {

        }
    }
}
