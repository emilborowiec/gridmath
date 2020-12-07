namespace GridMath.Shapes
{
    public interface IGridCircle : IGridShape
    {
        int Radius { get; set; }
        GridCoordinatePair Center { get; set; }
    }
}