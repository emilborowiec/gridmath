#region

using System;
using System.Collections.Generic;

#endregion

namespace GridMath.Algorithms
{
    public static class FloodFill
    {
        public static IEnumerable<GridCoordinatePair> GetFloodFillCoordinates(
            GridCoordinatePair start,
            GridCoordinatePair[] walls,
            GridBoundingBox bounds)
        {
            var fill = new List<GridCoordinatePair>();
            if (walls == null) return fill;
            if (!bounds.Contains(start) || ArrayContains(start, walls)) return fill;
            var queue = new Queue<GridCoordinatePair>();
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

        private static bool ArrayContains(GridCoordinatePair c, GridCoordinatePair[] walls)
        {
            return Array.IndexOf(walls, c) != -1;
        }
    }
}