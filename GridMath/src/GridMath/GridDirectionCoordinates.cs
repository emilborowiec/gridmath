#region

using GridMath.Grids;
using System;

#endregion

namespace GridMath
{
    /// <summary>
    ///     Simplified polar coordinates on grid that uses Direction instead of an angle
    /// </summary>
    public readonly struct GridDirectionCoordinates : IEquatable<GridDirectionCoordinates>
    {
        public static bool operator ==(GridDirectionCoordinates left, GridDirectionCoordinates right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GridDirectionCoordinates left, GridDirectionCoordinates right)
        {
            return !(left == right);
        }

        public GridDirectionCoordinates(Grid8Direction direction, int radius)
        {
            Direction = direction;
            Radius = radius;
        }

        public Grid8Direction Direction { get; }
        public int Radius { get; }

        public XYGridCoordinate ToGridCartesian()
        {
            return Direction switch
            {
                Grid8Direction.TopLeft => new XYGridCoordinate(-Radius, -Radius),
                Grid8Direction.Top => new XYGridCoordinate(0, -Radius),
                Grid8Direction.TopRight => new XYGridCoordinate(Radius, -Radius),
                Grid8Direction.Right => new XYGridCoordinate(Radius, 0),
                Grid8Direction.BottomRight => new XYGridCoordinate(Radius, Radius),
                Grid8Direction.Bottom => new XYGridCoordinate(0, Radius),
                Grid8Direction.BottomLeft => new XYGridCoordinate(-Radius, Radius),
                Grid8Direction.Left => new XYGridCoordinate(-Radius, 0),
                _ => new XYGridCoordinate(),
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

        public bool Equals(GridDirectionCoordinates other)
        {
            return Direction == other.Direction && Radius == other.Radius;
        }
    }
}