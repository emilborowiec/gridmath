namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    public interface IGridMultiPoint : IGridShape
    {
        bool AddPosition(XYGridCoordinate position);
        bool RemovePosition(XYGridCoordinate position);
    }
}