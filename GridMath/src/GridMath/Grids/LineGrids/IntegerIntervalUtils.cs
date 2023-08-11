using System;

namespace GridMath.Grids.LineGrids;

public static class IntegerIntervalUtils
{
    public static IntegerInterval FromRealInterval(double min, double maxExcl)
    {
        return FromExclusiveMax(LineGridTransforms.RealToGrid(min), LineGridTransforms.RealToGrid(maxExcl));
    }

    public static IntegerInterval FromExclusiveMax(int min, int maxExcl)
    {
        if (maxExcl <= min) throw new ArgumentException("MaxExcl is exclusive and must be greater than Min");
        return new IntegerInterval(min, maxExcl - 1);
    }

    public static IntegerInterval FromLength(int min, int length)
    {
        if (length < 1) throw new ArgumentException("Interval cannot be empty - length must be greater than 0");
        return FromExclusiveMax(min, min + length);
    }

    public static IntegerInterval FromRealLength(int min, double realLength)
    {
        var gridLength = LineGridTransforms.RealToGrid(realLength);
        return FromLength(min, gridLength);
    }

    public static bool Contains(this IntegerInterval i, int value)
    {
        return value >= i.Min && value < i.MaxExcl;
    }

    public static bool Contains(this IntegerInterval i, IntegerInterval other)
    {
        return i.Min <= other.Min && i.MaxExcl >= other.MaxExcl;
    }

    public static bool Overlaps(this IntegerInterval i, IntegerInterval other)
    {
        if (i.MaxExcl <= other.Min) return false;
        return i.Min < other.MaxExcl;
    }

    public static bool Touches(this IntegerInterval i, int coordinate)
    {
        return i.Min - 1 == coordinate || i.MaxExcl == coordinate;
    }

    public static bool Touches(this IntegerInterval i, IntegerInterval other)
    {
        return i.Min == other.MaxExcl || i.MaxExcl == other.Min;
    }

    public static bool IsEven(this IntegerInterval i)
    {
        return i.Length % 2 == 0;
    }

    public static IntegerInterval[] SplitEven(this IntegerInterval i)
    {
        if (!i.IsEven()) throw new InvalidOperationException("Cannot split even an interval with odd length");
        var halfLength = i.Length / 2;
        return new[] { FromExclusiveMax(i.Min, i.Min + halfLength), FromExclusiveMax(i.Min + halfLength, i.MaxExcl) };
    }

    public static IntegerInterval Translation(this IntegerInterval i, int value)
    {
        return new IntegerInterval(i.Min + value, i.Max + value);
    }

    public static IntegerInterval SetPosition(this IntegerInterval i, int position, IntervalAnchor anchor, int offset = 0)
    {
        return anchor switch
        {
            IntervalAnchor.Start => FromLength(position + offset, i.Length),
            IntervalAnchor.Center => FromLength((position - (i.Center - i.Min)) + offset, i.Length),
            IntervalAnchor.End => FromLength((position - (i.Max - i.Min)) + offset, i.Length),
            _ => throw new ArgumentOutOfRangeException(nameof(anchor), anchor, null),
        };
    }

    public static IntegerInterval SetMin(this IntegerInterval i, int min)
    {
        return FromLength(min, i.Length);
    }

    public static IntegerInterval SetMax(this IntegerInterval i, int max)
    {
        return new IntegerInterval((max - i.Length) + 1, max);
    }

    public static IntegerInterval SetMaxExcl(this IntegerInterval i, int maxExcl)
    {
        return new IntegerInterval(maxExcl - i.Length, maxExcl - 1);
    }

    public static IntegerInterval Relate(
        this IntegerInterval i,
        IntegerInterval second,
        SpatialRelation relation,
        int offset = 0)
    {
        return relation.Second switch
        {
            IntervalAnchor.Start => i.SetPosition(second.Min, relation.First, offset),
            IntervalAnchor.Center => i.SetPosition(second.Center, relation.First, offset),
            IntervalAnchor.End => i.SetPosition(second.Max, relation.First, offset),
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
    public static IntegerInterval? Multiplication(this IntegerInterval i, double value)
    {
        var realLength = value * i.Length;
        if (realLength < 1.0)
        {
            return null;
        }

        return FromRealLength(i.Min, i.Length * value);
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
    public static int Distance(this IntegerInterval i, int coordinate)
    {
        if (i.Contains(coordinate)) return 0;
        if (coordinate > i.Max) return coordinate - i.Max;
        return coordinate - i.Min;
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
    public static int Distance(this IntegerInterval i, IntegerInterval other)
    {
        if (i.Overlaps(other)) return 0;
        return other.Min > i.Max ? i.Distance(other.Min) : i.Distance(other.Max);
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
    public static int Depth(this IntegerInterval i, int coordinate)
    {
        if (!i.Contains(coordinate)) return 0;
        var toRight = coordinate - (i.Min - 1);
        var toLeft = coordinate - i.MaxExcl;
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
    public static int Depth(this IntegerInterval i, IntegerInterval other)
    {
        if (!i.Overlaps(other)) return 0;
        var toRight = other.Max - (i.Min - 1);
        var toLeft = other.Min - i.MaxExcl;
        return Math.Abs(toRight) <= Math.Abs(toLeft) ? toRight : toLeft;
    }
}