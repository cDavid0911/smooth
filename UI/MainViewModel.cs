using OxyPlot;
using OxyPlot.Annotations;
using OxyPlot.Axes;
using OxyPlot.Legends;
using OxyPlot.Series;

namespace yield.UI;

public class MainViewModel : IGraphModel
{
    private readonly LineSeries originalPoints;
    private readonly LineSeries expPoints;
    private readonly LineSeries avgPoints;
    private readonly LineSeries weightedAvgPoints;

    public PlotModel Model { get; }
    public IPlotController Controller { get; }

    public MainViewModel()
    {
        Controller = new PlotController();
        Model = new PlotModel { Title = "Порівняння методів згладжування" };

        Model.Legends.Add(new Legend
        {
            LegendPosition = LegendPosition.RightTop
        });

        Model.Axes.Add(new DateTimeAxis
        {
            Title = "Date",
            Position = AxisPosition.Bottom,
            MinimumPadding = 0,
            IntervalLength = 100
        });

        var line = new LineAnnotation
        {
            LineStyle = LineStyle.Solid,
            Color = OxyColors.Black,
            Type = LineAnnotationType.Horizontal,
            TextColor = OxyColors.Black,
        };

        Model.Annotations.Add(line);
        Model.Axes.Add(new LinearAxis
        {
            Title = "Y",
            Position = AxisPosition.Left,
            MinimumPadding = 0,
            Minimum = -5,
            Maximum = 600,
            IntervalLength = 25
        });

        originalPoints = AddCurve("original", OxyColors.Black);
        expPoints = AddCurve("exp", OxyColors.Red);
        avgPoints = AddCurve("max", OxyColors.Blue);
        //weightedAvgPoints = AddCurve("wd_avg", OxyColors.Green);

        Model.Series.Add(originalPoints);
        Model.Series.Add(expPoints);
        Model.Series.Add(avgPoints);
        //Model.Series.Add(weightedAvgPoints);
    }

    private LineSeries AddCurve(string label, OxyColor color)
    {
        return new LineSeries
        {
            Color = color,
            Title = label,
        };
    }

    public void AddPoint(DataPoint p)
    {
        originalPoints.Points.Add(DateTimeAxis.CreateDataPoint(p.X, p.OriginalY));
        expPoints.Points.Add(DateTimeAxis.CreateDataPoint(p.X, p.ExpSmoothedY));
        avgPoints.Points.Add(DateTimeAxis.CreateDataPoint(p.X, p.AvgSmoothedY));
        //weightedAvgPoints.Points.Add(DateTimeAxis.CreateDataPoint(p.X, p.WeightedAvgY));
    }
}
