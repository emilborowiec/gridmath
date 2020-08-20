#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    ///     Utility class for operations on collections of GridIntervals
    /// </summary>
    public static class GridIntervals
    {
        public static GridInterval[] Separate(GridInterval i1, GridInterval i2)
        {
            var depth = i1.Depth(i2);
            if (depth == 0) return new[] {i1, i2};
            var t1 = RealToGrid.ToGrid(depth / 2.0);
            var t2 = depth % 2 == 0 ? -t1 : RealToGrid.ToGrid(-(depth + 1) / 2.0);
            return new[] {i1.Translation(t1), i2.Translation(t2)};
        }

        /// <summary>
        ///     Finds subsets of overlapping intervals with positions where given set starts overlapping.
        /// </summary>
        /// <param name="intervals">intervals to check</param>
        /// <returns>
        ///     A tuple where first value is the position on grid space and second is list of intervals indexes referring to
        ///     input array
        /// </returns>
        public static List<List<int>> FindOverlappingIntervals(GridInterval[] intervals)
        {
            var listOfOverlappingLists = new List<List<int>>();
            var sortedStarts = intervals.Select((interval, index) => (index, interval.Min))
                                        .OrderBy(tuple => tuple.Min)
                                        .ToArray();
            var sortedEnds = intervals.Select((interval, index) => (index, interval.MaxExcl))
                                      .OrderBy(tuple => tuple.MaxExcl)
                                      .ToArray();

            var startsIndex = 0;
            var endsIndex = 0;

            List<int> currentOverlapList = null;

            while (endsIndex < sortedEnds.Length)
            {
                if (startsIndex < sortedStarts.Length && sortedStarts[startsIndex].Min < sortedEnds[endsIndex].MaxExcl)
                {
                    if (currentOverlapList == null) currentOverlapList = new List<int>();
                    currentOverlapList.Add(sortedStarts[startsIndex].index);
                    startsIndex++;
                }
                else
                {
                    if (currentOverlapList != null && currentOverlapList.Count > 1)
                    {
                        if (!listOfOverlappingLists.Any(
                            list =>
                                list.Intersect(currentOverlapList).Count() ==
                                currentOverlapList.Count))
                        {
                            listOfOverlappingLists.Add(currentOverlapList);
                            currentOverlapList = new List<int>(currentOverlapList);
                        }
                    }

                    currentOverlapList.Remove(sortedEnds[endsIndex].index);
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
            if (intervals == null) throw new ArgumentNullException(nameof(intervals));

            var weightDict = new Dictionary<int, int>();
            var positionSum = 0;
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

        public static void Pack(
            GridInterval[] intervals,
            IntervalAnchor alignment = IntervalAnchor.Center,
            int spacing = 0)
        {
            if (intervals == null) throw new ArgumentNullException(nameof(intervals));

            var originalTotalMin = int.MaxValue;
            var originalTotalMax = int.MinValue;
            var centerOfMass = FindCenterOfMass(intervals);
            var nextMin = 0;
            var translation = 0;
            for (var i = 0; i < intervals.Length; i++)
            {
                if (originalTotalMin > intervals[i].Min) originalTotalMin = intervals[i].Min;

                if (originalTotalMax < intervals[i].Max) originalTotalMax = intervals[i].Max;
                intervals[i] = intervals[i].SetMin(nextMin);
                nextMin = intervals[i].MaxExcl + spacing;
            }

            translation = alignment switch
            {
                IntervalAnchor.Start => originalTotalMin,
                IntervalAnchor.End => originalTotalMax - intervals[^1].Max,
                IntervalAnchor.Center => centerOfMass - FindCenterOfMass(intervals),
                _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null),
            };

            foreach (var interval in intervals) interval.Translation(translation);
        }
    }
}