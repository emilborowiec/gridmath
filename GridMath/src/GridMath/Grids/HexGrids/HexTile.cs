using GridMath.Geometry;
using System.Collections.Generic;

namespace GridMath.Grids.HexGrids;

public class HexTile
{
    public HexTile(HexGridCubeCoordinate hexGridCubeCoordinate, double size, HexOrientation hexOrientation)
    {
        GridCoordinate = hexGridCubeCoordinate;
        HexDimensions = new HexDimensions(size, hexOrientation);
        
        Center = HexGridTransforms.CalculateHexCenter(
            hexGridCubeCoordinate.Q, hexGridCubeCoordinate.R, HexDimensions);
        Vertices = HexGridTransforms.CalculateVertices(Center.X, Center.Y, HexDimensions);
    }
    
    public HexGridCubeCoordinate GridCoordinate { get; }
    public HexDimensions HexDimensions { get; }
    public IReadOnlyCollection<RealCoordinate> Vertices { get; }
    public RealCoordinate Center { get; }

}