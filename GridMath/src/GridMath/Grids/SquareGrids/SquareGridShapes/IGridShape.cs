#region

using System.Collections.Generic;

#endregion

namespace GridMath.Grids.SquareGrids.SquareGridShapes
{
    /// <summary>
    ///     IGridShape is a collection of grid coordinates which belong to shape's interior.
    /// </summary>
    /// <remarks>
    ///     In a regular planar geometry, shapes are defined by sets of vertices or edges,
    ///     and rules which decide how they divide space into interior and exterior.
    ///     The number of coordinates on the interior or on the edge of the shape is usually unlimited.
    ///     In low-resolution grid plane geometry, it is sometimes more efficient to treat shapes as
    ///     collections of coordinates. It is possible because the space is discrete.
    /// </remarks>
    public interface IGridShape
    {
        IEnumerable<XYGridCoordinate> Interior { get; }
        IEnumerable<XYGridCoordinate> Edge { get; }
        GridBoundingBox BoundingBox { get; }
        bool Contains(XYGridCoordinate position);
        bool Contains(int x, int y);
        bool Overlaps(GridBoundingBox boundingBox);
        void Translate(int x, int y);
        void Rotate(GridRotation rotation);
        void Flip(OrthogonalGridAxis axis);
    }
}