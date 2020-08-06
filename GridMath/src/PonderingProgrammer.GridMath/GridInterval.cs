using System;

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    /// A GridInterval, mathematically speaking, is a closed, finite interval in an integer space of numbers.
    /// It can be degenerate (having just one element) but it cannot be empty.
    /// </summary>
    /// <remarks>
    /// Values in grid space are discreet integers and are modelled simply as int.
    /// Mapping from real space to grid space is chosen such that N on grid corresponds to R[N,N+1).
    /// Cast to int, which truncates floating part, effectively provides mapping from R to grid.
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
        public static GridInterval FromExclusiveMax(int min, int maxExcl)
        {
            if (maxExcl <= min) throw new ArgumentException("MaxExcl is exclusive and must be greater than Min");
            return new GridInterval(min, maxExcl - 1);
        }

        public static GridInterval FromLength(int min, int range)
        {
            if (range < 1) throw new ArgumentException("Interval cannot be empty - length must be greater than 0");
            return FromExclusiveMax(min, min + range);
        }

        public readonly int Min;
        public readonly int Max;
        
        public int MaxExcl => Max + 1;
        public int Length => MaxExcl - Min;
        public int Center => (Min + Max) / 2;
        


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
            var halfRange = Length / 2;
            return new[] { FromExclusiveMax(Min, Min + halfRange), FromExclusiveMax(Min + halfRange, MaxExcl)};
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
            return FromLength(min, min + Length);
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

        public GridInterval Multiply(double fraction)
        {
            var fractionLength = (int)(Length * fraction);
            return FromLength(Min, fractionLength);
        }
    }
}
