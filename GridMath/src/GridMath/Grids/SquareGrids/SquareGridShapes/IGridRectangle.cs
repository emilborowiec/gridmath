namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    public interface IGridRectangle : IGridShape
    {
        int MinX { get; set; }
        int MinY { get; set; }
        int MaxX { get; set; }
        int MaxY { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        XYGridCoordinate TopLeft { get; set; }
    }
}