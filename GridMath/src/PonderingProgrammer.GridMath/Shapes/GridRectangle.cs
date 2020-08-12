using System.Collections.Generic;

namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridRectangle : AbstractGridShape
    {
        public GridRectangle(GridBoundingBox boundingBox)
        {
            BoundingBox = boundingBox;
        }

        public GridBoundingBox BoundingBox
        {
            get => _boundingBox;
            set
            {
                _boundingBox = value;
                UpdateBoundsAndContainedCoords();
            }
        }

        public override void Translate(int x, int y)
        {
            BoundingBox = BoundingBox.Translation(x, y);
        }

        public override void Rotate(Grid4Rotation rotation)
        {
            if (rotation == Grid4Rotation.NinetyCcw || rotation == Grid4Rotation.NinetyCw)
            {
                BoundingBox = GridBoundingBox.FromSize(BoundingBox.MinX, BoundingBox.MinY, BoundingBox.Height,BoundingBox.Width);
            }
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