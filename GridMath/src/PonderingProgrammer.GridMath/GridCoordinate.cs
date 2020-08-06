namespace PonderingProgrammer.GridMath
{
    /// <summary>
    /// A GridCoord is a coordinate on a 2-dimensional, integer space.
    /// </summary>
    /// <remarks>
    /// Values in grid space are discreet integers and are modelled simply as int.
    /// Mapping from real space to grid space is chosen such that N on grid corresponds to R[N,N+1).
    /// For coordinates, cast to int, which truncates floating part, effectively provides mapping from R to grid.
    /// For lengths and distances, however, real are rounded up.
    /// </remarks>
    public struct GridCoordinate
    {
        public readonly int X;
        public readonly int Y;

        public GridCoordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
