#region

using System.Collections.Generic;

#endregion

namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridPoint : IGridPoint
    {
        public GridPoint(GridCoordinatePair position)
        {
            Position = position;
        }

        private GridCoordinatePair _position;

        public IEnumerable<GridCoordinatePair> Interior => new[] {_position};
        public IEnumerable<GridCoordinatePair> Edge => new[] {_position};

        public GridBoundingBox BoundingBox =>
            GridBoundingBox.FromMinMax(_position.X, _position.Y, _position.X, _position.Y);

        public GridCoordinatePair Position
        {
            get => _position;
            set => _position = value;
        }

        public bool Contains(GridCoordinatePair position)
        {
            return _position == position;
        }

        public bool Contains(int x, int y)
        {
            return _position.X == x && _position.Y == y;
        }

        public bool Overlaps(GridBoundingBox boundingBox)
        {
            return boundingBox.Contains(_position);
        }

        public void Translate(int x, int y)
        {
            _position = _position.Translation(x, y);
        }

        public void Rotate(GridRotation rotation)
        {
            // Done
        }

        public void Flip(GridAxis axis)
        {
            // Done. Best code ever.
        }
    }
}