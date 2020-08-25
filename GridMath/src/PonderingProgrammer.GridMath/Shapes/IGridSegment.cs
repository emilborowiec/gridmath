namespace PonderingProgrammer.GridMath.Shapes
{
    /// <summary>
    /// IGridSegment is defined with start and end points,
    /// and is a collection of all the points .
    /// </summary>
    public interface IGridSegment : IGridShape
    {
        GridCoordinatePair A { get; set; }
        GridCoordinatePair B { get; set; }
        
        int Dx { get; }
        int Dy { get; }
    }
}