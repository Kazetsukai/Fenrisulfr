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
        //IRRADIANCE IS VERY POSSIBLY WRONG. EXPERIMENTS SUGGEST NEW EQUATIONS WORK BETTER. SEE Power770 and Power940 below.
        /*
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
        }*/

        public static double Power770(double CH0, double CH1)
        {
            return (double)((0.155615 * CH1) - (0.00715108 * CH0) + 105.5277);
        }

        public static double Power940(double CH0, double CH1)
        {
            return (double)((0.00286816799 * CH0) - (0.1196520514 * CH1) - 1.130977);
        }

        public static double Hb(double CH0, double CH1)
        {
            return (double)(((15175 * Power770(CH0, CH1)) - (8125 * Power940(CH0, CH1))) / 14273579);  
        }

        public static double HbO2(double CH0, double CH1)
        {
            return (double)(((32797 * Power940(CH0, CH1)) - (17336 * Power770(CH0, CH1))) / 28547158);
        }

        public static IEnumerable<double> Power770(this IEnumerable<SensorResult> data)
        {
            //uA770 = 0.155615(CH1) - 0.00715108(CH0) + 105.5277
            foreach (var v in data)
            {
                yield return (double)((0.155615 * v.CH1) - (0.00715108 * v.CH0) + 105.5277);
            }
        }

        public static IEnumerable<double> Power940(this IEnumerable<SensorResult> data)
        {
            //uA940 = 0.00286816799(CH0) - 0.1196520514(CH1) - 1.130977
            foreach (var v in data)
            {
                yield return (double)((0.00286816799 * v.CH0) - (0.1196520514 * v.CH1) - 1.130977);
            }
        }


        public static IEnumerable<double> Hb(this IEnumerable<SensorResult> data)
        {
            foreach (var v in data)
            {
                //LED datasheet says that 940 nm led current is linearly proportional to output light power. Assuming this is the case for the 770nm as well (datasheet does not have enough info for 770)
                var power770 = (double)((0.155615 * v.CH1) - (0.00715108 * v.CH0) + 105.5277);
                var power940 = (double)((0.00286816799 * v.CH0) - (0.1196520514 * v.CH1) - 1.130977);

                yield return (double)(((15175 * power770) - (8125 * power940)) / 14273579);      
            }
        }

        public static IEnumerable<double> HbO2(this IEnumerable<SensorResult> data)
        {
            foreach (var v in data)
            {
                var power770 = (double)((0.155615 * v.CH1) - (0.00715108 * v.CH0) + 105.5277);
                var power940 = (double)((0.00286816799 * v.CH0) - (0.1196520514 * v.CH1) - 1.130977);

                yield return (double)(((32797 * power940) - (17336 * power770)) / 28547158);
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
