using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace yield;

public static class ExpSmoothingTask
{
    public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
    {
        DataPoint prevPoint = null; 
        double y;
        foreach (DataPoint point in data)
        {
            if (prevPoint == null)           
                y = point.OriginalY;
            else            
                y = alpha * point.OriginalY + (1 - alpha) * prevPoint.ExpSmoothedY;            
            yield return point.WithExpSmoothedY(y);
            prevPoint = point.WithExpSmoothedY(y);
        }
    }
}
