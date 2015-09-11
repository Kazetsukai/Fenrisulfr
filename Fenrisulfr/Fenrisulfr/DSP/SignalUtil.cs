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
        public static double Power770(double CH0, double CH1)
        {
            return (double)((0.252484f * CH0) - (0.314568f * CH1) + 3.59901);
        }

        public static double Power940(double CH0, double CH1)
        {
            return (double)((0.183405f * CH1) - (0.101267f * CH0) + 39.7508);
        }

        /*
        public static double Hb(double CH0, double CH1)
        {
            return (double)(((15175 * Power770(CH0, CH1)) - (8125 * Power940(CH0, CH1))) / 14273579);  
        }

        public static double HbO2(double CH0, double CH1)
        {
            return (double)(((32797 * Power940(CH0, CH1)) - (17336 * Power770(CH0, CH1))) / 28547158);
        }
        */
        //9/12/2015 - JH - These new equations seem correct. Give both positive values for 770 and 940.
        public static IEnumerable<double> Power770(this IEnumerable<SensorResult> data)
        {            
            //uA770 = 0.252484(CH0) - 0.314568(CH1) + 3.59901
            foreach (var v in data)
            {
                yield return (double)((0.252484f * v.CH0) - (0.314568f * v.CH1) + 3.59901);
            }
        }

        public static IEnumerable<double> Power940(this IEnumerable<SensorResult> data)
        {
            //uA940 = 0.183405(CH1) - 0.101267(CH0) + 39.7508
            foreach (var v in data)
            {
                yield return (double)((0.183405f * v.CH1) - (0.101267f * v.CH0) + 39.7508);
            }
        }
        //JH 9/12/2015:   I THINK THESE CALCULATIONS FOR HB AND HBO2 ARE WRONG AND UNNEEDED ANYWAY. THE P770 AND P940 CORRELATE DIRECTLY TO HB AND HBO2 RESPECTIVELY, SO CAN JUST TAKE THEM AS RELATIVE TO EACH OTHER.
        /* 
        public static IEnumerable<double> Hb(this IEnumerable<SensorResult> data)
        {
            foreach (var v in data)
            {
                //LED datasheet says that 940 nm led current is linearly proportional to output light power. Assuming this is the case for the 770nm as well (datasheet does not have enough info for 770)  
                yield return (double)(((15175 * Power770(v.CH0, v.CH1)) - (8125 * Power940(v.CH0, v.CH1))) / 14273579);      
            }
        }

        public static IEnumerable<double> HbO2(this IEnumerable<SensorResult> data)
        {
            foreach (var v in data)
            {
                yield return (double)(((32797 * Power940(v.CH0, v.CH1)) - (17336 * Power770(v.CH0, v.CH1))) / 28547158);
            }
        }
        */
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
