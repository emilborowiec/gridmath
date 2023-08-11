using System;

namespace GridMath.Grids.SquareGrids.SquareGridShapes;

public static class SquareGridUtils
{
    public static int ManhattanDistance(XYGridCoordinate c1, XYGridCoordinate c2)
    {
        return ManhattanDistance(c1.X, c1.Y, c2.X, c2.Y);
    }

    public static int ManhattanDistance(int x1, int y1, int x2, int y2)
    {
        return Math.Abs(x2 - x1) + Math.Abs(y2 - y1);
    }

    public static int ChebyshevDistance(XYGridCoordinate c1, XYGridCoordinate c2)
    {
        return ChebyshevDistance(c1.X, c1.Y, c2.X, c2.Y);
    }

    public static int ChebyshevDistance(int x1, int y1, int x2, int y2)
    {
        return Math.Max(Math.Abs(x2 - x1), Math.Abs(y2 - y1));
    }

    public static double EuclideanDistance(int x1, int y1, int x2, int y2)
    {
        return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
    }

    public static double Sed(int x1, int y1, int x2, int y2)
    {
        return Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2);
    }
}