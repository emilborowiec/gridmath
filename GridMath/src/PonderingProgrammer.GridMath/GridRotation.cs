#region

using System;

#endregion

namespace PonderingProgrammer.GridMath
{
    public readonly struct GridRotation : IEquatable<GridRotation>
    {
        public static bool operator ==(GridRotation left, GridRotation right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GridRotation left, GridRotation right)
        {
            return !(left == right);
        }

        public GridRotation(int ticks, bool counterClockWise = false)
        {
            Ticks = ticks;
            CounterClockWise = counterClockWise;
        }

        public bool CounterClockWise { get; }
        public int Ticks { get; }

        public double ToRadians(int subdivision)
        {
            return CounterClockWise
                ? Directions.TwoPi - ((Directions.TwoPi * Ticks) / subdivision)
                : (Directions.TwoPi * Ticks) / subdivision;
        }

        public override bool Equals(object obj)
        {
            return obj is GridRotation other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CounterClockWise, Ticks);
        }

        public bool Equals(GridRotation other)
        {
            return CounterClockWise == other.CounterClockWise && Ticks == other.Ticks;
        }
    }
}