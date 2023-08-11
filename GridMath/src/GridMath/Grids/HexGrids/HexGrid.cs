namespace GridMath.Grids.HexGrids;

public class HexGrid
{
    public HexGrid(HexOrientation hexOrientation, double hexSize)
    {
        HexDimensions = new HexDimensions(hexSize, hexOrientation);
    }

    public HexDimensions HexDimensions { get; }
}