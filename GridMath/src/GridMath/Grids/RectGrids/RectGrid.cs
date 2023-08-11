using GridMath.Geometry;
using GridMath.Grids.LineGrids;

namespace GridMath.Grids.RectGrids;

/// <summary>
///     Grid of rectangles. A regular, rectlinear grid that is indexed with XY coordinates.
/// </summary>
/// <remarks>
///     Mapping from real space to grid space is chosen such that N on grid corresponds to R[N,N+1).
///     This mapping can be implemented by getting Floor of the real value.
///     Note that simple cast of double to int only truncates floating part and gives correct results only for positive
///     numbers.
/// </remarks>
public class RectGrid
{
    public RectGrid(double tileWidth, double tileHeight)
    {
        TileWidth = tileWidth;
        TileHeight = tileHeight;
    }

    public double TileWidth { get; }
    public double TileHeight { get; }
}