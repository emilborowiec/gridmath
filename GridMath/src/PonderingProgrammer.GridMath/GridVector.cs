using System;

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    /// Two-dimensional vector in grid space
    /// </summary>
    public class GridVector
    {
        public readonly int X;
        public readonly int Y;

        /// <summary>
        /// Correctly maps coordinates from continuous coordinate system to grid coordinate system by flooring the values.
        /// </summary>
        /// <param name="x">x ordinate in real space</param>
        /// <param name="y">y ordinate in real space</param>
        /// <returns>Corresponding GridVector</returns>
        public static  GridVector FromReal(double x, double y)
        {
            return new GridVector(RealToGrid.ToGrid(x), RealToGrid.ToGrid(y));
        }

        public GridVector(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        
        public double Length(int x, int y)
        {
            return Math.Sqrt(Math.Pow(x - X, 2) + Math.Pow(y - Y, 2));
        }

        public double DotProduct(GridVector other) => (X * other.X) + (Y * other.Y);
    }
}