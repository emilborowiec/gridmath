#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace GridMath.Shapes
{
    public class GridAASegment : IGridAASegment
    {
        public GridAASegment(GridCoordinatePair a, GridCoordinatePair b)
        {
            if (a.X == b.X && a.Y == b.Y) throw new ArgumentException("Segment must not be degenerate");
            if (a.X != b.X && a.Y != b.Y) throw new ArgumentException("Segment must be axis-aligned");
            _a = a;
            _b = b;
        }

        private GridCoordinatePair _a;
        private GridCoordinatePair _b;

        public int Dx => _b.X - _a.X;
        public int Dy => _b.Y - _a.Y;

        public GridAxis Axis => A.X == B.X ? GridAxis.Vertical : GridAxis.Horizontal;

        public IEnumerable<GridCoordinatePair> Interior
        {
            get
            {
                var min = Axis == GridAxis.Horizontal ? Math.Min(A.X, B.X) : Math.Min(A.Y, B.Y);
                var max = Axis == GridAxis.Horizontal ? Math.Max(A.X, B.X) : Math.Max(A.Y, B.Y);
                return Enumerable.Range(min, (max - min) + 1)
                                 .Select(
                                     i => Axis == GridAxis.Horizontal
                                         ? new GridCoordinatePair(i, A.Y)
                                         : new GridCoordinatePair(A.X, i));
            }
        }

        public IEnumerable<GridCoordinatePair> Edge => Interior;

        public GridBoundingBox BoundingBox =>
            GridBoundingBox.FromMinMax(Math.Min(A.X, B.X), Math.Min(A.Y, B.Y), Math.Max(A.X, B.X), Math.Max(A.Y, B.Y));

        public GridCoordinatePair A
        {
            get => _a;
            set
            {
                if (value == _b) return;
                _a = Axis == GridAxis.Horizontal
                    ? new GridCoordinatePair(value.X, _a.Y)
                    : new GridCoordinatePair(_a.X, value.Y);
            }
        }

        public GridCoordinatePair B
        {
            get => _b;
            set
            {
                if (value == _a) return;
                _b = Axis == GridAxis.Horizontal
                    ? new GridCoordinatePair(value.X, _b.Y)
                    : new GridCoordinatePair(_b.X, value.Y);
            }
        }

        public bool Contains(GridCoordinatePair position)
        {
            return Contains(position.X, position.Y);
        }

        public bool Contains(int x, int y)
        {
            return Axis == GridAxis.Horizontal
                ? y == A.Y && GetIntervalOnAxis().Contains(x)
                : x == A.X && GetIntervalOnAxis().Contains(y);
        }

        public bool Overlaps(GridBoundingBox boundingBox)
        {
            return BoundingBox.Overlaps(boundingBox);
        }

        public void Translate(int x, int y)
        {
            _a = A.Translation(x, y);
            _b = B.Translation(x, y);
        }

        public void Rotate(GridRotation rotation)
        {
            var polarB = GridPolarCoordinates.FromGridCartesian(Dx, Dy);
            polarB = polarB.Rotation(rotation.ToRadians(4));
            _b = polarB.ToGridCartesian().Translation(_a.X, _a.Y);
        }

        public void Flip(GridAxis axis)
        {
            Rotate(new GridRotation(2));
        }

        private GridInterval GetIntervalOnAxis()
        {
            return Axis == GridAxis.Horizontal
                ? new GridInterval(Math.Min(A.X, B.X), Math.Max(A.X, B.X))
                : new GridInterval(Math.Min(A.Y, B.Y), Math.Max(A.Y, B.Y));
        }
    }
}