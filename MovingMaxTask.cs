using System;
using System.Collections.Generic;
using System.Linq;

namespace yield;

public static class MovingMaxTask
{
    public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
    {
        {
            var queue = new Queue<double>();
            var deque = new LinkedList<double>();
            foreach (DataPoint point in data)
            {
                queue.Enqueue(point.OriginalY);
                while (deque.Count > 0 &&  point.OriginalY > deque.Last.Value)
                    deque.RemoveLast();
                
                deque.AddLast(point.OriginalY);
                if (queue.Count > windowWidth && queue.Dequeue() == deque.First.Value)
                    deque.RemoveFirst();
                yield return point.WithMaxY(deque.First.Value);
            }
        }
    }
}




//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace yield;

//public static class MovingMaxTask
//{
//    public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
//    {
//        var deque = new LinkedList<double>();
//        var leftBorder = -windowWidth;
//        var indexOfAdded = 0;
//        var counter = 0;

//        foreach (var point in data)
//        {
//            if (deque.First == null)
//            {
//                deque.AddLast(point.OriginalY);
//            }
//            else
//            {
//                if (leftBorder == indexOfAdded)
//                {
//                    deque.RemoveFirst();
//                    indexOfAdded = counter - 1;
//                }

//                while (deque.Count > 0 && point.OriginalY > deque.Last.Value)
//                {
//                    deque.RemoveLast();
//                }
//                deque.AddLast(point.OriginalY);
//                if (point.OriginalY == deque.First.Value)
//                    indexOfAdded = counter;

//            }
//            leftBorder++;
//            counter++;
//            yield return point.WithMaxY(deque.First.Value);

//        }
//    }
//}