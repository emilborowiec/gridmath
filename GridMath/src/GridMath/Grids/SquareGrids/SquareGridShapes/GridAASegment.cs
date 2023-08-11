#region

using GridMath.Grids.LineGrids;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    public class GridAASegment : IGridAASegment
    {
        public GridAASegment(XYGridCoordinate a, XYGridCoordinate b)
        {
            if (a.X == b.X && a.Y == b.Y) throw new ArgumentException("Segment must not be degenerate");
            if (a.X != b.X && a.Y != b.Y) throw new ArgumentException("Segment must be axis-aligned");
            _a = a;
            _b = b;
        }

        private XYGridCoordinate _a;
        private XYGridCoordinate _b;

        public int Dx => _b.X - _a.X;
        public int Dy => _b.Y - _a.Y;

        public OrthogonalGridAxis Axis => A.X == B.X ? OrthogonalGridAxis.Vertical : OrthogonalGridAxis.Horizontal;

        public IEnumerable<XYGridCoordinate> Interior
        {
            get
            {
                var min = Axis == OrthogonalGridAxis.Horizontal ? Math.Min(A.X, B.X) : Math.Min(A.Y, B.Y);
                var max = Axis == OrthogonalGridAxis.Horizontal ? Math.Max(A.X, B.X) : Math.Max(A.Y, B.Y);
                return Enumerable.Range(min, (max - min) + 1)
                                 .Select(
                                     i => Axis == OrthogonalGridAxis.Horizontal
                                         ? new XYGridCoordinate(i, A.Y)
                                         : new XYGridCoordinate(A.X, i));
            }
        }

        public IEnumerable<XYGridCoordinate> Edge => Interior;

        public GridBoundingBox BoundingBox =>
            GridBoundingBox.FromMinMax(Math.Min(A.X, B.X), Math.Min(A.Y, B.Y), Math.Max(A.X, B.X), Math.Max(A.Y, B.Y));

        public XYGridCoordinate A
        {
            get => _a;
            set
            {
                if (value == _b) return;
                _a = Axis == OrthogonalGridAxis.Horizontal
                    ? new XYGridCoordinate(value.X, _a.Y)
                    : new XYGridCoordinate(_a.X, value.Y);
            }
        }

        public XYGridCoordinate B
        {
            get => _b;
            set
            {
                if (value == _a) return;
                _b = Axis == OrthogonalGridAxis.Horizontal
                    ? new XYGridCoordinate(value.X, _b.Y)
                    : new XYGridCoordinate(_b.X, value.Y);
            }
        }

        public bool Contains(XYGridCoordinate position)
        {
            return Contains(position.X, position.Y);
        }

        public bool Contains(int x, int y)
        {
            return Axis == OrthogonalGridAxis.Horizontal
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

        public void Flip(OrthogonalGridAxis axis)
        {
            Rotate(new GridRotation(2));
        }

        private IntegerInterval GetIntervalOnAxis()
        {
            return Axis == OrthogonalGridAxis.Horizontal
                ? new IntegerInterval(Math.Min(A.X, B.X), Math.Max(A.X, B.X))
                : new IntegerInterval(Math.Min(A.Y, B.Y), Math.Max(A.Y, B.Y));
        }
    }
}