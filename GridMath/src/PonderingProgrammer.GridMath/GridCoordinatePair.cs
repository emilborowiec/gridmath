using System;

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    /// A GridCoord is a coordinate on a 2-dimensional, integer space.
    /// </summary>
    /// <remarks>
    /// Values in grid space are discreet integers and are modelled simply as int.
    /// Mapping from real space to grid space is chosen such that N on grid corresponds to R[N,N+1).
    /// This mapping can be implemented by getting Floor of the real value.
    /// Note that simple cast of double to int only truncates floating part and gives correct results only for positive numbers.
    /// </remarks>
    public readonly struct GridCoordinatePair
    {
        public readonly int X;
        public readonly int Y;

        /// <summary>
        /// Correctly maps coordinates from continuous coordinate system to grid coordinate system by flooring the values.
        /// </summary>
        /// <param name="x">x ordinate in real space</param>
        /// <param name="y">y ordinate in real space</param>
        /// <returns>Corresponding GridCoordinate</returns>
        public static  GridCoordinatePair FromReal(double x, double y)
        {
            return new GridCoordinatePair(RealToGrid.ToGrid(x), RealToGrid.ToGrid(y));
        }

        public GridCoordinatePair(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
