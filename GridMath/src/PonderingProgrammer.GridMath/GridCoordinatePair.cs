#region

using System;

#endregion

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    ///     A GridCoord is a coordinate on a 2-dimensional, integer space.
    /// </summary>
    /// <remarks>
    ///     Values in grid space are discreet integers and are modelled simply as int.
    ///     Mapping from real space to grid space is chosen such that N on grid corresponds to R[N,N+1).
    ///     This mapping can be implemented by getting Floor of the real value.
    ///     Note that simple cast of double to int only truncates floating part and gives correct results only for positive
    ///     numbers.
    /// </remarks>
    public readonly struct GridCoordinatePair : IEquatable<GridCoordinatePair>
    {
        /// <summary>
        ///     Correctly maps coordinates from continuous coordinate system to grid coordinate system by flooring the values.
        /// </summary>
        /// <param name="x">x ordinate in real space</param>
        /// <param name="y">y ordinate in real space</param>
        /// <returns>Corresponding GridCoordinate</returns>
        public static GridCoordinatePair FromReal(double x, double y)
        {
            return new GridCoordinatePair(GridConvert.ToGrid(x), GridConvert.ToGrid(y));
        }

        public static (double x, double y) ToReal(GridCoordinatePair gridCoordinates)
        {
            return (GridConvert.ToReal(gridCoordinates.X), GridConvert.ToReal(gridCoordinates.Y));
        }

        public static bool operator ==(GridCoordinatePair left, GridCoordinatePair right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GridCoordinatePair left, GridCoordinatePair right)
        {
            return !(left == right);
        }

        public GridCoordinatePair(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public int ManhattanDistance(int x, int y)
        {
            return Math.Abs(x - X) + Math.Abs(y - Y);
        }

        public int ChebyshevDistance(int x, int y)
        {
            return Math.Max(Math.Abs(x - X), Math.Abs(y - Y));
        }

        public double EuclideanDistance(int x, int y)
        {
            return Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2));
        }

        public double Sed(int x, int y)
        {
            return Math.Pow(x - X, 2) + Math.Pow(y - Y, 2);
        }

        public GridCoordinatePair Translation(int x, int y)
        {
            return new GridCoordinatePair(X + x, Y + y);
        }

        public bool Equals(GridCoordinatePair other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is GridCoordinatePair other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}