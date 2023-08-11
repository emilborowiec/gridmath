using GridMath.Grids.RectGrids;

namespace GridMath.Grids.SquareGrids;

public class SquareGrid : RectGrid
{
    public SquareGrid(double tileSize) : base(tileSize, tileSize)
    {
    }

    public double TileSize => TileWidth;
}