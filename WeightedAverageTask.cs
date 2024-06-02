using System.Collections.Generic;

namespace yield;

public static class WeightedAverageTask 
{
    public static IEnumerable<DataPoint> WeightedAverage(this IEnumerable<DataPoint> data, int windowWidth)
    {
        var window = new WeightedWindow(windowWidth, new ArithmeticalProgressionWeights(windowWidth));

        foreach (var point in data)
        {
            window.Add(point.OriginalY);
            yield return new DataPoint(point).WithWeightedAvgY(window.WeightedAverage);
        }
    }
}