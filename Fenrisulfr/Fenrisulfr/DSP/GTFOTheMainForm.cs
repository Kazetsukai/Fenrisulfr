using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics.Transformations;

namespace Fenrisulfr.DSP
{
    class GTFOTheMainForm
    {

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

            for (int i = 0; i < discreteData.Count; i++)
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

        // Explanation:
        // This code is a combination of display logic and "business" logic.
        // You should create a clear separation between the parts of the code that are
        // "doing stuff", such as calculating the output of an FFT or doing some other
        // signal processing, and then use the final output of that to populate display
        // controls. Keeps it all cleaner.
        public void CodeThatShouldntBeInTheMainForm()
        {
            //Update data chart
            //chartData.Series["S1_770_Raw"].Points.Add(new DataPoint(result.Milliseconds, (double)result.Read770));
            //chartData.Series["S1_940_Raw"].Points.Add(new DataPoint(result.Milliseconds, (double)result.Read940));

            //chartData.ChartAreas["ChartArea_770"].AxisX.Minimum = result.Milliseconds - _chartWidth;
            //chartData.ChartAreas["ChartArea_770"].AxisX.Maximum = result.Milliseconds;

            //chartData.ChartAreas["ChartArea_940"].AxisX.Minimum = result.Milliseconds - _chartWidth;
            //chartData.ChartAreas["ChartArea_940"].AxisX.Maximum = result.Milliseconds;
                
            //Update FFT chart                
            //if (chartData.Series["S1_770_Raw"].Points.Count > Properties.Settings.Default.FFTWindowSize)
            //{              
            //    List<DataPoint> fftWindowData = new List<DataPoint>();
                
            //    //Populate list of data to perform FFT on
            //    for (int i = 0; i < Properties.Settings.Default.FFTWindowSize; i++)
            //    {
            //        fftWindowData.Add(chartData.Series["S1_770_Raw"].Points[(chartData.Series["S1_770_Raw"].Points.Count - 1) - (Properties.Settings.Default.FFTWindowSize - i)]);
            //    }                 

            //    //Apply FFT to the data
            //    double[] fftData770X = new double[Properties.Settings.Default.FFTWindowSize / 2];
            //    double[] fftData770Y = new double[Properties.Settings.Default.FFTWindowSize / 2];

            //    GetSeriesFFT(fftWindowData, Properties.Settings.Default.FFTWindowSize, _controller.GetSampleRate(), out fftData770X, out fftData770Y);

            //    //Clear the chart
            //    chartFFT.Series["S1_770_FFT"].Points.Clear();
            //    chartFFT.Series["S1_770_BestFitLine"].Points.Clear();

            //    //Draw the data on the chart
            //    if (_drawFFT770)
            //    {
            //        for (int i = 0; i < Properties.Settings.Default.FFTWindowSize / 2; i++)
            //        {
            //            chartFFT.Series["S1_770_FFT"].Points.Add(new DataPoint(fftData770X[i], fftData770Y[i]));
            //        }
            //    }

            //    //Draw best fit line
            //    if (_fitPolyReg_770)
            //    {
            //        //Calculate polynomial constants for regression line
            //        double[] polynomialConstants = Fit.Polynomial(fftData770X, fftData770Y, _polyRegOrder);
                        
            //        //Draw the line of best fit on chart
            //        for (int i = 0; i < Properties.Settings.Default.FFTWindowSize / 2; i++)
            //        {
            //            double y = 0;

            //            for (int j = 0; j < polynomialConstants.Length; j++)
            //            {
            //                y += polynomialConstants[j] * Math.Pow(fftData770X[i], j);
            //            }                          

            //            chartFFT.Series["S1_770_BestFitLine"].Points.Add(new DataPoint(fftData770X[i], y));
            //        }
            //    }
            //}

            //if (chartData.Series["S1_940_Raw"].Points.Count > Properties.Settings.Default.FFTWindowSize)
            //{
            //    List<DataPoint> fftWindowData = new List<DataPoint>();

            //    for (int i = 0; i < Properties.Settings.Default.FFTWindowSize; i++)
            //    {
            //        fftWindowData.Add(chartData.Series["S1_940_Raw"].Points[(chartData.Series["S1_940_Raw"].Points.Count - 1) - (Properties.Settings.Default.FFTWindowSize - i)]);
            //    }
                    
            //    //Apply FFT to the data
            //    double[] fftData940X = new double[Properties.Settings.Default.FFTWindowSize / 2];
            //    double[] fftData940Y = new double[Properties.Settings.Default.FFTWindowSize / 2];

            //    GetSeriesFFT(fftWindowData, Properties.Settings.Default.FFTWindowSize, _controller.GetSampleRate(), out fftData940X, out fftData940Y);
                    
            //    //Clear the chart
            //    chartFFT.Series["S1_940_FFT"].Points.Clear();
            //    chartFFT.Series["S1_940_BestFitLine"].Points.Clear();

            //    //Draw the data on the chart
            //    if (_drawFFT940)
            //    {
            //        for (int i = 0; i < Properties.Settings.Default.FFTWindowSize / 2; i++)
            //        {
            //            chartFFT.Series["S1_940_FFT"].Points.Add(new DataPoint(fftData940X[i], fftData940Y[i]));
            //        }
            //    }
                    
            //    //Draw best fit line
            //    if (_fitPolyReg_940)
            //    {
            //        //Calculate polynomial constants for regression line
            //        double[] polynomialConstants = Fit.Polynomial(fftData940X, fftData940Y, _polyRegOrder);
                        
            //        //Draw the line of best fit on chart
            //        for (int i = 0; i < Properties.Settings.Default.FFTWindowSize / 2; i++)
            //        {
            //            double y = 0;

            //            for (int j = 0; j < polynomialConstants.Length; j++)
            //            {
            //                y += polynomialConstants[j] * Math.Pow(fftData940X[i], j);
            //            }                          

            //            chartFFT.Series["S1_940_BestFitLine"].Points.Add(new DataPoint(fftData940X[i], y));
            //        }
            //    }
            //}
        }
    }
}
