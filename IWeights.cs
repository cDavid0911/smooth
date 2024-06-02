using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yield
{
    public interface IWeights
    {
        int Length { get; }
        double this[int n] { get; }
        double GetSumOfNLastElements(int n);
        double UpdateWeightedTotal(WeightedWindow window, double newElement);
    }
}
