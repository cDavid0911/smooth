using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace yield;

public class DataSource
{
	public static IEnumerable<DataPoint> GetData(Random random)
	{
        return GenerateOriginalData("SN_d_tot_V2.0.csv").SmoothExponentially(0.02, 5).MovingMax(1000);//.WeightedAverage(1000);
	}

    public static IEnumerable<DataPoint> GenerateOriginalData(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = ";",
            HasHeaderRecord = false // CSV file doesn't have a header
        }))
        {
            DateTime? lastDate = null;
            double? lastY = null;
            bool inGap = false;
            List<DataPoint> buffer = new List<DataPoint>();

            while (csv.Read())
            {

                int year = csv.GetField<int>(0);
                int month = csv.GetField<int>(1);
                int day = csv.GetField<int>(2);
                var date = new DateTime(year, month, day);
                var sunspotNumber = csv.GetField<int>(4);
                //var date = DateTime.ParseExact(csv.GetField<string>(0), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                //var sunspotNumber = csv.GetField<double>(3);

                if (sunspotNumber == -1)
                {
                    if (!inGap)
                    {
                        buffer.Clear();
                        inGap = true;
                    }
                    buffer.Add(new DataPoint(date, sunspotNumber));
                }
                else
                {
                    if (inGap)
                    {
                        if (lastDate.HasValue && lastY.HasValue)
                        {
                            DateTime nextDate = date;
                            double nextY = sunspotNumber;
                            int gapSize = buffer.Count + 1;

                            for (int i = 0; i < buffer.Count; i++)
                            {
                                var interpolatedDate = buffer[i].X;
                                double interpolatedY = lastY.Value + (nextY - lastY.Value) * (i + 1) / gapSize;
                                yield return new DataPoint(interpolatedDate, interpolatedY);
                            }
                        }
                        inGap = false;
                    }

                    lastDate = date;
                    lastY = sunspotNumber;
                    yield return new DataPoint(date, sunspotNumber);
                }
            }
        }
    }

}