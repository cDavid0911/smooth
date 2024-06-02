using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace yield
{
    public class WeightedWindow
    {
        public int MaxCount { get; private set; }
        public int Count { get; private set; }
        public bool IsFilled { get { return Count == MaxCount; } }
        public double WeightedTotal { get; private set; }
        public Queue<double> Items { get; private set; }
        public double Total { get; private set; }

        readonly IWeights Weights;

        public double WeightedAverage
        {
            get {return WeightedTotal / Weights.GetSumOfNLastElements(Count);}
        }

        public WeightedWindow(int maxCount, IWeights weights)
        {
            Weights = weights;
            this.MaxCount = maxCount;
            Count = 0;
            Items = new Queue<double>();
            Total = 0;
        }
        public WeightedWindow(int maxCount) : this(maxCount, new DefaultWeights(maxCount)) { }

        public void Add(double item)
        {
            WeightedTotal = Weights.UpdateWeightedTotal(this, item);
            Items.Enqueue(item);
            Total += item;

            if (IsFilled)
                Total -= Items.Dequeue();
            else Count++;
        }
    }
}
