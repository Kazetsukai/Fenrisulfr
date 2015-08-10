using Fenrisulfr.FnirsControllerLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fenrisulfr.DSP
{
    public static class SignalUtil
    {
        public static IEnumerable<double> Irradiance770(this IEnumerable<SensorResult> data)
        {
            foreach (var v in data)
            {
                yield return (double)(((953 * v.CH0) - (1258 * v.CH1)) / 2029184);
            }
        }

        public static IEnumerable<double> Irradiance940(this IEnumerable<SensorResult> data)
        {
            foreach (var v in data)
            {
                yield return (double)(((6558 * v.CH1) - (3355 * v.CH0)) / 4058368);
            }
        }

        public static IEnumerable<double> Hb(this IEnumerable<SensorResult> data)
        {
            foreach (var v in data)
            {
                var irrad770 = (3.5817 * v.CH0) - (4.7278 * v.CH1); 
                var irrad940 = (12.3209 * v.CH1) - (6.3037 * v.CH0);

                yield return (double)(((15175 * irrad770) - (8125 * irrad940)) / 14273579);      
            }
        }

        public static IEnumerable<double> HbO2(this IEnumerable<SensorResult> data)
        {
            foreach (var v in data)
            {
                var irrad770 = (3.5817 * v.CH0) - (4.7278 * v.CH1);
                var irrad940 = (12.3209 * v.CH1) - (6.3037 * v.CH0);

                yield return (double)(((32797 * irrad940) - (17336 * irrad770)) / 28547158);
            }
        }

        public static IEnumerable<double> RunningAverage(this IEnumerable<double> data, int windowSize)
        {
            Queue<double> relevantPoints = new Queue<double>();

            foreach (double value in data)
            {
                relevantPoints.Enqueue(value);
                while (relevantPoints.Count > windowSize) relevantPoints.Dequeue();
                yield return relevantPoints.Sum() / relevantPoints.Count;
            }
        }       
    }
}
