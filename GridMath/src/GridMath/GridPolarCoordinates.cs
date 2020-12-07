#region

using System;

#endregion

namespace GridMath
{
    /// <summary>
    ///     Grid Polar Coordinate System works just like normal Polar Coordinate Systme,
    ///     but its origin is translated (0.5, 0.5) relative to Grid Coordinate System.
    /// </summary>
    public readonly struct GridPolarCoordinates : IEquatable<GridPolarCoordinates>
    {
        public static GridPolarCoordinates FromGridCartesian(GridCoordinatePair coords)
        {
            return FromGridCartesian(coords.X, coords.Y);
        }

        public static GridPolarCoordinates FromGridCartesian(int x, int y)
        {
            var r = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
            var t = Math.Atan2(y, x);
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
            if (radius < 0)
            {
                radius = -radius;
                theta += Math.PI;
            }

            Theta = Directions.WrapAngle(theta);
            Radius = radius;
        }

        public double Theta { get; }
        public double Radius { get; }

        public GridPolarCoordinates Rotation(double rotAngle)
        {
            return new GridPolarCoordinates(Directions.WrapAngle(Theta + rotAngle), Radius);
        }

        public GridCoordinatePair ToGridCartesian()
        {
            var (x, y) = ToRealCartesian();
            return new GridCoordinatePair(GridConvert.ToGrid(x), GridConvert.ToGrid(y));
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

        private (double x, double y) ToRealCartesian()
        {
            // grid polar coordinates convert to floating point values over grid space
            var x = Radius * Math.Cos(Theta);
            var y = Radius * Math.Sin(Theta);
            // those values are now translated to real space, which has origin in (0.5,0.5)
            return (GridConvert.ToReal(x), GridConvert.ToReal(y));
        }
    }
}