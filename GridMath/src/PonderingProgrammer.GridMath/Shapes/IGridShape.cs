using System.Collections.Generic;

namespace PonderingProgrammer.GridMath.Shapes
{
    public interface IGridShape
    {
        List<GridCoordinatePair> ContainedCoordinates { get; }
        GridBoundingBox BoundingBox { get; }
        void Translate(int x, int y);
        void Rotate(Grid4Rotation rotation);
        void Flip(GridAxis axis);
    }
}