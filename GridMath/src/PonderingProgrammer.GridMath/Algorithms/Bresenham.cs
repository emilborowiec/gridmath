#region

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace PonderingProgrammer.GridMath.Algorithms
{
    public static class Bresenham
    {
        public static IEnumerable<GridCoordinatePair> PlotLine(int x1, int y1, int x2, int y2)
        {
            var dx = x2 - x1;
            var dy = y2 - y1;
            if (dx == 0 && dy == 0) return new[] {new GridCoordinatePair(x1, y1)};
            if (dy == 0)
            {
                return Enumerable.Range(Math.Min(x1, x2), Math.Abs(x2 - x1) + 1)
                                 .Select(x => new GridCoordinatePair(x, y1));
            }

            if (dx == 0)
            {
                return Enumerable.Range(Math.Min(y1, y2), Math.Abs(y2 - y1) + 1)
                                 .Select(y => new GridCoordinatePair(x1, y));
            }

            var octant = Octants.GetOctant(dx, dy);
            // For octants where dx >= dy use LowLine. LowLine algorithm works towards increasing x (to right)
            // For octants where dx < dy use HighLine. HighLine algorithm works towards increasing y (downwards)
            return octant switch
            {
                Octant.Zero => LowLine(x1, y1, x2, y2),
                Octant.One => HighLine(x1, y1, x2, y2),
                Octant.Two => HighLine(x1, y1, x2, y2),
                Octant.Three => LowLine(x2, y2, x1, y1),
                Octant.Four => LowLine(x2, y2, x1, y1),
                Octant.Five => HighLine(x2, y2, x1, y1),
                Octant.Six => HighLine(x2, y2, x1, y1),
                Octant.Seven => LowLine(x1, y1, x2, y2),
                _ => Enumerable.Empty<GridCoordinatePair>(),
            };
        }

        public static IEnumerable<GridCoordinatePair> PlotCircle(int xc, int yc, int r)
        {
            if (r == 0) return new[] {new GridCoordinatePair(xc, yc)};
            var x = 0;
            var y = r;
            var d = 3 - (2 * r);
            var plot = new List<GridCoordinatePair>();
            plot.AddRange(PlotOnAllOctants(xc, yc, x, y));
            while (y >= x)
            {
                // for each pixel we will 
                // draw all eight pixels 
                x++;
                // check for decision parameter 
                // and correspondingly  
                // update d, x, y 
                if (d > 0)
                {
                    y--;
                    d = d + (4 * (x - y)) + 10;
                }
                else
                {
                    d = d + (4 * x) + 6;
                }

                plot.AddRange(PlotOnAllOctants(xc, yc, x, y));
            }

            return plot;
        }

        private static IEnumerable<GridCoordinatePair> LowLine(int x1, int y1, int x2, int y2)
        {
            var dx = x2 - x1;
            var dy = y2 - y1;
            var sign = 1;
            if (dy < 0)
            {
                sign = -1;
                dy = -dy;
            }

            double decision = (2 * dy) - dx;
            var y = y1;

            for (var x = x1; x <= x2; x++)
            {
                yield return new GridCoordinatePair(x, y);
                if (decision >= 0)
                {
                    y += sign;
                    decision -= 2 * dx;
                }

                decision += 2 * dy;
            }
        }

        private static IEnumerable<GridCoordinatePair> HighLine(int x1, int y1, int x2, int y2)
        {
            var dx = x2 - x1;
            var dy = y2 - y1;
            var sign = 1;
            if (dx < 0)
            {
                sign = -1;
                dx = -dx;
            }

            double decision = (2 * dx) - dy;
            var x = x1;

            for (var y = y1; y <= y2; y++)
            {
                yield return new GridCoordinatePair(x, y);
                if (decision >= 0)
                {
                    x += sign;
                    decision -= 2 * dy;
                }

                decision += 2 * dx;
            }
        }

        private static IEnumerable<GridCoordinatePair> PlotOnAllOctants(int xc, int yc, int x, int y)
        {
            yield return new GridCoordinatePair(xc + x, yc + y);
            yield return new GridCoordinatePair(xc - x, yc + y);
            yield return new GridCoordinatePair(xc + x, yc - y);
            yield return new GridCoordinatePair(xc - x, yc - y);
            yield return new GridCoordinatePair(xc + y, yc + x);
            yield return new GridCoordinatePair(xc - y, yc + x);
            yield return new GridCoordinatePair(xc + y, yc - x);
            yield return new GridCoordinatePair(xc - y, yc - x);
        }
    }
}