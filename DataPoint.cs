using System;

public class DataPoint
{
    public readonly DateTime X;
    public readonly double OriginalY;
    public double ExpSmoothedY { get; private set; }
    public double AvgSmoothedY { get; private set; }
    public double WeightedAvgY { get; private set; }

    public DataPoint(DateTime x, double y)
    {
        X = x;
        OriginalY = y;
        ExpSmoothedY = y;
        AvgSmoothedY = y;
        WeightedAvgY = y;
    }

    public DataPoint(DataPoint point)
    {
        X = point.X;
        OriginalY = point.OriginalY;
        ExpSmoothedY = point.ExpSmoothedY;
        AvgSmoothedY = point.AvgSmoothedY;
        WeightedAvgY = point.WeightedAvgY;
    }

    public DataPoint WithExpSmoothedY(double expSmoothedY)
    {
        return new DataPoint(this) { ExpSmoothedY = expSmoothedY };
    }

    public DataPoint WithAvgSmoothedY(double avgSmoothedY)
    {
        return new DataPoint(this) { AvgSmoothedY = avgSmoothedY };
    }

    public DataPoint WithWeightedAvgY(double weightedAvgY)
    {
        return new DataPoint(this) { WeightedAvgY = weightedAvgY };
    }
}
