using GridMath.Geometry;
using GridMath.Grids.LineGrids;

namespace GridMath.Grids.RectGrids;

public static class RectGridTransforms
{
    public static RealCoordinate CalculateTileCenter(int gridX, int gridY, double tileWidth, double tileHeight)
    {
        return new RealCoordinate(LineGridTransforms.GridToRealCenter(gridX, tileWidth), LineGridTransforms.GridToRealCenter(gridY, tileHeight));
    }
    
    public static RealCoordinate CalculateTileCenter(XYGridCoordinate gridCoordinate, double tileWidth, double tileHeight)
    {
        return CalculateTileCenter(gridCoordinate.X, gridCoordinate.Y, tileWidth, tileHeight);
    }

    public static XYGridCoordinate RealToGrid(RealCoordinate realCoordinate, double tileWidth, double tileHeight)
    {
        return new XYGridCoordinate(LineGridTransforms.RealToGrid(realCoordinate.X, tileWidth), LineGridTransforms.RealToGrid(realCoordinate.Y, tileHeight));
    }

    public static RealCoordinate[] CalculateRectTileVertices(int gridX, int gridY, double tileWidth, double tileHeight)
    {
        var left = gridX * tileWidth;
        var right = (gridX + 1) * tileWidth;
        var top = gridY * tileHeight;
        var bottom = (gridY + 1) * tileHeight;

        return new[]
        {
            new RealCoordinate(left, top), new RealCoordinate(right, top), new RealCoordinate(right, bottom),
            new RealCoordinate(left, bottom),
        };
    }
}