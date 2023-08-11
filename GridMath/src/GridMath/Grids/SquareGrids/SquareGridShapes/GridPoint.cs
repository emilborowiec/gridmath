#region

using System.Collections.Generic;

#endregion

namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    public class GridPoint : IGridPoint
    {
        public GridPoint(int x, int y) : this(new XYGridCoordinate(x, y))
        {
        }
        
        public GridPoint(XYGridCoordinate position)
        {
            Position = position;
        }

        private XYGridCoordinate _position;

        public IEnumerable<XYGridCoordinate> Interior => new[] {_position};
        public IEnumerable<XYGridCoordinate> Edge => new[] {_position};

        public GridBoundingBox BoundingBox =>
            GridBoundingBox.FromMinMax(_position.X, _position.Y, _position.X, _position.Y);

        public XYGridCoordinate Position
        {
            get => _position;
            set => _position = value;
        }

        public int X => Position.X;
        public int Y => Position.Y;

        public bool Contains(XYGridCoordinate position)
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

        public void Flip(OrthogonalGridAxis axis)
        {
            // Done. Best code ever.
        }
    }
}