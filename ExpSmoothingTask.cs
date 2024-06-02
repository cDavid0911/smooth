using OxyPlot;
using System.Collections.Generic;
using System.Linq;

namespace yield;

public static class ExpSmoothingTask
{
    public static IEnumerable<DataPoint> SmoothExponentially(this IEnumerable<DataPoint> data, double alpha, int smoothDegree)
    {
        if (smoothDegree == 0)
            return data;
        else
            return data.GetExponentialSmooth(alpha).SmoothExponentially(alpha,smoothDegree - 1);
    }

    public static IEnumerable<DataPoint> GetExponentialSmooth(this IEnumerable<DataPoint> data, double alpha)
    {
        double previousSmoothedY = 0;
        bool isFirst = true;
        foreach (DataPoint dataPoint in data)
        {
            if (isFirst)
            {
                yield return new DataPoint(dataPoint).WithExpSmoothedY(dataPoint.ExpSmoothedY);
                previousSmoothedY = dataPoint.ExpSmoothedY;
                isFirst = false;
                continue;
            }
            var smoothedY = alpha * dataPoint.ExpSmoothedY + (1 - alpha) * previousSmoothedY;
            previousSmoothedY = smoothedY;
            yield return new DataPoint(dataPoint).WithExpSmoothedY(smoothedY);
        };
    }
}
