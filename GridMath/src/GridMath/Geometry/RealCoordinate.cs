namespace GridMath.Geometry;

/// <summary>
/// Stores coordinates on a Cartesian plane.
/// </summary>
public readonly record struct RealCoordinate(double X, double Y)
{
    public void Deconstruct(out double x, out double y)
    {
        x = X;
        y = Y;
    }
}