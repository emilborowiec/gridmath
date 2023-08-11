#region

using System;

#endregion

namespace GridMath.Grids.LineGrids
{
    /// <summary>
    ///     Utility class providing mapping from real values to grid values and back.
    ///     Discrete grid value N corresponds to half-opened interval in real space R[N,N+1)
    ///     When mapping grid values to real, we map to midpoint of this interval,
    ///     so that small floating point arithmetic errors won't land us outside of this interval when mapping back.
    /// </summary>
    public static class LineGridTransforms
    {
        public static int RealToGrid(float realCoordinate, double gridSpacing = 1.0)
        {
            return Convert.ToInt32(Math.Floor(realCoordinate / gridSpacing));
        }

        public static int RealToGrid(double realCoordinate, double gridSpacing = 1.0)
        {
            return Convert.ToInt32(Math.Floor(realCoordinate / gridSpacing));
        }

        public static double GridToRealCenter(int gridCoordinate, double gridSpacing = 1.0)
        {
            return GridToReal(gridCoordinate, gridSpacing);
        }

        public static double GridToReal(int gridCoordinate, double gridSpacing = 1.0, double fractionOffset = 0.5)
        {
            return (gridCoordinate + fractionOffset) * gridSpacing;
        }

        public static double GridToReal(double gridCoordinate, double gridSpacing = 1.0, double fractionOffset = 0.5)
        {
            return (gridCoordinate + fractionOffset) * gridSpacing;
        }
    }
}