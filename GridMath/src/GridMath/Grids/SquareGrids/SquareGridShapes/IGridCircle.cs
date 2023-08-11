namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    public interface IGridCircle : IGridShape
    {
        int Radius { get; set; }
        XYGridCoordinate Center { get; set; }
    }
}