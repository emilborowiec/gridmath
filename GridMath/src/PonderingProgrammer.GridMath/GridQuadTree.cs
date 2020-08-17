using System.Collections.Generic;

namespace PonderingProgrammer.GridMath
{
    public class GridQuadTree<T>
    {
        GridBoundingBox bounds; // overall bounds we are indexing.
        GridBoundingBox root;
        IDictionary<T, GridBoundingBox> table;
    }
}