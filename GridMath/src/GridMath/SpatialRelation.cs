#region

using System;

#endregion

namespace GridMath
{
    public readonly struct SpatialRelation : IEquatable<SpatialRelation>
    {
        public static SpatialRelation StartToStart()
        {
            return new SpatialRelation(IntervalAnchor.Start, IntervalAnchor.Start);
        }

        public static SpatialRelation StartToCenter()
        {
            return new SpatialRelation(IntervalAnchor.Start, IntervalAnchor.Center);
        }

        public static SpatialRelation StartToEnd()
        {
            return new SpatialRelation(IntervalAnchor.Start, IntervalAnchor.End);
        }

        public static SpatialRelation CenterToStart()
        {
            return new SpatialRelation(IntervalAnchor.Center, IntervalAnchor.Start);
        }

        public static SpatialRelation CenterToCenter()
        {
            return new SpatialRelation(IntervalAnchor.Center, IntervalAnchor.Center);
        }

        public static SpatialRelation CenterToEnd()
        {
            return new SpatialRelation(IntervalAnchor.Center, IntervalAnchor.End);
        }

        public static SpatialRelation EndToStart()
        {
            return new SpatialRelation(IntervalAnchor.End, IntervalAnchor.Start);
        }

        public static SpatialRelation EndToCenter()
        {
            return new SpatialRelation(IntervalAnchor.End, IntervalAnchor.Center);
        }

        public static SpatialRelation EndToEnd()
        {
            return new SpatialRelation(IntervalAnchor.End, IntervalAnchor.End);
        }

        public static bool operator ==(SpatialRelation left, SpatialRelation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SpatialRelation left, SpatialRelation right)
        {
            return !(left == right);
        }

        public SpatialRelation(IntervalAnchor first, IntervalAnchor second)
        {
            First = first;
            Second = second;
        }

        public IntervalAnchor First { get; }
        public IntervalAnchor Second { get; }

        public override bool Equals(object obj)
        {
            return obj is SpatialRelation other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) First, (int) Second);
        }

        public bool Equals(SpatialRelation other)
        {
            return First == other.First && Second == other.Second;
        }
    }
}