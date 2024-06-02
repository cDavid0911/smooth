using System;
using System.Collections.Generic;
using System.Linq;

namespace yield;

public static class MovingAverageTask
{
    public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
    {
        var potentialMaxs = new PotentialMaxs(windowWidth);
        foreach (var point in data)
        {
            potentialMaxs.Add(point.OriginalY);
            yield return point.WithAvgSmoothedY(potentialMaxs.Max);
        }
    }

    private class Point
    {
        public double Value;
        public uint Number;
    }

    private class PotentialMaxs
    {
        private LinkedList<Point> Data;
        readonly int windowWidth;
        private uint iterator;
        public double Max { get { return Data.First.Value.Value; } }

        public PotentialMaxs(int windowWidth)
        {
            Data = new LinkedList<Point>();
            this.windowWidth = windowWidth;
            iterator = 0;
        }

        public void Add(double item)
        {
            while (Data.Count > 0)
            {
                if (Data.Last.Value.Value <= item)
                    Data.RemoveLast();
                else
                    break;
            }
            Data.AddLast(new Point { Value = item, Number = iterator });

            if (Data.First.Value.Number + windowWidth == iterator)
                Data.RemoveFirst();

            iterator++;
        }
    }
}

//using System.Collections.Generic;

//namespace yield;

//public static class MovingAverageTask
//{
//	public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
//	{
//        var window = new WeightedWindow(windowWidth);

//        foreach (var point in data)
//		{
//            window.Add(point.OriginalY);
//            yield return new DataPoint(point).WithAvgSmoothedY(window.WeightedAverage);
//		}
//	}
//}