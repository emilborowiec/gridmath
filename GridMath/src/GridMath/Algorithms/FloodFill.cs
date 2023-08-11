#region

using GridMath.Grids;
using System;
using System.Collections.Generic;

#endregion

namespace GridMath.Algorithms
{
    public static class FloodFill
    {
        public static IEnumerable<XYGridCoordinate> GetFloodFillCoordinates(
            XYGridCoordinate start,
            XYGridCoordinate[] walls,
            GridBoundingBox bounds)
        {
            var fill = new List<XYGridCoordinate>();
            if (walls == null) return fill;
            if (!bounds.Contains(start) || ArrayContains(start, walls)) return fill;
            var queue = new Queue<XYGridCoordinate>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var c = queue.Dequeue();
                if (fill.Contains(c) || ArrayContains(c, walls) || !bounds.Contains(c)) continue;

                fill.Add(c);

                queue.Enqueue(c.Translation(0, -1));
                queue.Enqueue(c.Translation(0, 1));
                queue.Enqueue(c.Translation(-1, 0));
                queue.Enqueue(c.Translation(1, 0));
            }

            return fill;
        }

        private static bool ArrayContains(XYGridCoordinate c, XYGridCoordinate[] walls)
        {
            return Array.IndexOf(walls, c) != -1;
        }
    }
}