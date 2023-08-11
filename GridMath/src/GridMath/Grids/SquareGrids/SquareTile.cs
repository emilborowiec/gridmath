using GridMath.Grids.RectGrids;

namespace GridMath.Grids.SquareGrids;

public class SquareTile : RectTile
{
    public SquareTile(int gridX, int gridY, double size) : base(gridX, gridY, size, size)
    {
    }
}