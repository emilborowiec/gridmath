using System.Collections.Generic;

namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridPoint : AbstractGridShape
    {
        private GridCoordinatePair _position;

        public GridPoint(GridCoordinatePair position)
        {
            Position = position;
        }

        public GridCoordinatePair Position
        {
            get => _position;
            set
            {
                _position = value;
                Update();
            }
        }

        public override void Translate(int x, int y)
        {
            Position = Position.Translation(x, y);
        }

        public override void Rotate(Grid4Rotation rotation)
        {
            // Done
        }

        public override void Flip(GridAxis axis)
        {
            // Done. Best code ever.
        }

        protected override void Update()
        {
            Coords = new List<GridCoordinatePair> {Position};
            BBox = GridBoundingBox.FromMinMax(Position.X, Position.Y, Position.X, Position.Y);
        }
    }
}