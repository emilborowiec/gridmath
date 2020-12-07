#region

using System;

#endregion

namespace GridMath
{
    /// <summary>
    ///     A GridInterval, mathematically speaking, is a closed, finite interval in an integer space of numbers.
    ///     It can be degenerate (having just one element) but it cannot be empty.
    /// </summary>
    /// <remarks>
    ///     This mapping can be implemented by getting Floor of the real value.
    ///     Note that simple cast of double to int only truncates floating part and gives correct results only for positive
    ///     numbers.
    /// </remarks>
    /// <remarks>
    ///     The GridInterval is primarily represented as a pair of numbers <c>Min</c> and <c>Max</c>.
    ///     It also provides alternative representations which are commonly used in computations:
    ///     <list type="bullet">
    ///         <item>
    ///             <term>
    ///                 <c>Length</c>
    ///             </term>
    ///             <description>Number of elements in the interval</description>
    ///         </item>
    ///         <item>
    ///             <term>
    ///                 <c>MaxExcl</c>
    ///             </term>
    ///             <description>Open (exclusive) end of the interval</description>
    ///         </item>
    ///     </list>
    /// </remarks>
    public readonly struct GridInterval : IEquatable<GridInterval>
    {
        public static GridInterval FromRealInterval(double min, double maxExcl)
        {
            return FromExclusiveMax(GridConvert.ToGrid(min), GridConvert.ToGrid(maxExcl));
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
            var gridLength = GridConvert.ToGrid(realLength);
            return FromLength(min, gridLength);
        }

        public static bool operator ==(GridInterval left, GridInterval right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GridInterval left, GridInterval right)
        {
            return !(left == right);
        }

        public GridInterval(int min, int max)
        {
            if (min > max) throw new ArgumentException("min cannot be greater than max");
            Min = min;
            Max = max;
        }

        public int Min { get; }
        public int Max { get; }
        public int MaxExcl => Max + 1;
        public int Length => MaxExcl - Min;
        public int Center => GridConvert.ToGrid((Min + Max) / 2.0);


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

        public bool Touches(int coordinate)
        {
            return Min - 1 == coordinate || MaxExcl == coordinate;
        }

        public bool Touches(GridInterval other)
        {
            return Min == other.MaxExcl || MaxExcl == other.Min;
        }

        public bool IsEven()
        {
            return Length % 2 == 0;
        }

        public GridInterval[] SplitEven()
        {
            if (!IsEven()) throw new InvalidOperationException("Cannot split even an interval with odd length");
            var halfLength = Length / 2;
            return new[] {FromExclusiveMax(Min, Min + halfLength), FromExclusiveMax(Min + halfLength, MaxExcl)};
        }

        public GridInterval Translation(int value)
        {
            return new GridInterval(Min + value, Max + value);
        }

        public GridInterval SetPosition(int position, IntervalAnchor anchor, int offset = 0)
        {
            return anchor switch
            {
                IntervalAnchor.Start => FromLength(position + offset, Length),
                IntervalAnchor.Center => FromLength((position - (Center - Min)) + offset, Length),
                IntervalAnchor.End => FromLength((position - (Max - Min)) + offset, Length),
                _ => throw new ArgumentOutOfRangeException(nameof(anchor), anchor, null),
            };
        }

        public GridInterval SetMin(int min)
        {
            return FromLength(min, Length);
        }

        public GridInterval SetMax(int max)
        {
            return new GridInterval((max - Length) + 1, max);
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
                _ => throw new ArgumentOutOfRangeException(nameof(relation), relation.Second, null),
            };
        }

        /// <summary>
        ///     Creates interval starting at the same Min value, but having length multiplied by given value.
        /// </summary>
        /// <param name="value">multiplier of Length</param>
        /// <returns>
        ///     GridInterval with same Min and multiplied Length mapped to grid space.
        ///     Returns null if resulting length is 0 or negative.
        /// </returns>
        public GridInterval? Multiplication(double value)
        {
            var realLength = value * Length;
            if (realLength < 1.0)
            {
                return null;
            }

            return FromRealLength(Min, Length * value);
        }

        /// <summary>
        ///     Measures how far is the interval to given point on the same axis.
        /// </summary>
        /// <param name="coordinate">a coordinate to which we measure distance</param>
        /// <returns>
        ///     Positive distance if the value is value is to the right of this interval.
        ///     Negative distance if the value is to the left of this interval.
        ///     0 if this interval contains the value
        /// </returns>
        public int Distance(int coordinate)
        {
            if (Contains(coordinate)) return 0;
            if (coordinate > Max) return coordinate - Max;
            return coordinate - Min;
        }

        /// <summary>
        ///     Measures how far is the interval to other interval on the same axis.
        /// </summary>
        /// <param name="other">another GridInterval to which we measure distance</param>
        /// <returns>
        ///     0 if the intervals are overlapping.
        ///     Positive distance if other is to the right.
        ///     Negative distance if other is to the left.
        /// </returns>
        public int Distance(GridInterval other)
        {
            if (Overlaps(other)) return 0;
            return other.Min > Max ? Distance(other.Min) : Distance(other.Max);
        }

        /// <summary>
        ///     Measures how deep is the value inside the interval.
        /// </summary>
        /// <param name="coordinate">a coordinate to which we measure depth</param>
        /// <returns>
        ///     0 if the value is outside of interval.
        ///     Positive distance if shortest translation to avoid the value is to the right.
        ///     Negative distance if shortest translation is to the left.
        /// </returns>
        public int Depth(int coordinate)
        {
            if (!Contains(coordinate)) return 0;
            var toRight = coordinate - (Min - 1);
            var toLeft = coordinate - MaxExcl;
            return Math.Abs(toRight) <= Math.Abs(toLeft) ? toRight : toLeft;
        }

        /// <summary>
        ///     Measures how deep is the interval overlapped with other interval on the same axis.
        /// </summary>
        /// <param name="other">another GridInterval to which we measure depth</param>
        /// <returns>
        ///     0 if the intervals are not overlapping.
        ///     Positive distance if shortest translation to separate is to the right.
        ///     Negative distance if shortest translation is to the left.
        /// </returns>
        public int Depth(GridInterval other)
        {
            if (!Overlaps(other)) return 0;
            var toRight = other.Max - (Min - 1);
            var toLeft = other.Min - MaxExcl;
            return Math.Abs(toRight) <= Math.Abs(toLeft) ? toRight : toLeft;
        }

        public override bool Equals(object obj)
        {
            return obj is GridInterval other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }

        public bool Equals(GridInterval other)
        {
            return Min == other.Min && Max == other.Max;
        }
    }
}