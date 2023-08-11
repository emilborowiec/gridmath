namespace GridMath.Grids
{
    /// <summary>
    ///     Pair of coordinates on integral Cartesian plane
    /// </summary>
    public readonly record struct XYGridCoordinate(int X, int Y)
    {
        public XYGridCoordinate Translation(int x, int y)
        {
            return new XYGridCoordinate(X + x, Y + y);
        }
    }
}