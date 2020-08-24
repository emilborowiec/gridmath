namespace PonderingProgrammer.GridMath.Shapes
{
    public interface IGridMultiPoint : IGridShape
    {
        bool AddPosition(GridCoordinatePair position);
        bool RemovePosition(GridCoordinatePair position);
    }
}