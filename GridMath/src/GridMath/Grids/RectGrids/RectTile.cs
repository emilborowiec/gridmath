using GridMath.Geometry;
using GridMath.Grids.LineGrids;
using System.Collections.Generic;

namespace GridMath.Grids.RectGrids;

public class RectTile
{
    public RectTile(int gridX, int gridY, double width, double height)
    {
        GridX = gridX;
        GridY = gridY;

        Vertices = RectGridTransforms.CalculateRectTileVertices(gridX, gridY, width, height);
        Center = RectGridTransforms.CalculateTileCenter(gridX, gridY, width, height);
    }

    public int GridX { get; }
    public int GridY { get; }
    public RealCoordinate Center { get; }
    public IReadOnlyList<RealCoordinate> Vertices { get; }
}