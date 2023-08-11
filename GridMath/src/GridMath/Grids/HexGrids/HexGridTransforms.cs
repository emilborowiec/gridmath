using GridMath.Geometry;
using System;
using System.Drawing;

namespace GridMath.Grids.HexGrids;

public static class HexGridTransforms
{
        public static RealCoordinate[] CalculateVertices(double x, double y, HexDimensions hexDimensions)
    {
        if (hexDimensions.HexOrientation == HexOrientation.Horizontal)
        {
            var top = new RealCoordinate(x, y - hexDimensions.Size);
            var topRight = new RealCoordinate(x + (hexDimensions.SideToSideDimension / 2), y - (hexDimensions.Size / 2));
            var bottomRight = new RealCoordinate(x + (hexDimensions.SideToSideDimension / 2), y + (hexDimensions.Size / 2));
            var bottom = new RealCoordinate(x, y + hexDimensions.Size);
            var bottomLeft = new RealCoordinate(x - (hexDimensions.SideToSideDimension / 2), y + (hexDimensions.Size / 2));
            var topLeft = new RealCoordinate(x - (hexDimensions.SideToSideDimension / 2), y - (hexDimensions.Size / 2));
            return new[] { top, topRight, bottomRight, bottom, bottomLeft, topLeft };
        }
        else
        {
            var topLeft = new RealCoordinate(x - (hexDimensions.Size / 2), y - (hexDimensions.SideToSideDimension / 2));
            var topRight = new RealCoordinate(x + (hexDimensions.Size / 2), y - (hexDimensions.SideToSideDimension / 2));
            var right = new RealCoordinate(x + hexDimensions.Size, y);
            var bottomRight = new RealCoordinate(x + (hexDimensions.Size / 2), y + (hexDimensions.SideToSideDimension / 2));
            var bottomLeft = new RealCoordinate(x - (hexDimensions.Size / 2), y + (hexDimensions.SideToSideDimension / 2));
            var left = new RealCoordinate(x - hexDimensions.Size, y);
            return new[] { topLeft, topRight, right, bottomRight, bottomLeft, left };
        }
    }

    public static RealCoordinate CalculateHexCenter(double q, double r, HexDimensions hexDimensions)
    {
        if (hexDimensions.HexOrientation == HexOrientation.Horizontal)
        {
            var x = (hexDimensions.Width * q) + (hexDimensions.Width * r / 2);
            var y = r * hexDimensions.VerticalSpacing;
            return new RealCoordinate(x, y);
        }
        else
        {
            var x = q * hexDimensions.HorizontalSpacing;
            var y = (hexDimensions.Height * r) + (hexDimensions.Height * q / 2);
            return new RealCoordinate(x, y);
        }
    }

    public static HexGridCubeCoordinate RealToHexGridCubeCoordinate(double x, double y, HexDimensions hexDimensions)
    {
        if (hexDimensions.HexOrientation == HexOrientation.Horizontal)
        {
            var r = y / hexDimensions.VerticalSpacing;
            var q = (x / hexDimensions.Width) - (r / 2);
            var s = -r - q;
            return RoundCubeCoordinates(q, r, s);
        }
        else
        {
            var q = x / hexDimensions.HorizontalSpacing;
            var r = (y / hexDimensions.Height) - (q / 2);
            var s = -r - q;
            return RoundCubeCoordinates(q, r, s);
        }
    }

    public static HexGridCubeCoordinate RoundCubeCoordinates(double qFrac, double rFrac, double sFrac)
    {
        var q = Convert.ToInt32(Math.Round(qFrac));
        var r = Convert.ToInt32(Math.Round(rFrac));
        var s = Convert.ToInt32(Math.Round(sFrac));

        var qDiff = Math.Abs(q - qFrac);
        var rDiff = Math.Abs(r - rFrac);
        var sDiff = Math.Abs(s - sFrac);

        if (qDiff > rDiff && (qDiff > sDiff))
        {
            q = -r - s;
        }
        else if (rDiff > sDiff) {
            r = -q - s;
        }
        else
        {
            s = -q - r;
        }

        return HexGridCubeCoordinate.Create(q, r, s);
    }
}