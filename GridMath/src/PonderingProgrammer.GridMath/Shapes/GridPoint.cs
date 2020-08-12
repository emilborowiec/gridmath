using System.Collections.Generic;

namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridPoint : AbstractGridShape
    {
        private GridCoordinatePair _coordinates;

        public GridPoint(GridCoordinatePair coordinates)
        {
            Coordinates = coordinates;
        }

        public GridCoordinatePair Coordinates
        {
            get => _coordinates;
            set
            {
                _coordinates = value;
                UpdateBoundsAndContainedCoords();
            }
        }

        protected override void UpdateBoundsAndContainedCoords()
        {
            _boundingBox = GridBoundingBox.FromMinMax(Coordinates.X, Coordinates.Y, Coordinates.X, Coordinates.Y);
            _containedCoordinates = new List<GridCoordinatePair> {Coordinates};
        }

        public override void Translate(int x, int y)
        {
            Coordinates = Coordinates.Translation(x, y);
        }

        public override void Rotate(Grid4Rotation rotation)
        {
            // Done
        }

        public override void Flip(GridAxis axis)
        {
            // Done. Best code ever.
        }
    }
}