namespace PonderingProgrammer.GridMath.Shapes
{
    public class GridPoint : AbstractGridShape
    {
        public GridPoint(GridCoordinatePair position)
        {
            Position = position;
        }

        private GridCoordinatePair _position;

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
            Coords.Clear();
            Coords.Add(Position);
            BBox = GridBoundingBox.FromMinMax(Position.X, Position.Y, Position.X, Position.Y);
        }
    }
}