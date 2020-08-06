namespace PonderingProgrammer.GridMath
{
    /// <summary>
    /// An IntCoord is a coordinate on a 2-dimensional, integer space.
    /// </summary>
    /// <remarks>
    /// Values in grid space are discreet integers and are modelled simply as int.
    /// Mapping from real space to grid space is chosen such that N on grid corresponds to R[N,N+1).
    /// Cast to int, which truncates floating part, effectively provides mapping from R to grid.
    /// </remarks>
    public struct IntCoord
    {
        public readonly int X;
        public readonly int Y;

        public IntCoord(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
