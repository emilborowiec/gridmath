using PonderingProgrammer.GridMath.Shapes;
using Xunit;

namespace PonderingProgrammer.GridMath.Tests.Shapes
{
    public class GridPointTest
    {
        [Fact]
        public void TestUpdateBoundsAndCoords()
        {
            var point = new GridPoint(new GridCoordinatePair(0, 0));
            point.Translate(2, 5);
            Assert.Equal(2, point.BoundingBox.MinX);
            Assert.Equal(2, point.BoundingBox.MaxX);
            Assert.Equal(5, point.BoundingBox.MinY);
            Assert.Equal(5, point.BoundingBox.MaxY);
            Assert.Equal(5, point.ContainedCoordinates[0].Y);
            Assert.Equal(point.Coordinates, point.ContainedCoordinates[0]);
        }
    }
}