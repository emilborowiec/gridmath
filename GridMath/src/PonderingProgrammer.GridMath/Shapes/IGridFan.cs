namespace PonderingProgrammer.GridMath.Shapes
{
    public interface IGridFan : IGridShape
    {
        int Radius { get; set; }
        GridCoordinatePair Origin { get; set; }
        Grid8Direction Direction { get; set; }
    }
}