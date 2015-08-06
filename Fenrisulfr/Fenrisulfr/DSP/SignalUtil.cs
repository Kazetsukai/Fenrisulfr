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
                yield return (3.5817 * v.CH0) - (4.7278 * v.CH1);
            }
        }

        public static IEnumerable<double> Irradiance940(this IEnumerable<SensorResult> data)
        {
            foreach (var v in data)
            {
                yield return (12.3209 * v.CH1) - (6.3037 * v.CH0);
            }
        }

        public static IEnumerable<double> Hb(this IEnumerable<SensorResult> data)
        {
            foreach (var v in data)
            {
                var irrad770 = (3.5817 * v.CH0) - (4.7278 * v.CH1); 
                var irrad940 = (12.3209 * v.CH1) - (6.3037 * v.CH0);

                yield return irrad770 + (irrad940 * 0.52858493);        //Note: Hb and HbO2 are normalized to Hb under 770nm light (=1311.88)
            }
        }

        public static IEnumerable<double> HbO2(this IEnumerable<SensorResult> data)
        {
            foreach (var v in data)
            {
                var irrad770 = (3.5817 * v.CH0) - (4.7278 * v.CH1);
                var irrad940 = (12.3209 * v.CH1) - (6.3037 * v.CH0);

                yield return (irrad770 * 0.49547215) + (irrad940 * 0.925389517);
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
