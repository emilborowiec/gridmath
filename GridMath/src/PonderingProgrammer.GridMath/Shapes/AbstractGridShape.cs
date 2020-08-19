#region

using System.Collections.Generic;
using System.Linq;

#endregion

namespace PonderingProgrammer.GridMath.Shapes
{
    public abstract class AbstractGridShape : IGridShape
    {
        public IReadOnlyList<GridCoordinatePair> Coordinates => Coords.AsReadOnly();
        public GridBoundingBox BoundingBox => BBox;
        protected List<GridCoordinatePair> Coords { get; } = new List<GridCoordinatePair>();
        protected GridBoundingBox BBox { get; set; }

        public virtual bool Overlaps(GridBoundingBox boundingBox)
        {
            return BBox.Overlaps(boundingBox) && Coords.Any(boundingBox.Contains);
        }

        public abstract void Translate(int x, int y);
        public abstract void Rotate(Grid4Rotation rotation);
        public abstract void Flip(GridAxis axis);

        protected abstract void Update();
    }
}