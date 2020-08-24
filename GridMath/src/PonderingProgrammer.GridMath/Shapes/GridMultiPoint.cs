using System;
using System.Collections.Generic;
using System.Linq;

namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridMultiPoint : IGridMultiPoint
    {
        public GridMultiPoint(IEnumerable<GridCoordinatePair> positions)
        {
            if (positions == null) throw new ArgumentNullException(nameof(positions));
            _positions = new List<GridCoordinatePair>(positions);
            if (_positions.Count == 0) throw new ArgumentException("MultiPoint cannot be empty");
        }

        private List<GridCoordinatePair> _positions;

        public IEnumerable<GridCoordinatePair> Interior => _positions.AsReadOnly();
        public IEnumerable<GridCoordinatePair> Edge => _positions.AsReadOnly();
        
        public GridBoundingBox BoundingBox
        {
            get
            {
                var minX = int.MaxValue;
                var minY = int.MaxValue;
                var maxX = int.MinValue;
                var maxY = int.MinValue;
                foreach (var c in _positions)
                {
                    if (c.X > maxX) maxX = c.X;
                    if (c.Y > maxY) maxY = c.Y;
                    if (c.X < minX) minX = c.X;
                    if (c.Y < minY) minY = c.Y;
                }

                return GridBoundingBox.FromMinMax(minX, minY, maxX, maxY);
            }
        }

        public bool AddPosition(GridCoordinatePair position)
        {
            if (_positions.Contains(position)) return false;
            _positions.Add(position);
            return true;
        }

        public bool RemovePosition(GridCoordinatePair position)
        {
            return _positions.Count != 1 && _positions.Remove(position);
        }

        public bool Contains(GridCoordinatePair position)
        {
            return _positions.Contains(position);
        }

        public bool Contains(int x, int y)
        {
            return Contains(new GridCoordinatePair(x, y));
        }

        public bool Overlaps(GridBoundingBox boundingBox)
        {
            return _positions.Any(boundingBox.Contains);
        }

        public void Translate(int x, int y)
        {
            for (var i = 0; i < _positions.Count; i++) _positions[i] = _positions[i].Translation(x, y);
        }

        public void Rotate(Grid4Rotation rotation)
        {
            throw new NotImplementedException();
        }

        public void Flip(GridAxis axis)
        {
            throw new NotImplementedException();
        }
    }
}