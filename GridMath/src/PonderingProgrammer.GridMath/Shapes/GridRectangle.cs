using System;
using System.Collections.Generic;

namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridRectangle : AbstractGridShape
    {
        public GridRectangle(GridBoundingBox rectangle)
        {
            Rectangle = rectangle;
        }

        public GridBoundingBox Rectangle
        {
            get => BBox;
            set
            {
                BBox = value;
                Update();
            }
        }

        public override bool Overlaps(GridBoundingBox boundingBox)
        {
            return Rectangle.Overlaps(boundingBox);
        }

        public override void Translate(int x, int y)
        {
            Rectangle = Rectangle.Translation(x, y);
        }

        public override void Rotate(Grid4Rotation rotation)
        {
            var topRight = GridPolarCoordinates.FromGridCartesian(Rectangle.TopRight.Translation(-Rectangle.Center.X, -Rectangle.Center.Y));
            var bottomLeft = GridPolarCoordinates.FromGridCartesian(Rectangle.BottomLeft.Translation(-Rectangle.Center.X, -Rectangle.Center.Y));
            var rotatedTopRight = topRight.Rotation(Directions.Grid4RotationToAngle(rotation)).ToGridCartesian();
            var rotatedBottomLeft = bottomLeft.Rotation(Directions.Grid4RotationToAngle(rotation)).ToGridCartesian();
            Rectangle = rotation switch
            {
                Grid4Rotation.Ccw90 => GridBoundingBox.FromMinMax(rotatedTopRight.Translation(Rectangle.Center.X, Rectangle.Center.Y), 
                    rotatedBottomLeft.Translation(Rectangle.Center.X, Rectangle.Center.Y)),
                Grid4Rotation.Cw90 => GridBoundingBox.FromMinMax(rotatedBottomLeft.Translation(Rectangle.Center.X, Rectangle.Center.Y), 
                    rotatedTopRight.Translation(Rectangle.Center.X, Rectangle.Center.Y)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public override void Flip(GridAxis axis)
        {
            // Done
        }

        protected override void Update()
        {
            Coords = new List<GridCoordinatePair>();
            for (var y = Rectangle.MinY; y < Rectangle.MaxYExcl; y++)
            {
                for (var x = Rectangle.MinX; x < Rectangle.MaxXExcl; x++)
                {
                    Coords.Add(new GridCoordinatePair(x, y));
                }
            }
        }
    }
}