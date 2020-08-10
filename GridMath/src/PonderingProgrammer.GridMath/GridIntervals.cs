using System.Collections.Generic;
using System.Linq;

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    /// Utility class for operations on collections of GridIntervals
    /// </summary>
    public static class GridIntervals
    {
        public static GridInterval[] Separate(GridInterval i1, GridInterval i2)
        {
            var depth = i1.Depth(i2);
            if (depth == 0) return new[] {i1, i2};
            var t1 = RealToGrid.ToGrid(depth / 2.0);
            var t2 = (depth % 2 == 0) ? -t1 : RealToGrid.ToGrid(-(depth + 1) / 2.0);
            return new[] {i1.Translate(t1), i2.Translate(t2)};
        }

        /// <summary>
        /// Finds subsets of overlapping intervals with positions where given set starts overlapping.
        /// </summary>
        /// <param name="intervals">intervals to check</param>
        /// <returns>A tuple where first value is the position on grid space and second is list of intervals indexes referring to input array</returns>
        public static List<List<int>> FindOverlappingIntervals(GridInterval[] intervals)
        {
            var listOfOverlappingLists = new List<List<int>>();
            var sortedStarts = intervals.Select((interval, index) => (index, interval.Min)).OrderBy(tuple  => tuple.Min).ToArray();
            var sortedEnds = intervals.Select((interval, index) => (index, interval.MaxExcl)).OrderBy(tuple => tuple.MaxExcl).ToArray();

            var startsIndex = 0;
            var endsIndex = 0;
            var unendedIntervals = 0;
            
            var maxOverlapping = 0;
            var position = 0;
            var dict = new Dictionary<int, List<int>>();
            dict[sortedStarts[0].Min] = new List<int>();
            var currentOverlapList = new List<int>();

            while (endsIndex < sortedStarts.Length && endsIndex < sortedEnds.Length)
            {
                if (sortedStarts[startsIndex].Min < sortedEnds[endsIndex].MaxExcl)
                {
                    currentOverlapList.Add(sortedStarts[startsIndex].index);
                    startsIndex++;
                }
                else
                {
                    if (currentOverlapList.Count > 1)
                    {
                        listOfOverlappingLists.Add(currentOverlapList);
                    }
                    currentOverlapList.Remove(sortedEnds[endsIndex].index);
                    currentOverlapList = new List<int>(currentOverlapList);
                    endsIndex++;
                }
            }

            return listOfOverlappingLists;
        }

        public static bool SomeOverlap(GridInterval[] intervals)
        {
            return FindOverlappingIntervals(intervals).Count > 0;
        }

        public static int FindCenterOfMass(GridInterval[] intervals)
        {
            var weightDict = new Dictionary<int, int>();
            var positionSum = 0;
            var weightSum = 0;
            foreach (var interval in intervals)
            {
                for (var i = interval.Min; i < interval.MaxExcl; i++)
                {
                    positionSum++;
                    if (!weightDict.ContainsKey(i)) weightDict[i] = 0;
                    weightDict[i] = weightDict[i] + 1;
                }
            }

            return weightDict.Sum(pair => pair.Key * pair.Value) / positionSum;
        }
    }
}