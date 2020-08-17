using System;
using System.Collections.Generic;

namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridRectangle : AbstractGridShape
    {
        public GridRectangle(GridBoundingBox boundingBox)
        {
            BoundingBox = boundingBox;
        }

        public new GridBoundingBox BoundingBox
        {
            get => _boundingBox;
            set
            {
                _boundingBox = value;
                UpdateBoundsAndContainedCoords();
            }
        }

        public override bool Overlaps(GridBoundingBox boundingBox)
        {
            return _boundingBox.Overlaps(boundingBox);
        }

        public override void Translate(int x, int y)
        {
            BoundingBox = BoundingBox.Translation(x, y);
        }

        public override void Rotate(Grid4Rotation rotation)
        {
            var topRight = GridPolarCoordinates.FromGridCartesian(BoundingBox.TopRight.Translation(-BoundingBox.Center.X, -BoundingBox.Center.Y));
            var bottomLeft = GridPolarCoordinates.FromGridCartesian(BoundingBox.BottomLeft.Translation(-BoundingBox.Center.X, -BoundingBox.Center.Y));
            var rotatedTopRight = topRight.Rotation(Directions.Grid4RotationToAngle(rotation)).ToGridCartesian();
            var rotatedBottomLeft = bottomLeft.Rotation(Directions.Grid4RotationToAngle(rotation)).ToGridCartesian();
            BoundingBox = rotation switch
            {
                Grid4Rotation.Ccw90 => GridBoundingBox.FromMinMax(rotatedTopRight.Translation(BoundingBox.Center.X, BoundingBox.Center.Y), 
                    rotatedBottomLeft.Translation(BoundingBox.Center.X, BoundingBox.Center.Y)),
                Grid4Rotation.Cw90 => GridBoundingBox.FromMinMax(rotatedBottomLeft.Translation(BoundingBox.Center.X, BoundingBox.Center.Y), 
                    rotatedTopRight.Translation(BoundingBox.Center.X, BoundingBox.Center.Y)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public override void Flip(GridAxis axis)
        {
            // Done
        }

        protected override void UpdateBoundsAndContainedCoords()
        {
            _containedCoordinates = new List<GridCoordinatePair>();
            for (var y = BoundingBox.MinY; y < BoundingBox.MaxYExcl; y++)
            {
                for (var x = BoundingBox.MinX; x < BoundingBox.MaxXExcl; x++)
                {
                    _containedCoordinates.Add(new GridCoordinatePair(x, y));
                }
            }
        }
    }
}