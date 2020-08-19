#region

using System.Collections.Generic;

#endregion

namespace PonderingProgrammer.GridMath.Shapes
{
    public interface IGridShape
    {
        IReadOnlyList<GridCoordinatePair> Coordinates { get; }
        GridBoundingBox BoundingBox { get; }
        bool Overlaps(GridBoundingBox boundingBox);
        void Translate(int x, int y);
        void Rotate(Grid4Rotation rotation);
        void Flip(GridAxis axis);
    }
}