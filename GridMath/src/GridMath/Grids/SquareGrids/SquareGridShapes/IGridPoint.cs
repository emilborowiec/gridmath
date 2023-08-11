namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    public interface IGridPoint : IGridShape
    {
        XYGridCoordinate Position { get; set; }
    }
}