using System;

namespace GridMath.Grids.HexGrids;

/// <summary>
///     Trio of coordinates used to index fields of HexGrid.
/// </summary>
public readonly record struct HexGridCubeCoordinate
{
    private static bool ValidateHexCubeCoordinate(int q, int r, int s) => q + r + s == 0;

    public static HexGridCubeCoordinate Create(int q, int r, int s)
    {
        if (!ValidateHexCubeCoordinate(q, r, s))
        {
            throw new ArgumentException("Cube coordinates for HexGrid must sum up to 0 in order to be valid");
        }

        return new HexGridCubeCoordinate(q, r, s);
    }
    
    private HexGridCubeCoordinate(int q, int r, int s)
    {
        Q = q;
        R = r;
        S = s;
    }
    
    public HexGridCubeCoordinate Translation(int x, int y, int z)
    {
        return new HexGridCubeCoordinate(Q + x, R + y, S + z);
    }

    public int Q { get; }
    public int R { get; }
    public int S { get; }
}