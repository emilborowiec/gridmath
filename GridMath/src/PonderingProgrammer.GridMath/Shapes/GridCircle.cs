using System.Collections.Generic;

namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridCircle : AbstractGridShape
    {
        private GridCoordinatePair _center;
        private int _radius;

        public GridCircle(GridCoordinatePair center, int radius)
        {
            _center = center;
            _radius = radius;
            Update();
        }

        public GridCoordinatePair Center
        {
            get => _center;
            set
            {
                _center = value;
                Update();
            }
        }

        public int Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                Update();
            }
        }

        public override void Translate(int x, int y)
        {
            Center = _center.Translation(x, y);
        }

        public override void Rotate(Grid4Rotation rotation)
        {
            // Done
        }

        public override void Flip(GridAxis axis)
        {
            // Done
        }

        protected sealed override void Update()
        {
            BBox = GridBoundingBox.FromMinMax(_center.X - _radius, _center.Y - _radius, _center.X + _radius,
                _center.Y + _radius);
            Coords = new List<GridCoordinatePair>();
            for (var y = BoundingBox.MinY; y < BoundingBox.MaxYExcl; y++)
            {
                for (var x = BoundingBox.MinX; x < BoundingBox.MaxXExcl; x++)
                {
                    if (_center.EuclideanDistance(x, y) <= _radius) Coords.Add(new GridCoordinatePair(x, y));
                }
            }
        }
    }
}