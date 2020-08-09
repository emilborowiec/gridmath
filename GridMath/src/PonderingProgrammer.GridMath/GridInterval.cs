using System;

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    /// A GridInterval, mathematically speaking, is a closed, finite interval in an integer space of numbers.
    /// It can be degenerate (having just one element) but it cannot be empty.
    /// </summary>
    /// <remarks>
    /// This mapping can be implemented by getting Floor of the real value.
    /// Note that simple cast of double to int only truncates floating part and gives correct results only for positive numbers.
    /// </remarks>
    /// <remarks>
    /// The GridInterval is primarily represented as a pair of numbers <c>Min</c> and <c>Max</c>.
    /// It also provides alternative representations which are commonly used in computations:
    /// <list type="bullet">
    /// <item>
    /// <term><c>Length</c></term>
    /// <description>Number of elements in the interval</description>
    /// </item>
    /// <item>
    /// <term><c>MaxExcl</c></term>
    /// <description>Open (exclusive) end of the interval</description>
    /// </item>
    /// </list>  
    /// </remarks>
    public readonly struct GridInterval
    {
        public static GridInterval FromRealInterval(double min, double maxExcl)
        {
            return FromExclusiveMax(RealToGrid.ToGrid(min), RealToGrid.ToGrid(maxExcl));
        }
        
        public static GridInterval FromExclusiveMax(int min, int maxExcl)
        {
            if (maxExcl <= min) throw new ArgumentException("MaxExcl is exclusive and must be greater than Min");
            return new GridInterval(min, maxExcl - 1);
        }

        public static GridInterval FromLength(int min, int length)
        {
            if (length < 1) throw new ArgumentException("Interval cannot be empty - length must be greater than 0");
            return FromExclusiveMax(min, min + length);
        }

        public static GridInterval FromRealLength(int min, double realLength)
        {
            var gridLength = RealToGrid.ToGrid(realLength);
            return FromLength(min, gridLength);
        }

        public static GridInterval[] Separate(GridInterval i1, GridInterval i2)
        {
            var depth = i1.Depth(i2);
            if (depth == 0) return new[] {i1, i2};
            var t1 = RealToGrid.ToGrid(depth / 2.0);
            var t2 = (depth % 2 == 0) ? -t1 : RealToGrid.ToGrid(-(depth + 1) / 2.0);
            return new[] {i1.Translate(t1), i2.Translate(t2)};
        }

        public readonly int Min;
        public readonly int Max;
        
        public int MaxExcl => Max + 1;
        public int Length => MaxExcl - Min;
        public int Center => RealToGrid.ToGrid((Min + Max) / 2.0);
        


        public GridInterval(int min, int max)
        {
            if (min > max) throw new ArgumentException("min cannot be greater than max");
            Min = min;
            Max = max;
        }

        public bool Contains(int value)
        {
            return value >= Min && value < MaxExcl;
        }

        public bool Contains(GridInterval other)
        {
            return Min <= other.Min && MaxExcl >= other.MaxExcl;
        }

        public bool Overlaps(GridInterval other)
        {
            if (MaxExcl <= other.Min) return false;
            return Min < other.MaxExcl;
        }

        public bool Touches(GridInterval other)
        {
            return Min == other.MaxExcl || MaxExcl == other.Min;
        }

        public bool IsEven() => Length % 2 == 0;

        public GridInterval[] SplitEven()
        {
            if (!IsEven()) throw new InvalidOperationException("Cannot split even an interval with odd length");
            var halfLength = Length / 2;
            return new[] { FromExclusiveMax(Min, Min + halfLength), FromExclusiveMax(Min + halfLength, MaxExcl)};
        }

        public GridInterval Translate(int value)
        {
            return new GridInterval(Min + value, Max + value);
        }

        public GridInterval SetPosition(int position, IntervalAnchor anchor, int offset = 0)
        {
            return anchor switch
            {
                IntervalAnchor.Start => FromLength(position + offset, Length),
                IntervalAnchor.Center => FromLength(position - (Center - Min) + offset, Length),
                IntervalAnchor.End => FromLength(position - (Max - Min) + offset, Length),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public GridInterval SetMin(int min)
        {
            return FromLength(min, Length);
        }

        public GridInterval SetMax(int max)
        {
            return new GridInterval(max - Length + 1, max);
        }

        public GridInterval SetMaxExcl(int maxExcl)
        {
            return new GridInterval(maxExcl - Length, maxExcl - 1);
        }

        public GridInterval Relate(GridInterval second, Relation relation, int offset = 0)
        {
            return relation.Second switch
            {
                IntervalAnchor.Start => SetPosition(second.Min, relation.First, offset),
                IntervalAnchor.Center => SetPosition(second.Center, relation.First, offset),
                IntervalAnchor.End => SetPosition(second.Max, relation.First, offset),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        /// <summary>
        /// Creates interval starting at the same Min value, but having length multiplied by given value.
        /// </summary>
        /// <param name="value">multiplier of Length</param>
        /// <returns>GridInterval with same Min and multiplied Length mapped to grid space.
        /// Returns null if resulting length is 0 or negative.</returns>
        public GridInterval? Multiply(double value)
        {
            var realLength = value * Length;
            if (realLength < 1.0)
                return null;
            return FromRealLength(Min, Length * value);
        }

        /// <summary>
        /// Measures how far is the interval to given point on the same axis.
        /// </summary>
        /// <param name="value">a value on common axis with the interval</param>
        /// <returns>Positive distance if the value is value is to the right of this interval.
        /// Negative distance if the value is to the left of this interval.
        /// 0 if this interval contains value</returns>
        public int Distance(int value)
        {
            if (Contains(value)) return 0;
            if (value > Max) return value - Max;
            return value - Min;
        }
        
        /// <summary>
        /// Measures how far is the interval to other interval on the same axis.
        /// </summary>
        /// <param name="other">a GridInterval on the same axis</param>
        /// <returns>0 if the intervals are overlapping.
        /// Positive distance if other is to the right.
        /// Negative distance if other is to the left.</returns>
        public int Distance(GridInterval other)
        {
            if (Overlaps(other)) return 0;
            return other.Min > Max ? Distance(other.Min) : Distance(other.Max);
        }

        /// <summary>
        /// Measures how deep is the value inside the interval.
        /// </summary>
        /// <param name="value">a value on common axis with the interval</param>
        /// <returns>0 if the value is outside of interval.
        /// Positive distance if shortest translation to avoid the value is to the right.
        /// Negative distance if shortest translation is to the left.</returns>
        public int Depth(int value)
        {
            if (!Contains(value)) return 0;
            var toRight = value - (Min - 1);
            var toLeft = value - MaxExcl;
            return Math.Abs(toRight) <= Math.Abs(toLeft) ? toRight : toLeft;
        }

        /// <summary>
        /// Measures how deep is the interval overlapped with other interval on the same axis.
        /// </summary>
        /// <param name="other">a GridInterval on the same axis</param>
        /// <returns>0 if the intervals are not overlapping.
        /// Positive distance if shortest translation to separate is to the right.
        /// Negative distance if shortest translation is to the left.</returns>
        public int Depth(GridInterval other)
        {
            if (!Overlaps(other)) return 0;
            var toRight = other.Max - (Min - 1);
            var toLeft = other.Min - MaxExcl;
            return Math.Abs(toRight) <= Math.Abs(toLeft) ? toRight : toLeft;
        }
        
        
    }
}
