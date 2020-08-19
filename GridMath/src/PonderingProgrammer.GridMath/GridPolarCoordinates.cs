#region

using System;

#endregion

namespace PonderingProgrammer.GridMath
{
    public readonly struct GridPolarCoordinates : IEquatable<GridPolarCoordinates>
    {
        private const double TwoPi = Math.PI * 2;

        public static GridPolarCoordinates FromGridCartesian(GridCoordinatePair coords)
        {
            return FromGridCartesian(coords.X, coords.Y);
        }

        public static GridPolarCoordinates FromGridCartesian(int x, int y)
        {
            var r = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            var t = Math.Atan2(-y, x);
            if (t < 0) t += TwoPi;
            return new GridPolarCoordinates(t, r);
        }

        public static bool operator ==(GridPolarCoordinates left, GridPolarCoordinates right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GridPolarCoordinates left, GridPolarCoordinates right)
        {
            return !(left == right);
        }

        public GridPolarCoordinates(double theta, double radius)
        {
            Theta = theta;
            Radius = radius;
        }

        public double Theta { get; }
        public double Radius { get; }

        public GridPolarCoordinates Rotation(double rotAngle)
        {
            var newAngle = Theta + rotAngle;
            if (newAngle < 0) newAngle += TwoPi;
            if (newAngle > TwoPi) newAngle -= TwoPi;
            return new GridPolarCoordinates(newAngle, Radius);
        }

        public GridCoordinatePair ToGridCartesian()
        {
            // Need to flip y because grid coord system goes downwards.
            // Flip must be done after mapping from real to grid, because of flooring
            var x = RealToGrid.ToGrid(Radius * Math.Cos(Theta));
            var y = -RealToGrid.ToGrid(Radius * Math.Sin(Theta));
            return new GridCoordinatePair(x, y);
        }

        public override bool Equals(object obj)
        {
            return obj is GridPolarCoordinates other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Theta, Radius);
        }

        public bool Equals(GridPolarCoordinates other)
        {
            return Theta.Equals(other.Theta) && Radius.Equals(other.Radius);
        }
    }
}