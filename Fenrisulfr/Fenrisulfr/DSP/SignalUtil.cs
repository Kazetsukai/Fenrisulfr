using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fenrisulfr.DSP
{
    public static class SignalUtil
    {
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
