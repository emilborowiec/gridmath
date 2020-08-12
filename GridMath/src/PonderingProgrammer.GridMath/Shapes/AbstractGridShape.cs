using System.Collections.Generic;

namespace PonderingProgrammer.GridMath.Shapes
{
    public abstract class AbstractGridShape : IGridShape
    {
        protected GridBoundingBox _boundingBox;
        protected List<GridCoordinatePair> _containedCoordinates;

        public GridBoundingBox BoundingBox => _boundingBox;
        public List<GridCoordinatePair> ContainedCoordinates => _containedCoordinates;

        public abstract void Translate(int x, int y);
        public abstract void Rotate(Grid4Rotation rotation);
        public abstract void Flip(GridAxis axis);
        
        protected abstract void UpdateBoundsAndContainedCoords();
    }
}