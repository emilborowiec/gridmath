using System.Collections.Generic;
using System.Linq;

namespace PonderingProgrammer.GridMath.Shapes
{
    public abstract class AbstractGridShape : IGridShape
    {
        protected GridBoundingBox _boundingBox;
        protected List<GridCoordinatePair> _containedCoordinates;

        public GridBoundingBox BoundingBox => _boundingBox;
        public List<GridCoordinatePair> ContainedCoordinates => _containedCoordinates;

        public virtual bool Overlaps(GridBoundingBox boundingBox)
        {
            return _containedCoordinates.Any(coords => boundingBox.Contains(coords));
        }

        public abstract void Translate(int x, int y);
        public abstract void Rotate(Grid4Rotation rotation);
        public abstract void Flip(GridAxis axis);
        
        protected abstract void UpdateBoundsAndContainedCoords();
    }
}