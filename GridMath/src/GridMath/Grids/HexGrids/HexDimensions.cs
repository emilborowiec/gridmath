using GridMath.Geometry;
using System;

namespace GridMath.Grids.HexGrids;

public readonly record struct HexDimensions
{
    public HexDimensions(double size, HexOrientation hexOrientation)
    {
        Size = size;
        HexOrientation = hexOrientation;
        
        SideToSideDimension = Math.Sqrt(3) * Size;
        PointToPointDimension = Size * 2;
        
        Width = HexOrientation == HexOrientation.Vertical ? PointToPointDimension : SideToSideDimension;
        Height = HexOrientation == HexOrientation.Vertical ? SideToSideDimension : PointToPointDimension;
        
        var stackSpacing = SideToSideDimension;
        var crossAxisSpacing = Size * 3 / 2;
        HorizontalSpacing = HexOrientation == HexOrientation.Vertical ? crossAxisSpacing : stackSpacing;
        VerticalSpacing = HexOrientation == HexOrientation.Vertical ? stackSpacing : crossAxisSpacing;
    }
    
    public double Size { get; }
    public HexOrientation HexOrientation { get; }
    public double SideToSideDimension { get; }
    public double PointToPointDimension { get; }
    public double Width { get; }
    public double Height { get; }
    public double HorizontalSpacing { get; }
    public double VerticalSpacing { get; }
}