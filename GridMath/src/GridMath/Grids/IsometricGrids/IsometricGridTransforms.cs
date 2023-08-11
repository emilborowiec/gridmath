using GridMath.Geometry;
using System;

namespace GridMath.Grids.IsometricGrids;

public static class IsometricGridTransforms
{
    /// <summary>
    /// Calculates position of tile origin in real space.
    /// Origin of an isometric tile is the top vertex.
    /// </summary>
    public static RealCoordinate GridCoordinateToTileOrigin(int gridX, int gridY, double halfWidth, double halfHeight)
    {
        return new RealCoordinate((gridX - gridY) * halfWidth, (gridX + gridY) * halfHeight);
    }

    public static RealCoordinate GridCoordinateToTileOrigin(XYGridCoordinate gridCoordinate, double halfWidth, double halfHeight)
    {
        return GridCoordinateToTileOrigin(gridCoordinate.X, gridCoordinate.Y, halfWidth, halfHeight);
    }

    /// <summary>
    /// Calculates position of tile center in real space.
    /// </summary>
    public static RealCoordinate GridCoordinateToTileCenter(int gridX, int gridY, double halfWidth, double halfHeight)
    {
        return new RealCoordinate((gridX - gridY) * halfWidth, (gridX + gridY + 1) * halfHeight);
    }
    
    public static RealCoordinate GridCoordinateToTileCenter(XYGridCoordinate gridCoordinate, double halfWidth, double halfHeight)
    {
        return GridCoordinateToTileCenter(gridCoordinate.X, gridCoordinate.Y, halfWidth, halfHeight);
    }

    /// <summary>
    /// Calculates vertices of tile polygon in real space.
    /// Vertices start from top and go clockwise.
    /// </summary>
    public static RealCoordinate[] CalculateTileVertices(int gridX, int gridY, double halfWidth, double halfHeight)
    {
        var top = GridCoordinateToTileOrigin(gridX, gridY, halfWidth, halfHeight);
        var left = GridCoordinateToTileOrigin(gridX, gridY+1, halfWidth, halfHeight);
        var right = GridCoordinateToTileOrigin(gridX+1, gridY, halfWidth, halfHeight);
        var bottom = GridCoordinateToTileOrigin(gridX+1, gridY+1, halfWidth, halfHeight);

        return new[]
        {
            top,
            right,
            bottom,
            left
        };
    }
    
    public static RealCoordinate[] CalculateTileVertices(XYGridCoordinate gridCoordinate, double halfWidth, double halfHeight)
    {
        return CalculateTileVertices(gridCoordinate.X, gridCoordinate.Y, halfWidth, halfHeight);
    }
    
    /// <summary>
    /// Calculates grid coordinate of tile on which real coordinate.
    /// </summary>
    public static XYGridCoordinate RealToGrid(RealCoordinate realCoordinate, double halfWidth, double halfHeight)
    {
        var gridX = ((realCoordinate.X / halfWidth) + (realCoordinate.Y / halfHeight)) /2;
        var gridY = ((realCoordinate.Y / halfHeight) -(realCoordinate.X / halfWidth)) /2;

        return new XYGridCoordinate(Convert.ToInt32(Math.Floor(gridX)), Convert.ToInt32(Math.Floor(gridY)));
    }

}