#region

using GridMath.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    public class GridSegment : IGridSegment
    {
        public GridSegment(XYGridCoordinate a, XYGridCoordinate b)
        {
            if (a.X == b.X && a.Y == b.Y) throw new ArgumentException("Segment must not be degenerate");
            _a = a;
            _b = b;
        }

        private XYGridCoordinate _a;
        private XYGridCoordinate _b;

        public int Dx => _b.X - _a.X;
        public int Dy => _b.Y - _a.Y;

        public IEnumerable<XYGridCoordinate> Interior => Bresenham.PlotLine(A.X, A.Y, B.X, B.Y);

        public IEnumerable<XYGridCoordinate> Edge => Interior;

        public GridBoundingBox BoundingBox =>
            GridBoundingBox.FromMinMax(Math.Min(A.X, B.X), Math.Min(A.Y, B.Y), Math.Max(A.X, B.X), Math.Max(A.Y, B.Y));

        public XYGridCoordinate A
        {
            get => _a;
            set
            {
                if (value == _b) return;
                _a = value;
            }
        }

        public XYGridCoordinate B
        {
            get => _b;
            set
            {
                if (value == _a) return;
                _b = value;
            }
        }

        public bool Contains(XYGridCoordinate position)
        {
            return Interior.Contains(position);
        }

        public bool Contains(int x, int y)
        {
            return Interior.Contains(new XYGridCoordinate(x, y));
        }

        public bool Overlaps(GridBoundingBox boundingBox)
        {
            return Interior.Any(boundingBox.Contains);
        }

        public void Translate(int x, int y)
        {
            _a = A.Translation(x, y);
            _b = B.Translation(x, y);
        }

        public void Rotate(GridRotation rotation)
        {
            var ticks = rotation.Ticks % 12;
            var polarB = GridPolarCoordinates.FromGridCartesian(Dx, Dy);
            polarB = polarB.Rotation(rotation.ToRadians(12));
            _b = polarB.ToGridCartesian().Translation(_a.X, _a.Y);
        }

        public void Flip(OrthogonalGridAxis axis)
        {
            Rotate(new GridRotation(6));
        }
    }
}