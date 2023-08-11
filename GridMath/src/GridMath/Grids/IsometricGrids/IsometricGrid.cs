namespace GridMath.Grids.IsometricGrids;

public class IsometricGrid
{
    public IsometricGrid(double tileWidth, double tileHeight)
    {
        TileWidth = tileWidth;
        TileHeight = tileHeight;
    }

    public double TileWidth { get; }
    public double TileHeight { get; }

}