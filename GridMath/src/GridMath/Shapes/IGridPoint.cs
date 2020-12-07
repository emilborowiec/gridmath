namespace GridMath.Shapes
{
    public interface IGridPoint : IGridShape
    {
        GridCoordinatePair Position { get; set; }
    }
}