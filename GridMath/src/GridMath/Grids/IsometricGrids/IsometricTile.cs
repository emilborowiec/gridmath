using GridMath.Geometry;
using System.Collections.Generic;

namespace GridMath.Grids.IsometricGrids;

public class IsometricTile
{
    public IsometricTile(int gridX, int gridY, double halfWidth, double halfHeight)
    {
        GridX = gridX;
        GridY = gridY;
        Center = IsometricGridTransforms.GridCoordinateToTileCenter(gridX, gridY, halfWidth, halfHeight);
        Vertices = IsometricGridTransforms.CalculateTileVertices(gridX, gridY, halfWidth, halfHeight);
    }

    public int GridX { get; }
    public int GridY { get; }
    public RealCoordinate Center { get; }
    public IReadOnlyList<RealCoordinate> Vertices { get; }
}