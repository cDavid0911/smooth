using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yield
{
    public class DefaultWeights : IWeights
    {
        public int Length { get; private set; }
        public double this[int n]
        {
            get { return 1; }
        }
        public DefaultWeights(int length)
        {
            Length = length;
        }

        public double GetSumOfNLastElements(int n)
        {
            if (n > Length || n < 0) throw new ArgumentException();
            return n;
        }

        public double UpdateWeightedTotal(WeightedWindow window, double newElement)
        {
            if (window.IsFilled) 
                return window.Total - window.Items.Peek() + newElement;
            else
                return window.Total + newElement;
        }
    }

    public class ArithmeticalProgressionWeights : IWeights
    {
        public int Length { get; private set; }
        public double this[int n]
        {
            get { return Length - n; }
        }
        public ArithmeticalProgressionWeights(int length)
        {
            Length = length;
        }
        public double GetSumOfNLastElements(int n)
        {
            if (n > Length || n < 0) throw new ArgumentException();
            return n * (2 * Length - n + 1) / 2;
        }

        public double UpdateWeightedTotal(WeightedWindow window, double newElement)
        {
            return window.WeightedTotal + this[0] * newElement - window.Total;
        }
    }
}
