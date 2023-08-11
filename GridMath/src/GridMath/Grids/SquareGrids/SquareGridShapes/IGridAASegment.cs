namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    /// <summary>
    ///     Axis-aligned segment in grid coordinates.
    /// </summary>
    public interface IGridAASegment : IGridSegment
    {
        OrthogonalGridAxis Axis { get; }
    }
}