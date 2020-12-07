namespace GridMath.Shapes
{
    /// <summary>
    ///     Axis-aligned segment in grid coordinates.
    /// </summary>
    public interface IGridAASegment : IGridSegment
    {
        GridAxis Axis { get; }
    }
}