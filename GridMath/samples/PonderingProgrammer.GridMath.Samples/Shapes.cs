using System.Collections.Generic;
using System.Linq;
using PonderingProgrammer.GridMath.Shapes;

namespace PonderingProgrammer.GridMath.Samples
{
    public static class Shapes
    {
        public static IEnumerable<IGridShape> GetShapes(Demo demo)
        {
            return demo switch
            {
                Demo.Point => CreatePoint(),
                Demo.Rect => CreateRect(),
                Demo.AlignedSegments => CreateAlignedSegments(),
                Demo.Segments => CreateSegments(),
                Demo.Circles => CreateCircles(),
                Demo.Fans1 => CreateDiagonalFans(),
                Demo.Fans2 => CreateOtherFans(),
                _ => Enumerable.Empty<IGridShape>(),
            };
        }

        private static IEnumerable<IGridShape> CreateOtherFans()
        {
            var center = new GridCoordinatePair(30, 20);
            var up = new GridFan(center.Translation(0, -2), 10, Grid8Direction.Top);
            var down = new GridFan(center.Translation(0, 2), 10, Grid8Direction.Bottom);
            var left = new GridFan(center.Translation(0, -2), 10, Grid8Direction.Left);
            var right = new GridFan(center.Translation(0, 2), 10, Grid8Direction.Right);
            return new[] {up, down, left, right};
        }

        private static IEnumerable<IGridShape> CreateDiagonalFans()
        {
            var center = new GridCoordinatePair(30, 20);
            var upleft = new GridFan(center.Translation(-2, -2), 10, Grid8Direction.TopLeft);
            var upRight = new GridFan(center.Translation(2, 2), 10, Grid8Direction.TopRight);
            var downLeft = new GridFan(center.Translation(-2, 2), 10, Grid8Direction.BottomLeft);
            var downRight = new GridFan(center.Translation(2, 2), 10, Grid8Direction.BottomRight);
            return new[] {upleft, downRight, downLeft, upRight};
        }

        private static IEnumerable<IGridShape> CreateCircles()
        {
            var center = new GridCoordinatePair(30, 20);
            return Enumerable.Range(0, 5).Select(r => new GridCircle(center, r * 5)).ToArray();
        }

        private static IEnumerable<IGridShape> CreateSegments()
        {
            var center = new GridCoordinatePair(30, 20);
            var ends = new []
            {
                new GridCoordinatePair(10, 0),
                new GridCoordinatePair(10, -5),
                new GridCoordinatePair(10, -10),
                new GridCoordinatePair(5, -10),
                new GridCoordinatePair(0, -10),
                new GridCoordinatePair(-5, -10),
                new GridCoordinatePair(-10, -10),
                new GridCoordinatePair(-10, -5),
                new GridCoordinatePair(-10, 0),
                new GridCoordinatePair(-10, 5),
                new GridCoordinatePair(-10, 10),
                new GridCoordinatePair(-5, 10),
                new GridCoordinatePair(0, 10),
                new GridCoordinatePair(5, 10),
                new GridCoordinatePair(10, 10),
                new GridCoordinatePair(10, 5),
            };
            return ends.Select(end => new GridSegment(center, end.Translation(center.X, center.Y))).ToArray();
        }

        private static IEnumerable<IGridShape> CreateAlignedSegments()
        {
            return new[]
            {
                new GridAASegment(new GridCoordinatePair(40, 1), new GridCoordinatePair(40, 10)), 
                new GridAASegment(new GridCoordinatePair(40, 5), new GridCoordinatePair(50, 5)),
            };
        }

        private static IEnumerable<IGridShape> CreateRect()
        {
            return new [] {new GridRectangle(GridBoundingBox.FromSize(20, 10, 15, 10))};
        }

        private static IEnumerable<IGridShape> CreatePoint()
        {
            return new [] {new GridPoint(new GridCoordinatePair(30, 20))};
        }

        public enum Demo
        {
            Point,
            Rect,
            AlignedSegments,
            Segments,
            Circles,
            Fans1,
            Fans2,
        }
    }
}