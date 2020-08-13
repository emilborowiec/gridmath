using PonderingProgrammer.GridMath.Shapes;
using Xunit;

namespace PonderingProgrammer.GridMath.Tests.Shapes
{
    public class GridRectangleTest
    {
        [Fact]
        public void TestUpdateContainedCoords()
        {
            var rect = new GridRectangle(GridBoundingBox.FromMinMax(2, 2, 4, 4));
            Assert.Equal(9, rect.ContainedCoordinates.Count);
            Assert.Contains(new GridCoordinatePair(2, 2), rect.ContainedCoordinates);
            Assert.Contains(new GridCoordinatePair(3, 3), rect.ContainedCoordinates);
            Assert.Contains(new GridCoordinatePair(4, 4), rect.ContainedCoordinates);
            rect.BoundingBox = rect.BoundingBox.Translation(10, 0);
            Assert.Contains(new GridCoordinatePair(12, 2), rect.ContainedCoordinates);
            Assert.Contains(new GridCoordinatePair(13, 3), rect.ContainedCoordinates);
            Assert.Contains(new GridCoordinatePair(14, 4), rect.ContainedCoordinates);
        }

        [Fact]
        public void TestRotate()
        {
            var rect = new GridRectangle(GridBoundingBox.FromMinMax(0, 0, 1, 0));
            rect.Rotate(Grid4Rotation.Ccw90);
            Assert.Equal(0, rect.BoundingBox.MinX);
            Assert.Equal(-1, rect.BoundingBox.MinY);
            Assert.Equal(0, rect.BoundingBox.MaxX);
            Assert.Equal(0, rect.BoundingBox.MaxY);
            rect.Rotate(Grid4Rotation.Cw90);
            Assert.Equal(-1, rect.BoundingBox.MinX);
            Assert.Equal(-1, rect.BoundingBox.MinY);
            Assert.Equal(0, rect.BoundingBox.MaxX);
            Assert.Equal(-1, rect.BoundingBox.MaxY);
        }
    }
}