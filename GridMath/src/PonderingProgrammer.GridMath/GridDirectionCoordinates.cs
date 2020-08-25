using System;

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    /// Simplified polar coordinates on grid that uses Direction instead of an angle
    /// </summary>
    public readonly struct GridDirectionCoordinates : IEquatable<GridDirectionCoordinates>
    {
        public GridDirectionCoordinates(Grid8Direction direction, int radius)
        {
            Direction = direction;
            Radius = radius;
        }

        public Grid8Direction Direction { get; }
        public int Radius { get; }

        public GridCoordinatePair ToGridCartesian()
        {
            return Direction switch
            {
                Grid8Direction.TopLeft => new GridCoordinatePair(-Radius, -Radius),
                Grid8Direction.Top => new GridCoordinatePair(0, -Radius),
                Grid8Direction.TopRight => new GridCoordinatePair(Radius, -Radius),
                Grid8Direction.Right => new GridCoordinatePair(Radius, 0),
                Grid8Direction.BottomRight => new GridCoordinatePair(Radius, Radius),
                Grid8Direction.Bottom => new GridCoordinatePair(0, Radius),
                Grid8Direction.BottomLeft => new GridCoordinatePair(-Radius, Radius),
                Grid8Direction.Left => new GridCoordinatePair(-Radius, 0),
                _ => new GridCoordinatePair(),
            };
        }

        public override bool Equals(object obj)
        {
            return obj is GridDirectionCoordinates other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine((int) Direction, Radius);
        }

        public static bool operator ==(GridDirectionCoordinates left, GridDirectionCoordinates right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GridDirectionCoordinates left, GridDirectionCoordinates right)
        {
            return !(left == right);
        }

        public bool Equals(GridDirectionCoordinates other)
        {
            return Direction == other.Direction && Radius == other.Radius;
        }
    }
}