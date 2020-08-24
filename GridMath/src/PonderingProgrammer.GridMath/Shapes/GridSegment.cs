using System;
using System.Collections.Generic;
using System.Linq;
using PonderingProgrammer.GridMath.Algorithms;

namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridSegment : IGridSegment
    {
        public GridSegment(GridCoordinatePair a, GridCoordinatePair b)
        {
            if (a.X == b.X && a.Y == b.Y) throw new ArgumentException("Segment must not be degenerate");
            _a = a;
            _b = b;
        }

        private GridCoordinatePair _a;
        private GridCoordinatePair _b;

        public GridCoordinatePair A
        {
            get => _a;
            set
            {
                if (value == _b) return;
                _a = value;
            }
        }

        public GridCoordinatePair B
        {
            get => _b;
            set
            {
                if (value == _a) return;
                _b = value;
            }
        }

        public IEnumerable<GridCoordinatePair> Interior => Bresenham.PlotLine(A.X, A.Y, B.X, B.Y);

        public IEnumerable<GridCoordinatePair> Edge => Interior;
        
        public GridBoundingBox BoundingBox => GridBoundingBox.FromMinMax(Math.Min(A.X, B.X), Math.Min(A.Y, B.Y), Math.Max(A.X, B.X), Math.Max(A.Y, B.Y));
        
        public bool Contains(GridCoordinatePair position)
        {
            return Interior.Contains(position);
        }

        public bool Contains(int x, int y)
        {
            return Interior.Contains(new GridCoordinatePair(x, y));
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

        public void Rotate(Grid4Rotation rotation)
        {
            throw new System.NotImplementedException();
        }

        public void Flip(GridAxis axis)
        {
            switch (axis)
            {
                case GridAxis.Horizontal:
                    _a = new GridCoordinatePair(_a.X, _b.Y);
                    _b = new GridCoordinatePair(_b.X, _a.Y);
                    break;
                case GridAxis.Vertical:
                    _a = new GridCoordinatePair(_b.X, _a.Y);
                    _b = new GridCoordinatePair(_a.X, _b.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
            }
        }
    }
}