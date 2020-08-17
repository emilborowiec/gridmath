using System;

namespace PonderingProgrammer.GridMath.Shapes
{
    /// <summary>
    /// GridPolygon is a rectilinear polygon with sides parallel to axes of grid coordinates.
    /// Work in progress.
    /// </summary>
    public class GridPolygon : AbstractGridShape
    {
        private readonly GridCoordinatePair[] _vertices;

        public GridPolygon(GridCoordinatePair[] vertices)
        {
            if (vertices.Length < 4 || vertices.Length % 2 != 0)
                throw new ArgumentException("GridPolygon must have even count of at least 4 vertices.");
            _vertices = vertices;
            for (var i = 0; i < vertices.Length - 1; i++)
            {
                if (!IsAxisAligned(vertices[i], vertices[i+1]))
                    throw new ArgumentException("All edges of GridPolygon must be axis-aligned.");
            }
        }
        
        public override void Translate(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        public override void Rotate(Grid4Rotation rotation)
        {
            throw new System.NotImplementedException();
        }

        public override void Flip(GridAxis axis)
        {
            throw new System.NotImplementedException();
        }

        protected override void UpdateBoundsAndContainedCoords()
        {
            throw new System.NotImplementedException();
        }

        private static bool IsAxisAligned(GridCoordinatePair v1, GridCoordinatePair v2)
        {
            return v1.X == v2.X || v1.Y == v2.Y;
        }
    }
}