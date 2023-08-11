namespace GridMath.Grids.HexGrids;

public static class HexGridGeometry
{
    public static HexGridCubeCoordinate QVector { get; } = HexGridCubeCoordinate.Create(0, 1, -1);
    public static HexGridCubeCoordinate RVector { get; } = HexGridCubeCoordinate.Create(1, 0, -1);
    public static HexGridCubeCoordinate SVector { get; } = HexGridCubeCoordinate.Create(-1, 1, 0);

    public static HexGridCubeCoordinate RotateVector60Deg(HexGridCubeCoordinate v)
    {
        return HexGridCubeCoordinate.Create(-v.R, -v.S, -v.Q);
    }
}