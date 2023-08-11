namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    public interface IGridFan : IGridShape
    {
        int Radius { get; set; }
        XYGridCoordinate Origin { get; set; }
        Grid8Direction Direction { get; set; }
    }
}