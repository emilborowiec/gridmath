namespace GridMath.Grids.LineGrids;

public class LineGridSection
{
    public LineGridSection(int gridCoordinate, double gridSpacing)
    {
        GridCoordinate = gridCoordinate;
        Start = gridCoordinate * gridSpacing;
        End = Start + gridSpacing;
        Center = Start + (gridSpacing / 2);
    }

    public int GridCoordinate { get; }
    public double Start { get; }
    public double End { get; }
    public double Center { get; }

}