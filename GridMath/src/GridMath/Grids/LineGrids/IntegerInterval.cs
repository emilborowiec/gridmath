#region

using System;

#endregion

namespace GridMath.Grids.LineGrids
{
    /// <summary>
    ///     A closed, finite interval in a space of integral numbers.
    ///     It can be degenerate (having just one element) but it cannot be empty.
    /// </summary>
    public readonly record struct IntegerInterval : IEquatable<IntegerInterval>
    {
        public IntegerInterval(int min, int max)
        {
            if (min > max) throw new ArgumentException("min cannot be greater than max");
            Min = min;
            Max = max;
        }

        public int Min { get; }
        public int Max { get; }
        public int MaxExcl => Max + 1;
        public int Length => MaxExcl - Min;
        public int Center => LineGridTransforms.RealToGrid((Min + Max) / 2.0);
    }
}