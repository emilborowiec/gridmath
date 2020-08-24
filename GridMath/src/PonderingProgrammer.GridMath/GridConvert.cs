#region

using System;

#endregion

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    ///     Utility class providing mapping from real values to grid values and back.
    ///     Discrete grid value N corresponds to half-opened interval in real space R[N,N+1)
    ///     When mapping grid values to real, we map to midpoint of this interval,
    ///     so that small floating point arithmetic errors won't land us outside of this interval when mapping back.
    /// </summary>
    public static class GridConvert
    {
        public static int ToGrid(float value)
        {
            return Convert.ToInt32(Math.Floor(value));
        }

        public static int ToGrid(double value)
        {
            return Convert.ToInt32(Math.Floor(value));
        }

        public static double ToReal(int gridValue)
        {
            return gridValue + 0.5;
        }

        public static double ToReal(double gridValue)
        {
            return gridValue + 0.5;
        }
    }
}