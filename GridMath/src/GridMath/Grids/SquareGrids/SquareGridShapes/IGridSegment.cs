namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    /// <summary>
    ///     IGridSegment is defined with start and end points,
    ///     and is a collection of all the points .
    /// </summary>
    public interface IGridSegment : IGridShape
    {
        int Dx { get; }
        int Dy { get; }
        XYGridCoordinate A { get; set; }
        XYGridCoordinate B { get; set; }
    }
}