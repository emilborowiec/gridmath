#region

using System;

#endregion

namespace PonderingProgrammer.GridMath
{
    /// <summary>
    ///     Utility class providing mapping from real values to grid values.
    ///     Discrete grid value N corresponds to half-opened interval in real space R[N,N+1)
    /// </summary>
    public static class RealToGrid
    {
        public static int ToGrid(float value)
        {
            return Convert.ToInt32(Math.Floor(value));
        }

        public static int ToGrid(double value)
        {
            return Convert.ToInt32(Math.Floor(value));
        }
    }
}