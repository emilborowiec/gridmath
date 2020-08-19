#region

using System;

#endregion

namespace PonderingProgrammer.GridMath
{
    public readonly struct Relation : IEquatable<Relation>
    {
        public static Relation StartToStart()
        {
            return new Relation(IntervalAnchor.Start, IntervalAnchor.Start);
        }

        public static Relation StartToCenter()
        {
            return new Relation(IntervalAnchor.Start, IntervalAnchor.Center);
        }

        public static Relation StartToEnd()
        {
            return new Relation(IntervalAnchor.Start, IntervalAnchor.End);
        }

        public static Relation CenterToStart()
        {
            return new Relation(IntervalAnchor.Center, IntervalAnchor.Start);
        }

        public static Relation CenterToCenter()
        {
            return new Relation(IntervalAnchor.Center, IntervalAnchor.Center);
        }

        public static Relation CenterToEnd()
        {
            return new Relation(IntervalAnchor.Center, IntervalAnchor.End);
        }

        public static Relation EndToStart()
        {
            return new Relation(IntervalAnchor.End, IntervalAnchor.Start);
        }

        public static Relation EndToCenter()
        {
            return new Relation(IntervalAnchor.End, IntervalAnchor.Center);
        }

        public static Relation EndToEnd()
        {
            return new Relation(IntervalAnchor.End, IntervalAnchor.End);
        }

        public static bool operator ==(Relation left, Relation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Relation left, Relation right)
        {
            return !(left == right);
        }

        public Relation(IntervalAnchor first, IntervalAnchor second)
        {
            First = first;
            Second = second;
        }

        public IntervalAnchor First { get; }
        public IntervalAnchor Second { get; }

        public override bool Equals(object obj)
        {
            return obj is Relation other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) First, (int) Second);
        }

        public bool Equals(Relation other)
        {
            return First == other.First && Second == other.Second;
        }
    }
}