using System.Collections.Generic;
using System.Linq;
using PonderingProgrammer.GridMath.Shapes;

namespace PonderingProgrammer.GridMath.Samples
{
    public static class DemoShapesFactory
    {
        public static IEnumerable<IGridShape> GetShapes(Demo demo)
        {
            return demo switch
            {
                Demo.PointsRects => CreatePoint(1, 1).Concat(CreateRect(3, 1)),
                Demo.Circles => CreateCircles(40, 30),
                Demo.Segments => CreateSegments(40, 30).Concat(CreateAlignedSegments(1, 30)),
                Demo.Fans => Create4WayFans(3, 3, 1).Concat(CreateDiagonalFans(6, 3, 1)),
                _ => Enumerable.Empty<IGridShape>(),
            };
        }

        private static IEnumerable<IGridShape> Create4WayFans(int x, int y, int radius)
        {
            var center = new GridCoordinatePair(x, y);
            var up = new GridFan(center.Translation(0, -1), radius, Grid8Direction.Top);
            var down = new GridFan(center.Translation(0, 1), radius, Grid8Direction.Bottom);
            var left = new GridFan(center.Translation(-1, 0), radius, Grid8Direction.Left);
            var right = new GridFan(center.Translation(1, 0), radius, Grid8Direction.Right);
            return new[] {up, down, left, right};
        }

        private static IEnumerable<IGridShape> CreateDiagonalFans(int x, int y, int radius)
        {
            var center = new GridCoordinatePair(x, y);
            var upLeft = new GridFan(center.Translation(-1, -1), radius, Grid8Direction.TopLeft);
            var upRight = new GridFan(center.Translation(1, -1), radius, Grid8Direction.TopRight);
            var downLeft = new GridFan(center.Translation(-1, 1), radius, Grid8Direction.BottomLeft);
            var downRight = new GridFan(center.Translation(1, 1), radius, Grid8Direction.BottomRight);
            return new[] {upLeft, downRight, downLeft, upRight};
        }

        private static IEnumerable<IGridShape> CreateCircles(int x, int y)
        {
            var center = new GridCoordinatePair(x, y);
            return Enumerable.Range(0, 5).Select(r => new GridCircle(center, r * 5)).ToArray();
        }

        private static IEnumerable<IGridShape> CreateSegments(int x, int y)
        {
            var center = new GridCoordinatePair(x, y);
            var ends = new[]
            {
                new GridCoordinatePair(10, 0), new GridCoordinatePair(10, -5), new GridCoordinatePair(10, -10),
                new GridCoordinatePair(5, -10), new GridCoordinatePair(0, -10), new GridCoordinatePair(-5, -10),
                new GridCoordinatePair(-10, -10), new GridCoordinatePair(-10, -5), new GridCoordinatePair(-10, 0),
                new GridCoordinatePair(-10, 5), new GridCoordinatePair(-10, 10), new GridCoordinatePair(-5, 10),
                new GridCoordinatePair(0, 10), new GridCoordinatePair(5, 10), new GridCoordinatePair(10, 10),
                new GridCoordinatePair(10, 5),
            };
            return ends.Select(end => new GridSegment(center, end.Translation(center.X, center.Y))).ToArray();
        }

        private static IEnumerable<IGridShape> CreateAlignedSegments(int x, int y)
        {
            return new[]
            {
                new GridAASegment(new GridCoordinatePair(x, y), new GridCoordinatePair(x, y + 10)),
                new GridAASegment(new GridCoordinatePair(x, y + 5), new GridCoordinatePair(x + 10, y + 5)),
            };
        }

        private static IEnumerable<IGridShape> CreateRect(int x, int y)
        {
            return new[] {new GridRectangle(GridBoundingBox.FromSize(x, y, 15, 10))};
        }

        private static IEnumerable<IGridShape> CreatePoint(int x, int y)
        {
            return new[] {new GridPoint(new GridCoordinatePair(x, y))};
        }

        public enum Demo
        {
            PointsRects,
            Segments,
            Circles,
            Fans,
        }
    }
}