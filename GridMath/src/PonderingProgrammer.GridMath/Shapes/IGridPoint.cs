namespace PonderingProgrammer.GridMath.Shapes
{
    public interface IGridPoint : IGridShape
    {
        GridCoordinatePair Position { get; set; }
    }
}