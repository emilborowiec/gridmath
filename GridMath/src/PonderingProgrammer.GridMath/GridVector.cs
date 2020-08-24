#region

using System;

#endregion

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    ///     Two-dimensional vector in grid space
    /// </summary>
    public readonly struct GridVector : IEquatable<GridVector>
    {
        /// <summary>
        ///     Correctly maps coordinates from continuous coordinate system to grid coordinate system by flooring the values.
        /// </summary>
        /// <param name="x">x ordinate in real space</param>
        /// <param name="y">y ordinate in real space</param>
        /// <returns>Corresponding GridVector</returns>
        public static GridVector FromReal(double x, double y)
        {
            return new GridVector(GridConvert.ToGrid(x), GridConvert.ToGrid(y));
        }

        public static bool operator ==(GridVector left, GridVector right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GridVector left, GridVector right)
        {
            return !(left == right);
        }

        public GridVector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public double Length(int x, int y)
        {
            return Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2));
        }

        public double DotProduct(GridVector other)
        {
            return (X * other.X) + (Y * other.Y);
        }

        public bool Equals(GridVector other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is GridVector other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }
}