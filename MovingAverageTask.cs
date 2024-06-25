using System.Collections.Generic;

namespace yield;

public static class MovingAverageTask
{
    public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
    {
        var queue = new Queue<double>();
        double sum = 0;
        foreach (DataPoint point in data)
        {
            queue.Enqueue(point.OriginalY);
            sum += point.OriginalY;
            if (queue.Count > windowWidth)
            {
                sum -= queue.Dequeue();
            }
            var y = sum / queue.Count;
            yield return point.WithAvgSmoothedY(y);
        }
    }
}